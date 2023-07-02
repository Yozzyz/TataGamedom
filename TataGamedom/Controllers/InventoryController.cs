using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TataGamedom.Models.Infra.DapperRepositories;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.Services;

namespace TataGamedom.Controllers
{
    public class InventoryController : Controller
    {
        
        public ActionResult Index()
        {
            IInventoryRepository repo = new InventoryRepository();
            var service = new InventoryService(repo); 
            var inventory = service.GetAll();
            return View(inventory);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

    }
}
