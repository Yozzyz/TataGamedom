using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TataGamedom.Models.ViewModels.Orders
{
	public class OrderCreateVM
	{
		public int Id { get; set; }

		[Display(Name = "會員")]
		public int MemberId { get; set; }

		[Display(Name = "訂單狀態")]
		public int OrderStatusId { get; set; }

		[Display(Name = "物流狀態")]
		public int ShipmentStatusId { get; set; }

		[Display(Name = "付款狀態")]
		public int PaymentStatusId { get; set; }

		[Display(Name = "訂單日期")]
		public DateTime CreatedAt { get; set; } = DateTime.Now;

		[Display(Name = "訂單完成日期")]
		[Compare("CreatedAt", ErrorMessage ="應填入相對於'訂單日期'近的日期")] //if<0, error
		public DateTime? CompletedAt { get; set; }

		[Display(Name = "寄送方式")]
		public int ShipmemtMethodId { get; set; }

		[Required]
		[StringLength(20)]
		[Display(Name = "收件人")]
		public string RecipientName { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "寄送地址")]
		public string ToAddress { get; set; }
		
		[Display(Name = "寄送日期")]
		public DateTime? SentAt { get; set; }
		
		[Display(Name = "抵達日期")]
		[Compare("CreatedAt", ErrorMessage = "應填入相對於'寄送日期'近的日期")]
		public DateTime? DeliveredAt { get; set; }

		[StringLength(20)]
		[Display(Name = "貨態追蹤代碼")]
		public string TrackingNum { get; set; }
	}
}