namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Coupon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Coupon()
        {
            CouponsProducts = new HashSet<CouponsProduct>();
            OrderItemsCoupons = new HashSet<OrderItemsCoupon>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public int Discount { get; set; }

        public int DiscountTypeId { get; set; }

        [Required]
        [StringLength(30)]
        public string Description { get; set; }

        public DateTime CreatedTime { get; set; }

        public int CreatedBackendMemberId { get; set; }

        public int Threshold { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool ActiveFlag { get; set; }

        public DateTime? ModifiedTime { get; set; }

        public int? ModifiedBackendMemberId { get; set; }

        public virtual BackendMember BackendMember { get; set; }

        public virtual BackendMember BackendMember1 { get; set; }

        public virtual DiscountTypeCode DiscountTypeCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CouponsProduct> CouponsProducts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItemsCoupon> OrderItemsCoupons { get; set; }
    }
}
