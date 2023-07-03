namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FAQ")]
    public partial class FAQ
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public int Id { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int IssueTypeId { get; set; }   

        public virtual IssueTypesCode IssueTypesCode { get; set; }

    }
}
