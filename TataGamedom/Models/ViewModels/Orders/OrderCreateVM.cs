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

        [Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "會員編號")]
		[Range(1,99999999,ErrorMessage = "{0}不得小於1")]
		public int MemberId { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "訂單狀態")]
        [Range(1,4)]
        public int OrderStatusId { get; set; }
        
		[Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "物流狀態")]
        [Range(1, 7)]
        public int ShipmentStatusId { get; set; }
        
		[Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "付款狀態")]
        [Range(1, 5)]
        public int PaymentStatusId { get; set; }
        
		[Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "訂單日期")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

		[Display(Name = "訂單完成日期")]
        [DataType(DataType.Date)]
        public DateTime? CompletedAt { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "寄送方式")]
        [Range(1, 6)]
        public int ShipmemtMethodId { get; set; }

		[Required(ErrorMessage = "{0} 必填")]
		[StringLength(20)]
		[Display(Name = "收件人")]
		public string RecipientName { get; set; }

		[Required(ErrorMessage = "{0} 必填")]
		[StringLength(50)]
		[Display(Name = "寄送地址")]
		public string ToAddress { get; set; }
		
		[Display(Name = "寄送日期")]
        [DataType(DataType.Date)]
        public DateTime? SentAt { get; set; }
		
		[Display(Name = "抵達日期")]
        [DataType(DataType.Date)]
        public DateTime? DeliveredAt { get; set; }

		[StringLength(20)]
		[Display(Name = "貨態追蹤代碼")]
		public string TrackingNum { get; set; }
	}
}