namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BucketLog
    {
        public int Id { get; set; }

        public int BucketMemberId { get; set; }

        public int? ModeratorMemberId { get; set; }

        public int? BackendMmemberId { get; set; }

        public int BoardId { get; set; }

        [Required]
        [StringLength(500)]
        public string BucketReason { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool IsNoctified { get; set; }

        public virtual BackendMember BackendMember { get; set; }

        public virtual Board Board { get; set; }

        public virtual Member Member { get; set; }

        public virtual Member Member1 { get; set; }
    }
}
