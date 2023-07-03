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
using TataGamedom.Models.EFModels;
using TataGamedom.Models.Infra.DapperRepositories;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.Services;

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

        


    }
}
