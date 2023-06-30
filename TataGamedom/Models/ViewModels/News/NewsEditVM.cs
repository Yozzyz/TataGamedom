using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TataGamedom.Models.EFModels;

namespace TataGamedom.Models.ViewModels.News
{
	public class NewsEditVM
	{
		public int Id { get; set; }
		[Display(Name = "新聞標題")]
		[Required]
		[StringLength(100)]
		public string Title { get; set; }
		[Display(Name = "新聞內容")]
		[Required]
		[AllowHtml]
		public string Content { get; set; }

		public string ContentText
		{
			get
			{
				return this.Content.Length > 10
					? this.Content.Substring(0, 10) + "..."
					: this.Content;
			}

		}
		[Display(Name = "修改人員")]
		public int BackendMemberId { get; set; }
		[Display(Name = "新聞類別")]
		public int? NewsCategoryId { get; set; }
		[Display(Name = "遊戲類別")]
		public int? GamesId { get; set; }
		[Display(Name = "圖片")]
		[StringLength(100)]
		public string CoverImg { get; set; }
		[Display(Name = "上線時間")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
		public DateTime ScheduleDate { get; set; }
		[Display(Name = "狀態")]
		public bool ActiveFlag { get; set; }

		public string ActiveFlagText
		{
			get
			{
				return ActiveFlag == true ? "上線" : "隱藏";
			}
		}

		public DateTime? DeleteDatetime { get; set; }

		public int? DeleteBackendMemberId { get; set; }

		public BackendMember BackendMember { get; set; }
		[Display(Name = "修改人員")]
		public string BackendMemberName { get; set; }

		[Display(Name = "遊戲分類")]
		public string GameClassificationName { get; set; }
	}
}