namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PostComment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PostComment()
        {
            PostCommentReports = new HashSet<PostCommentReport>();
            PostComments1 = new HashSet<PostComment>();
            PostCommentUpDownVotes = new HashSet<PostCommentUpDownVote>();
        }

        public int Id { get; set; }

        public int MemberId { get; set; }

        public int PostId { get; set; }

        [Required]
        [StringLength(280)]
        public string Content { get; set; }

        public DateTime Datetime { get; set; }

        public bool ActiveFlag { get; set; }

        public DateTime? DeleteDatetime { get; set; }

        public int? DeleteMemberId { get; set; }

        public int? DeleteBackendMemberId { get; set; }

        public int? ParentId { get; set; }

        public virtual BackendMember BackendMember { get; set; }

        public virtual Member Member { get; set; }

        public virtual Member Member1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostCommentReport> PostCommentReports { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostComment> PostComments1 { get; set; }

        public virtual PostComment PostComment1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostCommentUpDownVote> PostCommentUpDownVotes { get; set; }

        public virtual Post Post { get; set; }
    }
}
