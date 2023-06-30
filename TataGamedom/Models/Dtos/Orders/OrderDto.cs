using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TataGamedom.Models.ViewModels.Orders;

namespace TataGamedom.Models.Dtos.Orders
{
	public class OrderDto
	{
		public int Id { get; set; }

		/// <summary>
		/// 自動產生的Index
		/// </summary>
		public string Index { get; set; }

		public int MemberId { get; set; }

		public int OrderStatusId { get; set; }

		public int ShipmentStatusId { get; set; }

		public int PaymentStatusId { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime? CompletedAt { get; set; }

		public int ShipmemtMethodId { get; set; }

		public string RecipientName { get; set; }

		public string ToAddress { get; set; }

		public DateTime? SentAt { get; set; }

		public DateTime? DeliveredAt { get; set; }

		public string TrackingNum { get; set; }
	}

	public static class OrderDtoExts 
	{
		public static OrderDto ToDto(this OrderCreateVM vm)
		{
			return new OrderDto
			{
				Id = vm.Id,
				MemberId = vm.MemberId,
				OrderStatusId = vm.OrderStatusId,
				ShipmentStatusId = vm.ShipmentStatusId,
				PaymentStatusId = vm.PaymentStatusId,
				CreatedAt = vm.CreatedAt,
				CompletedAt = vm.CompletedAt,
				ShipmemtMethodId = vm.ShipmemtMethodId,
				RecipientName = vm.RecipientName,
				ToAddress = vm.ToAddress,
				SentAt = vm.SentAt,
				DeliveredAt = vm.DeliveredAt,
				TrackingNum = vm.TrackingNum,
			};
		}
	}

}