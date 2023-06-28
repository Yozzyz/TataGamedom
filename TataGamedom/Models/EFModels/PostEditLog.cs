namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PostEditLog
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        [Required]
        [StringLength(1500)]
        public string ContentBeforeEdit { get; set; }

        public DateTime EditDatetime { get; set; }

        public virtual Post Post { get; set; }
    }
}
