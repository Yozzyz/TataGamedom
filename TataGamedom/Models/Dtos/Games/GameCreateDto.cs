using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TataGamedom.Models.ViewModels.Games;

namespace TataGamedom.Models.Dtos.Games
{
	public class GameCreateDto
	{
		public int Id { get; set; }

		[Display(Name = "中文名稱")]
		public string ChiName { get; set; }

		[Display(Name = "英文名稱")]
		public string EngName { get; set; }
		public List<int> SelectedGameClassification { get; set; }

		[Display(Name = "遊戲介紹")]
		public string Description { get; set; }
		[Display(Name = "年齡限制")]
		public bool IsRestrict { get; set; }

		[Display(Name = "封面圖片")]
		public string GameCoverImg { get; set; }

		[Display(Name = "創建時間")]
		public DateTime CreatedTime { get; set; }
		[Display(Name = "創建者")]

		public int CreatedBackendMemberId { get; set; }
	}

	public static class GameCreateExts
	{
		public static GameCreateDto ToDto(this GameCreateVM vm)
		{
			return new GameCreateDto
			{
				Id = vm.Id,
				ChiName = vm.ChiName,
				EngName = vm.EngName,
				SelectedGameClassification = vm.SelectedGameClassification,
				Description = vm.Description,
				IsRestrict = vm.IsRestrict,
				GameCoverImg = vm.GameCoverImg,
				CreatedTime = DateTime.Now,
				CreatedBackendMemberId = 1//待Member做好再改
			};
		}
	}
}