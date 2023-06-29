namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StockInSheet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StockInSheet()
        {
            InventoryItems = new HashSet<InventoryItem>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Index { get; set; }

        public int StockInStatusId { get; set; }

        public int SupplierId { get; set; }

        public int Quantity { get; set; }

        public DateTime OrderRequestDate { get; set; }

        public DateTime? ArrivedAt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }

        public virtual StockInStatusCode StockInStatusCode { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
