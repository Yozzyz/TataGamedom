namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class InventoryItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InventoryItem()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Index { get; set; }

        public int ProductId { get; set; }

        public int StockInSheetId { get; set; }

        public decimal Cost { get; set; }

        [StringLength(50)]
        public string GameKey { get; set; }

        public virtual Product Product { get; set; }

        public virtual StockInSheet StockInSheet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
