using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TataGamedom.Models.ViewModels.Orders;

namespace TataGamedom.Models.Dtos.Orders
{
	public class OrderIndexDto
	{
		public string Index { get; set; }

		public string MemberName { get; set; }

		public string OrderStatusCodeName { get; set; }

		public string ShipmentStatusCodeName { get; set; }

		public string PaymentStatusCodeName { get; set; }

		public DateTime CreatedAt { get; set; }

		public int Total { get; set; }
	}
	public static class OrderIndexExts
	{
		public static OrderIndexVM ToIndexVM(this OrderIndexDto dto)
		{
			return new OrderIndexVM
			{
				Index = dto.Index,
				MemberName = dto.MemberName,
				OrderStatusCodeName = dto.OrderStatusCodeName,
				ShipmentStatusCodeName = dto.ShipmentStatusCodeName,
				PaymentStatusCodeName = dto.PaymentStatusCodeName,
				CreatedAt = dto.CreatedAt,
				Total = dto.Total
			};
		}
	}

}