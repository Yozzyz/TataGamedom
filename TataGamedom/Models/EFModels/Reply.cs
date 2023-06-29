namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reply
    {
        public int Id { get; set; }

        public int? IssueId { get; set; }

        public int? BackendMemberId { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        public string Content { get; set; }

        public virtual BackendMember BackendMember { get; set; }

        public virtual Issue Issue { get; set; }
    }
}
