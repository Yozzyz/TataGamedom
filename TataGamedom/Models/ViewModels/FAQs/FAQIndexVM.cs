using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TataGamedom.Models.EFModels;

namespace TataGamedom.Models.ViewModels.CustomerServices
{
    public class FAQIndexVM
    {
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Display(Name = "問題")]
        public string Question { get; set; }

        [Display(Name = "答案")]
        public string Answer { get; set; }

        [Display(Name = "創建時間")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "問題類別")]
        public int IssueTypeId { get; set; }
        public virtual IssueTypesCode IssueTypesCode { get; set; }
    }
}