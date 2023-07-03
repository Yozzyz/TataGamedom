using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TataGamedom.Controllers
{
	public class BoardsController : Controller
	{
		// GET: Boards
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult test()
		{
			return View();
		}
		public ActionResult ModeratorApplicationsList()
		{
			return View();

		}

		public ActionResult ModeratorApplicationDetail()
		{
			return View();

		}

	}
}