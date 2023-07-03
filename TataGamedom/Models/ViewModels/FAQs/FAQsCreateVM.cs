using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TataGamedom.Models.EFModels;

namespace TataGamedom.Models.ViewModels.CustomerServices
{
    public class FAQsCreateVM
    {
        public int Id { get; set; }

        [Display(Name = "輸入問題")]
        [Required]
        [StringLength(600)]
        public string Question { get; set; }

        [Display(Name = "輸入答案")]
        [Required]
        [StringLength(600)]

        public string Answer { get; set; }

        [Display(Name = "創建時間")]
        [Required]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "問題類別")]
        [Required]
        public string IssueTypeId { get; set; }
    }
}