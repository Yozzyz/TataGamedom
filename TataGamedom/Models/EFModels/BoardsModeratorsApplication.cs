namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BoardsModeratorsApplication
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public int BoardId { get; set; }

        public DateTime ApplyDate { get; set; }

        public bool AddOrRemove { get; set; }

        [Required]
        [StringLength(500)]
        public string ApplyReason { get; set; }

        public int ApprovalStatusId { get; set; }

        public bool? ApprovalResult { get; set; }

        public int? BackendMemberId { get; set; }

        public DateTime? ApprovalStatusDate { get; set; }

        public virtual ApprovalStatusCode ApprovalStatusCode { get; set; }

        public virtual BackendMember BackendMember { get; set; }

        public virtual Board Board { get; set; }

        public virtual Member Member { get; set; }
    }
}
