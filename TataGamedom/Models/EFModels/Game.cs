namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Game
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Game()
        {
            Boards = new HashSet<Board>();
            GameClassificationGames = new HashSet<GameClassificationGame>();
            GameComments = new HashSet<GameComment>();
            News = new HashSet<News>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ChiName { get; set; }

        [Required]
        [StringLength(100)]
        public string EngName { get; set; }

        [Required]
        [StringLength(1500)]
        public string Description { get; set; }

        public bool IsRestrict { get; set; }

        [Required]
        [StringLength(100)]
        public string GameCoverImg { get; set; }

        public DateTime CreatedTime { get; set; }

        public int CreatedBackendMemberId { get; set; }

        public DateTime? ModifiedTime { get; set; }

        public int? ModifiedBackendMemberId { get; set; }

        public virtual BackendMember BackendMember { get; set; }

        public virtual BackendMember BackendMember1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Board> Boards { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GameClassificationGame> GameClassificationGames { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GameComment> GameComments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<News> News { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
