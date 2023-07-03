using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TataGamedom.Models.Dtos.FAQs
{
    public class FAQIndexDto
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime CreateAt { get; set; }
        public int IssueTypeId { get; set; }
    }
}