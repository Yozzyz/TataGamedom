namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Carts = new HashSet<Cart>();
            CouponsProducts = new HashSet<CouponsProduct>();
            InventoryItems = new HashSet<InventoryItem>();
            MemberProductViews = new HashSet<MemberProductView>();
            OrderItems = new HashSet<OrderItem>();
            ProductImages = new HashSet<ProductImage>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Index { get; set; }

        public int? GameId { get; set; }

        public bool IsVirtual { get; set; }

        public int Price { get; set; }

        public int GamePlatformId { get; set; }

        [Required]
        [StringLength(1500)]
        public string SystemRequire { get; set; }

        public int ProductStatusId { get; set; }

        public DateTime SaleDate { get; set; }

        public int CreatedBackendMemberId { get; set; }

        public DateTime CreatedTime { get; set; }

        public int? ModifiedBackendMemberId { get; set; }

        public DateTime? ModifiedTime { get; set; }

        public virtual BackendMember BackendMember { get; set; }

        public virtual BackendMember BackendMember1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CouponsProduct> CouponsProducts { get; set; }

        public virtual GamePlatformsCode GamePlatformsCode { get; set; }

        public virtual Game Game { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MemberProductView> MemberProductViews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductImage> ProductImages { get; set; }

        public virtual ProductStatusCode ProductStatusCode { get; set; }
    }
}
