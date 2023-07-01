using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.ViewModels.Coupons;

namespace TataGamedom.Models.Services
{
	public class CouponService
	{
		private ICouponRepository _repo;
        public CouponService(ICouponRepository repo)
        {
            _repo=repo;
        }

        public IEnumerable<CouponIndexVM> Get()
        {
            return _repo.Get();
        }
    }
}