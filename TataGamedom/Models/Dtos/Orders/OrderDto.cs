using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using TataGamedom.Models.EFModels;
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
		public static OrderDto ToDto(this Order entity) 
		{
			return new OrderDto
			{
				Id = entity.Id,
                Index = entity.Index,
                MemberId = entity.MemberId,
				OrderStatusId = entity.OrderStatusId,
				ShipmentStatusId = entity.ShipmentStatusId,
				PaymentStatusId = entity.PaymentStatusId,
				CreatedAt = entity.CreatedAt,
				CompletedAt = entity.CompletedAt,
				ShipmemtMethodId= entity.ShipmemtMethodId,
				RecipientName= entity.RecipientName,
				ToAddress = entity.ToAddress,
				SentAt= entity.SentAt,
				DeliveredAt= entity.DeliveredAt,
				TrackingNum = entity.TrackingNum
			};
		}
        public static OrderEditVM ToEditVM(this OrderDto dto)
        {
            return new OrderEditVM
            {
                Id = dto.Id,
                Index = dto.Index,
                MemberId = dto.MemberId,
                OrderStatusId = dto.OrderStatusId,
                ShipmentStatusId = dto.ShipmentStatusId,
                PaymentStatusId = dto.PaymentStatusId,
                CreatedAt = dto.CreatedAt,
                CompletedAt = dto.CompletedAt,
                ShipmemtMethodId = dto.ShipmemtMethodId,
                RecipientName = dto.RecipientName,
                ToAddress = dto.ToAddress,
                SentAt = dto.SentAt,
                DeliveredAt = dto.DeliveredAt,
                TrackingNum = dto.TrackingNum
            };
        }
		public static OrderDto ToDto(this OrderEditVM vm) 
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