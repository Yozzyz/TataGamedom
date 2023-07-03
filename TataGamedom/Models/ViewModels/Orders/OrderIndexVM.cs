using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TataGamedom.Models.ViewModels.Orders
{
	public class OrderIndexVM
	{
		public int Id { get; set; }

		[Display(Name = "訂單編號")]
		public string Index { get; set; }

		[Display(Name = "會員")]
		public string MemberName { get; set; }

		[Display(Name = "訂單狀態")]
		public string OrderStatusCodeName { get; set; }

		[Display(Name = "物流狀態")]
		public string ShipmentStatusCodeName { get; set; }

		[Display(Name = "付款狀態")]
		public string PaymentStatusCodeName { get; set; }

		[Display(Name = "訂單日期")]
		public DateTime CreatedAt { get; set; }

		[Display(Name = "訂單日期")]
		public string CreateAtText { get => CreatedAt.ToString("yyyy/MM/dd"); }

		[Display(Name = "總額")]
		public int Total { get; set; }
	}
}