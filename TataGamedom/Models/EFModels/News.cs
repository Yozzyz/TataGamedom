namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public News()
        {
            NewsComments = new HashSet<NewsComment>();
            NewsLikes = new HashSet<NewsLike>();
            NewsViews = new HashSet<NewsView>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public int BackendMemberId { get; set; }

        public int NewsCategoryId { get; set; }

        public int? GamesId { get; set; }

        [StringLength(100)]
        public string CoverImg { get; set; }

        public DateTime ScheduleDate { get; set; }

        public bool ActiveFlag { get; set; }

        public DateTime? DeleteDatetime { get; set; }

        public int? DeleteBackendMemberId { get; set; }

        public virtual BackendMember BackendMember { get; set; }

        public virtual BackendMember BackendMember1 { get; set; }

        public virtual Game Game { get; set; }

        public virtual NewsCategoryCode NewsCategoryCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NewsComment> NewsComments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NewsLike> NewsLikes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NewsView> NewsViews { get; set; }
    }
}
