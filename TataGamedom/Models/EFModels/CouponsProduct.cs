namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CouponsProduct
    {
        public int Id { get; set; }

        public int CouponId { get; set; }

        public int ProductId { get; set; }

        public virtual Coupon Coupon { get; set; }

        public virtual Product Product { get; set; }
    }
}
