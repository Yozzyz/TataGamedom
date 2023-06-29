using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.ViewModels.Games;

namespace TataGamedom.Models.Dtos.Games
{
	public class GameEditDto
	{
		public int Id { get; set; }

		[Display(Name = "中文名稱")]

		public string ChiName { get; set; }

		[Display(Name = "英文名稱")]

		public string EngName { get; set; }
		public List<GameClassificationsCode> GameClassification { get; set; }

		//輔助List<int> SelectedGameClassification用，先將DB中的資料用string接出來，再轉成int
		public string SelectedGameClassificationString { get; set; }
		[Display(Name = "遊戲類別（複選最多兩項）")]
		[Required(ErrorMessage = "請選擇遊戲分類")]
		public List<int> SelectedGameClassification { get; set; }

		[Display(Name = "遊戲介紹")]
		public string Description { get; set; }
		[Display(Name = "年齡限制")]

		public bool IsRestrict { get; set; }

		[Display(Name = "最後修改時間")]

		public DateTime? ModifiedTime { get; set; }
		[Display(Name = "最後修改者")]

		public string ModifiedBackendMemberName { get; set; }
		public int ModifiedBackendMemberId { get; set; }

	}

	public static class GameEditExts
	{
		public static GameEditDto ToDto(this GameEditVM vm)
		{
			return new GameEditDto
			{
				Id = vm.Id,
				ChiName = vm.ChiName,
				EngName = vm.EngName,
				SelectedGameClassification = vm.SelectedGameClassification,
				Description = vm.Description,
				IsRestrict = vm.IsRestrict,
				ModifiedTime = DateTime.Now,
				ModifiedBackendMemberId = 1 //待member功能做好再修改
			};
		}

		public static GameEditVM ToViewModel(this GameEditDto dto)
		{
			return new GameEditVM
			{
				Id = dto.Id,
				ChiName = dto.ChiName,
				EngName = dto.EngName,
				SelectedGameClassification = dto.SelectedGameClassification,
				Description = dto.Description,
				IsRestrict = dto.IsRestrict,
				ModifiedTime = dto.ModifiedTime,
				ModifiedBackendMemberName = dto.ModifiedBackendMemberName,
				ModifiedBackendMemberId = dto.ModifiedBackendMemberId
			};
		}
	}
}