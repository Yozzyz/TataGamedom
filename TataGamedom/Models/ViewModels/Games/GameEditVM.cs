using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TataGamedom.Models.ViewModels.Games
{
	public class GameEditVM
	{
		public int Id { get; set; }

		[Display(Name = "中文名稱")]
		[Required]
		[StringLength(50)]
		public string ChiName { get; set; }

		[Display(Name = "英文名稱")]
		[Required]
		[StringLength(100)]
		public string EngName { get; set; }
		public string Classification { get; set; }
		//[Display(Name = "遊戲類別（複選最多兩項）")]
		//public List<GameClassificationsCode> GameClassification { get; set; }

		//[Required]
		//public List<int> SelectedGameClassification { get; set; }

		[Display(Name = "遊戲介紹")]
		[Required]
		[StringLength(1500)]
		public string Description { get; set; }
		[Display(Name = "年齡限制")]
		[Required]
		public bool IsRestrict { get; set; }

		[Display(Name = "最後修改時間")]
		public DateTime? ModifiedTime { get; set; }
		[Display(Name = "最後修改者")]
		public string ModifiedBackendMemberName { get; set; }

		public int ModifiedBackendMemberId { get; set; }

	}
}