using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TataGamedom.Models.ViewModels.Coupons;

namespace TataGamedom.Models.Interfaces
{
	public interface ICouponRepository
	{
		IEnumerable<CouponIndexVM> Get();
	}
}
