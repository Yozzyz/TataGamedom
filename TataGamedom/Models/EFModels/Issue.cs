namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Issue
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Issue()
        {
            Replies = new HashSet<Reply>();
        }

        public int Id { get; set; }

        public int? MemberId { get; set; }

        public int? IssueTypeId { get; set; }

        [Required]
        public string Content { get; set; }

        [StringLength(600)]
        public string File { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? Status { get; set; }

        public virtual IssueTypesCode IssueTypesCode { get; set; }

        public virtual Member Member { get; set; }

        public virtual IssueStatusCode IssueStatusCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reply> Replies { get; set; }
    }
}
