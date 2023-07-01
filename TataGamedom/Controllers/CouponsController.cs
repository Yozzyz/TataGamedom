using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TataGamedom.Models.Infra.DapperRepositories;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.Services;
using TataGamedom.Models.ViewModels.Coupons;

namespace TataGamedom.Controllers
{
    public class CouponsController : Controller
    {
        [Authorize]
        // GET: Coupons
        public ActionResult Index()
        {
            IEnumerable<CouponIndexVM> coupons = GetCoupons();
            return View(coupons);
        }

		private IEnumerable<CouponIndexVM> GetCoupons()
		{
			ICouponRepository repo = new CouponDapperRepository();
            CouponService service = new CouponService(repo);
            return service.Get();
		}
	}
}