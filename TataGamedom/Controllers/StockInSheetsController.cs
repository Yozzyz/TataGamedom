using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TataGamedom.Models.Dtos.StockInSheets;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.Infra;
using TataGamedom.Models.Infra.DapperRepositories;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.Services;
using TataGamedom.Models.ViewModels.StockInSheets;

namespace TataGamedom.Controllers
{
    public class StockInSheetsController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();
        private static readonly IStockInSheetRepository _repo = new StockInSheetRepository();
        private readonly StockInSheetService _service = new StockInSheetService(_repo);

        public ActionResult Index() => View(_service.Search().Select(stockInSheet => stockInSheet.ToVM()));
        public ActionResult Create() 
        {
            PrepareCreateDataSource(null, null);
            return View();
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StockInSheetVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            PrepareCreateDataSource(vm.StockInStatusId, vm.SupplierId);

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

        public ActionResult Edit(int? id) 
        {
            PrepareCreateDataSource(null, null);
            if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var stockInSheet = _service.GetById(id);
            return View(stockInSheet.ToVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StockInSheetVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            PrepareCreateDataSource(vm.StockInStatusId, vm.SupplierId);

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
        private void PrepareCreateDataSource(int?stockInStatusId, int? supplierId) 
        {
            var sisSelectList = new List<SelectListItem>();
            foreach (var sis in db.StockInStatusCodes) 
            {
                sisSelectList.Add(new SelectListItem { Value = sis.Id.ToString(), Text = sis.Name });
            }

            var supplierSelectList = new List<SelectListItem>();
            foreach (var supplier in db.Suppliers) 
            {
                supplierSelectList.Add(new SelectListItem { Value = supplier.Id.ToString(), Text = supplier.Name });
            }
            ViewBag.StockInStatusId = sisSelectList;
            ViewBag.SupplierId = supplierSelectList;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }

    }
}