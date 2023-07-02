using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TataGamedom.Models.ViewModels.Orders
{
    public class OrderInfoVM
    {
        public int Id { get; set; }

        [Display(Name = "訂單商品編號")]
        public string OrderItemIndex { get; set; }

        [Display(Name = "商品單價")]
        public decimal? ProductPrice { get; set; }

        [Display(Name = "訂單編號")]
        public string Index { get; set; }

        [Display(Name = "訂單日期")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "完成日期")]
        public DateTime? CompletedAt { get; set; }

        [Display(Name = "總額")]
        public decimal? Total { get; set; }

        [Display(Name = "遊戲序號")]
        public string GameKey { get; set; }

        [Display(Name = "訂單狀態")]
        public string OrderStatusCodeName { get; set; }
       
        [Display(Name = "物流狀態")]
        public string ShipmentStatusCodeName { get; set; }

        [Display(Name = "付款狀態")]
        public string PaymentStatusCodeName { get; set; }

        [Display(Name = "遊戲名稱")]
        public string GameName { get; set; }

        [Display(Name = "商品圖")]
        public string GameCoverImg { get; set; }

        [Display(Name = "優惠券")]
        public string CouponDescription { get; set; }
    }
}