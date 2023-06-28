namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PostReport
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public int MemberID { get; set; }

        public DateTime Datetime { get; set; }

        [Required]
        [StringLength(500)]
        public string Reason { get; set; }

        public int? ReviewerBackenMemberId { get; set; }

        public DateTime? ReviewDatetime { get; set; }

        public bool IsCommit { get; set; }

        public string ReviewComment { get; set; }

        public virtual BackendMember BackendMember { get; set; }

        public virtual Member Member { get; set; }

        public virtual Post Post { get; set; }
    }
}
