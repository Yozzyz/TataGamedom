namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderItemsCoupon
    {
        public int Id { get; set; }

        public int OrderItemId { get; set; }

        public int? CouponId { get; set; }

        public virtual Coupon Coupon { get; set; }

        public virtual OrderItem OrderItem { get; set; }
    }
}
