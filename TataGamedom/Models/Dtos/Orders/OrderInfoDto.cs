using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.ViewModels.Orders;

namespace TataGamedom.Models.Dtos.Orders
{
    public class OrderInfoDto
    {
        public int Id { get; set; }
        public string OrderItemIndex { get; set; }
        
        public decimal? ProductPrice { get; set; }
        
        public string Index { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime? CompletedAt { get; set; }
        
        public decimal? Total { get; set; }
        
        public string GameKey { get; set; }
        
        public string OrderStatusCodeName { get; set; }
        
        public string PaymentStatusCodeName { get; set; }
        
        public string ShipmentStatusCodeName { get; set; }

        public string GameName{ get; set; }

        public string GameCoverImg { get; set; }

        public string CouponDescription { get; set; }
    }

    public static class OrderInfoExts 
    {
        public static OrderInfoVM ToVM(this OrderInfoDto dto) 
        {
            return new OrderInfoVM 
            {
                Id = dto.Id,
                OrderItemIndex = dto.OrderItemIndex,
                ProductPrice = dto.ProductPrice,
                Index = dto.Index,
                CreatedAt = dto.CreatedAt,
                CompletedAt = dto.CompletedAt,
                Total = dto.Total,
                GameKey = dto.GameKey,
                OrderStatusCodeName = dto.OrderStatusCodeName,
                PaymentStatusCodeName = dto.PaymentStatusCodeName,
                ShipmentStatusCodeName = dto.ShipmentStatusCodeName,
                GameCoverImg = dto.GameCoverImg,
                GameName = dto.GameName,
                CouponDescription = dto.CouponDescription
            };
        }
    }
}