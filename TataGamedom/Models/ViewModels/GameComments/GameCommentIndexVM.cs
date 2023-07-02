using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TataGamedom.Models.ViewModels.GameComments
{
	public class GameCommentIndexVM
	{
        public int Id { get; set; }
        [Display(Name ="遊戲名稱")]
		public string GameName { get; set; }
		[Display(Name = "評論者")]
		public string MemberName { get; set; }
		public string Content { get; set; }
		[Display(Name = "評論內容")]

		public string ContentText
		{
			get
			{
				return this.Content.Length > 10 ? this.Content.Substring(0, 10) + "..." : this.Content;
			}
		}
		[Display(Name = "評分")]
		public byte Score { get; set; }
		[Display(Name = "評論時間")]
		public DateTime CreatedTime { get; set; }
		[Display(Name = "狀態")]
		public bool ActiveFlag { get; set; }
		[Display(Name = "最後修改者")]
		public string DeleteBackendMemberName { get; set; }
		[Display(Name = "最後修改時間")]
		public DateTime? DeleteDatetime { get; set; }

	}
}