namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MembersBoard
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public int BoardId { get; set; }

        public bool IsFavorite { get; set; }

        public virtual Board Board { get; set; }

        public virtual Member Member { get; set; }
    }
}
