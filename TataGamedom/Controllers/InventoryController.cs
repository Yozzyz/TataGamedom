using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TataGamedom.Controllers
{
    public class InventoryController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }

    }
}
