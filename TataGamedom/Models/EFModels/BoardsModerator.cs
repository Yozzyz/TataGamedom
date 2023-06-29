namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BoardsModerator
    {
        public int Id { get; set; }

        public int ModeratorMemberId { get; set; }

        public int BoardId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual Board Board { get; set; }

        public virtual Member Member { get; set; }
    }
}
