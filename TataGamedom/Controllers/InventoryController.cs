using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using TataGamedom.Models.Dtos.InventoryItems;
using TataGamedom.Models.Dtos.Orders;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.Infra;
using TataGamedom.Models.Infra.DapperRepositories;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.Services;
using TataGamedom.Models.ViewModels.InventoryItems;
using TataGamedom.Models.ViewModels.Orders;

namespace TataGamedom.Controllers
{

    public class InventoryController : Controller
    {
        private AppDbContext db = new AppDbContext();
        private static readonly IInventoryRepository _repo = new InventoryRepository();
        private readonly InventoryService _service = new InventoryService(_repo);

        public ActionResult Index()
        {
            var inventory = _service.GetAll();
            return View(inventory);
        }

        public ActionResult Details(int? productId)
        {
            if (productId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var orderInfo = _service.GetItemInfo(productId);
            return View(orderInfo);

        }

		public ActionResult Create()
		{
			PrepareCreateInventoryDataSource(null, null);
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(InventoryItemVM vm)
		{
			if (!ModelState.IsValid) return View(vm);

			PrepareCreateInventoryDataSource(vm.ProductId, vm.StockInSheetIndex);
			Result result = _service.Create(vm.ToDto());
			if (result.IsSuccess)
			{
				return RedirectToAction("Details", new { productId = vm.ProductId });
			}
			else
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return View(vm);
			}

		}


		public ActionResult Edit(string index)
		{
			PrepareCreateInventoryDataSource(null, null);

			if (index == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			var order = _service.GetByIndex(index).ToVM();
			return View(order);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(InventoryItemVM vm)
		{
			if (!ModelState.IsValid) return View(vm);
			PrepareCreateInventoryDataSource(vm.ProductId, vm.StockInSheetIndex);

			Result result = _service.Update(vm.ToEditDto());
			if (result.IsSuccess)
			{
				return RedirectToAction("Details", new { productId = vm.ProductId });
			}
			else
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return View(vm);
			}
		}


		private void PrepareCreateInventoryDataSource(int? productId, string StockInSheetIndex)
		{
			var productIdSelectList = new List<SelectListItem>();
			foreach (var p in db.Products) 
			{
				productIdSelectList.Add(new SelectListItem { Value = p.Id.ToString(), Text = p.Index }); 
			}

			ViewBag.productId = productIdSelectList;

			var StockInSheetIndexSelectList = new List<SelectListItem>();
			foreach (var sis in db.StockInSheets) 
			{
				StockInSheetIndexSelectList.Add(new SelectListItem { Value = sis.Id.ToString(), Text = sis.Index }); 
			}
			ViewBag.StockInSheetIndex = StockInSheetIndexSelectList;
		}


	}
}
