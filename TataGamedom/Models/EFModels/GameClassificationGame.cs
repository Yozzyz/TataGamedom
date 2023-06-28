namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GameClassificationGame
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public int GameClassificationId { get; set; }

        public virtual GameClassificationsCode GameClassificationsCode { get; set; }

        public virtual Game Game { get; set; }
    }
}
