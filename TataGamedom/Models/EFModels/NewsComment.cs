namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NewsComment
    {
        public int Id { get; set; }

        public int NewsId { get; set; }

        [Required]
        [StringLength(280)]
        public string Content { get; set; }

        public DateTime Time { get; set; }

        public int MemberID { get; set; }

        public bool ActiveFlag { get; set; }

        public DateTime? DeleteDatetime { get; set; }

        public int? DeleteMemberId { get; set; }

        public int? DeleteBackendMemberId { get; set; }

        public virtual BackendMember BackendMember { get; set; }

        public virtual Member Member { get; set; }

        public virtual Member Member1 { get; set; }

        public virtual News News { get; set; }

		public virtual News Title { get; set; }
	}
}
