using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TataGamedom.Models.EFModels;

namespace TataGamedom.Models.ViewModels.Games
{
	public class GameCreateVM
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

		public List<GameClassificationsCode> GameClassification { get; set; }
		[Display(Name = "遊戲類別（複選最多兩項）")]
		[Required(ErrorMessage = "請選擇遊戲分類")]
		public List<int> SelectedGameClassification { get; set; } = new List<int>();

		[Display(Name = "遊戲介紹")]
		[Required]
		[StringLength(1500)]
		public string Description { get; set; }
		[Display(Name = "年齡限制")]
		[Required]
		public bool IsRestrict { get; set; }

		[Display(Name = "封面圖片")]
		[StringLength(100)]
		public string GameCoverImg { get; set; }

		[Display(Name = "創建時間")]

		public DateTime CreatedTime { get; set; }
		[Display(Name = "創建者")]

		public int CreatedBackendMemberId { get; set; }
	}
}