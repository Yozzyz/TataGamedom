namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderItem()
        {
            OrderItemReturns = new HashSet<OrderItemReturn>();
            OrderItemsCoupons = new HashSet<OrderItemsCoupon>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Index { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public decimal ProductPrice { get; set; }

        public int InventoryItemId { get; set; }

        public virtual InventoryItem InventoryItem { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItemReturn> OrderItemReturns { get; set; }

        public virtual Order Order { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItemsCoupon> OrderItemsCoupons { get; set; }

        public virtual Product Product { get; set; }
    }
}
