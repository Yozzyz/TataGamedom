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
using TataGamedom.Models.ViewModels.Orders;

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
			PrepareCreateOrderDataSource(null, null, null, null, null);
            return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(OrderCreateVM vm)
		{
			if (ModelState.IsValid)
			{
				PrepareCreateOrderDataSource(vm.MemberId, vm.OrderStatusId, vm.PaymentStatusId, vm.ShipmemtMethodId, vm.ShipmentStatusId);
                _service.Create(vm.ToDto());
				return RedirectToAction("Index");
			}

			return View(vm);
		}
        private void PrepareCreateOrderDataSource(int? memberId, int? orderStatusId, int? paymentStatusId, int? shipmemtMethodId, int? shipmentStatusId)
        {
            ViewBag.MemberId = new SelectList(db.Members.ToList().Prepend(new Member {Id = 0, Name = ""}), "Id", "Name", memberId);
            ViewBag.OrderStatusId = new SelectList(db.OrderStatusCodes.ToList().Prepend(new OrderStatusCode { Id = 0, Name = ""}), "Id", "Name", orderStatusId);
            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatusCodes.ToList().Prepend(new PaymentStatusCode { Id = 0, Name = "" }), "Id", "Name", paymentStatusId);
            ViewBag.ShipmemtMethodId = new SelectList(db.ShipmemtMethods.ToList().Prepend(new ShipmemtMethod { Id = 0, Name = "" }), "Id", "Name", shipmemtMethodId);
            ViewBag.ShipmentStatusId = new SelectList(db.ShipmentStatusesCodes.ToList().Prepend(new ShipmentStatusesCode { Id = 0, Name = "" }), "Id", "Name", shipmentStatusId);
        }

    }
}