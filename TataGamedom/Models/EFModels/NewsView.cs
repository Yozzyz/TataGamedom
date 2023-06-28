namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NewsView
    {
        public int Id { get; set; }

        public int NewsId { get; set; }

        public int MemberId { get; set; }

        public DateTime ViewTime { get; set; }

        public virtual Member Member { get; set; }

        public virtual News News { get; set; }
    }
}
