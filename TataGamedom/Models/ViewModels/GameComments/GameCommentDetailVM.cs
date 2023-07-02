using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TataGamedom.Models.ViewModels.GameComments
{
	public class GameCommentDetailVM
	{
		public int Id { get; set; }
		[Display(Name = "遊戲名稱")]
		public string GameName { get; set; }
		[Display(Name = "評論者")]
		public string MemberName { get; set; }
		[Display(Name = "評論內容")]

		public string Content { get; set; }

		[Display(Name = "評分")]
		public byte Score { get; set; }
		[Display(Name = "評論時間")]
		public DateTime CreatedTime { get; set; }

        public bool ActiveFlag { get; set; }

        public DateTime DeleteDateTime { get; set; }
        public int DeleteBackendMemberId { get; set; }
    }
}