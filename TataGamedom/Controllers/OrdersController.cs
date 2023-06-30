using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TataGamedom.Models.EFModels;
using static TataGamedom.Models.Services.OrderService;
using TataGamedom.Models.Infra.DapperRepositories;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.Services;
using TataGamedom.Models.Dtos.Orders;

namespace TataGamedom.Controllers
{
	public class OrdersController : Controller
	{
		private AppDbContext db = new AppDbContext();
		private static readonly IOrderRepository _repo = new OrderRepository();
		private readonly OrderService _service = new OrderService(_repo);

		public ActionResult Index(Criteria criteria, string columnName, string directionName, int pageNum = 1)
		{
			criteria = criteria ?? new Criteria();
			PrepareOrderStatusDataSource(criteria.OrderStatusId);
			ViewBag.Criteria = criteria;

			var sortInfo = new SortInfo(columnName, directionName, criteria);
			ViewBag.SortInfo = sortInfo;


			int totalRecord = _service.Search(criteria, sortInfo).Count();
			int pageSize = 20;
			PageInfo pageInfo = new PageInfo(totalRecord, pageNum, pageSize, "/Orders/Index/?PageNum={0}");
			ViewBag.PageInfo = pageInfo;


			string sqlAddPage = GetSql(criteria, sortInfo) + pageInfo.sqlPage(criteria, sortInfo);
			var orders = _service.Search(criteria, sortInfo, sqlAddPage).Select(order => order.ToIndexVM());
			return View(orders);
		}

		private void PrepareOrderStatusDataSource(int? orderStatusId)
		{
			ViewBag.OrderStatusId = new SelectList(
				db.OrderStatusCodes
				.ToList()
				.Prepend(new OrderStatusCode { Id = 0, Name = "" }), "Id", "Name", orderStatusId
				);
		}



		public ActionResult Create()
		{
			ViewBag.MemberId = new SelectList(db.Members, "Id", "Name");
			ViewBag.OrderStatusId = new SelectList(db.OrderStatusCodes, "Id", "Name");
			ViewBag.PaymentStatusId = new SelectList(db.PaymentStatusCodes, "Id", "Name");
			ViewBag.ShipmemtMethodId = new SelectList(db.ShipmemtMethods, "Id", "Name");
			ViewBag.ShipmentStatusId = new SelectList(db.ShipmentStatusesCodes, "Id", "Name");
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Order order)
		{
			if (ModelState.IsValid)
			{
				db.Orders.Add(order);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.MemberId = new SelectList(db.Members, "Id", "Name", order.MemberId);
			ViewBag.OrderStatusId = new SelectList(db.OrderStatusCodes, "Id", "Name", order.OrderStatusId);
			ViewBag.PaymentStatusId = new SelectList(db.PaymentStatusCodes, "Id", "Name", order.PaymentStatusId);
			ViewBag.ShipmemtMethodId = new SelectList(db.ShipmemtMethods, "Id", "Name", order.ShipmemtMethodId);
			ViewBag.ShipmentStatusId = new SelectList(db.ShipmentStatusesCodes, "Id", "Name", order.ShipmentStatusId);
			return View(order);
		}

	}
}