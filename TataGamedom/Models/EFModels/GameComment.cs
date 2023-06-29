namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GameComment
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public int MemberId { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        public byte Score { get; set; }

        public DateTime CreatedTime { get; set; }

        public bool ActiveFlag { get; set; }

        public DateTime? DeleteDatetime { get; set; }

        public int? DeleteBackendMemberId { get; set; }

        public virtual BackendMember BackendMember { get; set; }

        public virtual Game Game { get; set; }

        public virtual Member Member { get; set; }
    }
}
