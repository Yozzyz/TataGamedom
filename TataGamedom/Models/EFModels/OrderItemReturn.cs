namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderItemReturn
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Index { get; set; }

        public int OrderItemId { get; set; }

        [Required]
        [StringLength(500)]
        public string Reason { get; set; }

        public DateTime IssuedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public bool IsRefunded { get; set; }

        public bool IsReturned { get; set; }

        public bool IsResellable { get; set; }

        public virtual OrderItem OrderItem { get; set; }
    }
}
