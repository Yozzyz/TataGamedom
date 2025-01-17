﻿using System;
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
using TataGamedom.Models.Infra;
using System.Web.Http.Results;
using System.Data.Entity.Core.Metadata.Edm;

namespace TataGamedom.Controllers
{
	public class OrdersController : Controller
	{
		private readonly AppDbContext db = new AppDbContext();
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
			PrepareCreateOrderDataSource(null, null, null, null);
            return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(OrderCreateVM vm)
		{
            if (!ModelState.IsValid) return View(vm);

			PrepareCreateOrderDataSource(vm.OrderStatusId, vm.PaymentStatusId, vm.ShipmemtMethodId, vm.ShipmentStatusId);
            Result result = _service.Create(vm.ToDto());	
			if (result.IsSuccess)
			{
				return RedirectToAction("Index");
			}
			else 
			{
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                return View(vm);
            }

		}
		private void PrepareCreateOrderDataSource(int? orderStatusId, int? paymentStatusId, int? shipmemtMethodId, int? shipmentStatusId)
		{
			var orderStatusSelectList = new List<SelectListItem>();
			foreach (var osc in db.OrderStatusCodes) { orderStatusSelectList.Add(new SelectListItem { Value = osc.Id.ToString(), Text = osc.Name}); }
			ViewBag.OrderStatuses = orderStatusSelectList;

            var paymentStatusSelectList = new List<SelectListItem>();
            foreach (var psc in db.PaymentStatusCodes) { paymentStatusSelectList.Add(new SelectListItem { Value = psc.Id.ToString(), Text = psc.Name }); }
            ViewBag.PaymentStatuses = paymentStatusSelectList;

            var shipmemtMethodSelectList = new List<SelectListItem>();
            foreach (var smc in db.ShipmemtMethods) { shipmemtMethodSelectList.Add(new SelectListItem { Value = smc.Id.ToString(), Text = smc.Name }); }
            ViewBag.ShipmemtMethods = shipmemtMethodSelectList;

            var shipmentStatusSelectList = new List<SelectListItem>();
            foreach (var ssc in db.ShipmentStatusesCodes) { shipmentStatusSelectList.Add(new SelectListItem { Value = ssc.Id.ToString(), Text = ssc.Name }); }
            ViewBag.ShipmentStatuses = shipmentStatusSelectList;

		}

		public ActionResult Info(string index)
		{
			if (index == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			var orderInfo = _service.GetOrderItemsInfo(index).Select(x => x.ToVM());
			return View(orderInfo);
		}

		public ActionResult Edit(string index) 
		{
            PrepareCreateOrderDataSource(null, null, null, null);

            if (index == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			var order = _service.GetByIndex(index).ToEditVM();
			return View(order);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            PrepareCreateOrderDataSource(vm.OrderStatusId, vm.PaymentStatusId, vm.ShipmemtMethodId, vm.ShipmentStatusId);

            Result result = _service.Update(vm.ToDto());
            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                return View(vm);
            }
        }


   //     public ActionResult Delete(string index)
   //     {
   //         if (index == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			//var order = db.Orders.SingleOrDefault(x => x.Index == index);
   //         if (order == null) return HttpNotFound();
   //         return View(order);
   //     }

   //     [HttpPost]
   //     [ActionName("Delete")]
   //     [ValidateAntiForgeryToken]
   //     public ActionResult DeleteConfirmed(string index)
   //     {
			//_service.Delete(index);
   //         return RedirectToAction("Index");
   //     }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}