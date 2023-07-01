using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TataGamedom.Models.EFModels;

namespace TataGamedom.Models.ViewModels.News
{
	public class NewsCommentIndexVM
	{
		public int Id { get; set; }

		public int NewsId { get; set; }
		[Display(Name ="留言內容")]
		[Required]
		[StringLength(280)]
		public string Content { get; set; }
		[Display(Name = "留言時間")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
		public DateTime Time { get; set; }

		[Display(Name = "新聞標題")]
		public string Title { get; set; }

		public int MemberId { get; set; }
		[Display(Name = "留言者")]
		public string MemberName { get; set; }
		[Display(Name = "留言狀態")]
		public bool ActiveFlag { get; set; }

		public string ActiveFlagText
		{
			get
			{
				return ActiveFlag == true ? "公開" : "已刪除";
			}
		}

		public DateTime? DeleteDatetime { get; set; }

		public int? DeleteMemberId { get; set; }

		public int? DeleteBackendMemberId { get; set; }

		public virtual BackendMember BackendMember { get; set; }

		public virtual Member Member { get; set; }
		
	}
}