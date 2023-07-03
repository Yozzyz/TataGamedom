namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Post()
        {
            PostComments = new HashSet<PostComment>();
            PostEditLogs = new HashSet<PostEditLog>();
            PostReports = new HashSet<PostReport>();
            PostUpDownVotes = new HashSet<PostUpDownVote>();
        }

        public int Id { get; set; }

        public int MemberId { get; set; }

        public int? BoardId { get; set; }

        [Required]
        [StringLength(1500)]
        public string Content { get; set; }

        public DateTime Datetime { get; set; }

        public DateTime LastEditDatetime { get; set; }

        public bool ActiveFlag { get; set; }

        public DateTime? DeleteDatetime { get; set; }

        public int? DeleteMemberId { get; set; }

        public int? DeleteBackendMemberId { get; set; }

        public virtual BackendMember BackendMember { get; set; }

        public virtual Board Board { get; set; }

        public virtual Member Member { get; set; }

        public virtual Member Member1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostComment> PostComments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostEditLog> PostEditLogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostReport> PostReports { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostUpDownVote> PostUpDownVotes { get; set; }
    }
}
