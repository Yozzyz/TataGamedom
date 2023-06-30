using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TataGamedom.Models.EFModels;

namespace TataGamedom.Models.ViewModels.Coupons
{
	public class CouponIndexVM
	{
		[Display(Name ="編號")]
		public int CouponId { get; set; }
		[Display(Name = "活動名稱")]
		[Required]
		[StringLength(20)]
		public string Name { get; set; }
		[Display(Name = "活動內容")]
		[Required]
		[StringLength(30)]
		public string Description { get; set; }
		[Display(Name = "門檻")]
		[Required]
		public int Threshold { get; set; }

		[Display(Name = "開始時間")]
		[Required]
		public DateTime StartTime { get; set; }
		[Display(Name = "結束時間")]
		[Required]
		public DateTime EndTime { get; set; }
		[Display(Name = "創建者")]
		public string CreatedBackendMemberName { get; set; }

		[Display(Name = "創建時間")]
		public DateTime CreatedTime { get; set; }
		[Display(Name = "狀態")]
		public bool ActiveFlag { get; set; }
	}
}