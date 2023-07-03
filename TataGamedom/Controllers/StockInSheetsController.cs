using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TataGamedom.Models.Infra.DapperRepositories;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.Services;

namespace TataGamedom.Controllers
{
    public class StockInSheetsController : Controller
    {
        private static readonly IStockInSheetRepository _repo = new StockInSheetRepository();
        private readonly StockInSheetService _service = new StockInSheetService(_repo);
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create() 
        {
			return View();
		}
        public ActionResult Edit() 
        {
            return View();
        }

        private void PrepareCreateDataSource(int?stockInStatusId, int? supplierId) 
        {
            
        }

	}
}