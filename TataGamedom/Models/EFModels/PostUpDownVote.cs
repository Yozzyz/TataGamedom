namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PostUpDownVote
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public int PostId { get; set; }

        public DateTime Date { get; set; }

        public bool Type { get; set; }

        public virtual Member Member { get; set; }

        public virtual Post Post { get; set; }
    }
}
