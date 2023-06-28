namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NewsletterLog
    {
        public int Id { get; set; }

        public int? NewsletterId { get; set; }

        public DateTime EmailSentDatetime { get; set; }

        public int AddresseeMemberID { get; set; }

        [Required]
        [StringLength(50)]
        public string AddresseeMemberName { get; set; }

        [Required]
        [StringLength(150)]
        public string AddresseeMemberEmail { get; set; }

        public virtual Member Member { get; set; }

        public virtual Newsletter Newsletter { get; set; }
    }
}
