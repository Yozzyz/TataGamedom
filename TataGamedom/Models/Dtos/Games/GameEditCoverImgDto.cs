using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TataGamedom.Models.ViewModels.Games;

namespace TataGamedom.Models.Dtos.Games
{
	public class GameEditCoverImgDto
	{
		public int Id { get; set; }

		public string GameCoverImg { get; set; }

		public DateTime? ModifiedTime { get; set; }
		[Display(Name = "最後修改者")]

		public string ModifiedBackendMemberName { get; set; }
		public int ModifiedBackendMemberId { get; set; }
	}

	public static class GameEditCoverImgExts
	{
		public static GameEditCoverImgDto ToDto(this GameEditCoverImgVM vm)
		{
			return new GameEditCoverImgDto
			{
				GameCoverImg = vm.GameCoverImg,
				Id = vm.Id,
				ModifiedTime = DateTime.Now,
				ModifiedBackendMemberId = 1 //待Member寫好後修改
			};
		}
		public static GameEditCoverImgVM ToViewModel(this GameEditCoverImgDto dto)
		{
			return new GameEditCoverImgVM { GameCoverImg = dto.GameCoverImg, Id = dto.Id };
		}
	}
}