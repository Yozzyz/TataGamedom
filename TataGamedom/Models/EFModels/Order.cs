namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Index { get; set; }

        public int MemberId { get; set; }

        public int OrderStatusId { get; set; }

        public int ShipmentStatusId { get; set; }

        public int PaymentStatusId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public int ShipmemtMethodId { get; set; }

        [Required]
        [StringLength(20)]
        public string RecipientName { get; set; }

        [Required]
        [StringLength(50)]
        public string ToAddress { get; set; }

        public DateTime? SentAt { get; set; }

        public DateTime? DeliveredAt { get; set; }

        [StringLength(20)]
        public string TrackingNum { get; set; }

        public virtual Member Member { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public virtual OrderStatusCode OrderStatusCode { get; set; }

        public virtual PaymentStatusCode PaymentStatusCode { get; set; }

        public virtual ShipmemtMethod ShipmemtMethod { get; set; }

        public virtual ShipmentStatusesCode ShipmentStatusesCode { get; set; }
    }
}
