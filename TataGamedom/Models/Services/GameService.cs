using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TataGamedom.Models.Dtos.Games;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.Infra;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.ViewModels.Games;

namespace TataGamedom.Models.Services
{
	public class GameService
	{
		private IGameRepository _repo;
		public GameService(IGameRepository repo)
		{
			_repo = repo;
		}

		public IEnumerable<GameIndexVM> Search()
		{
			return _repo.Search()
				.Select(dto => new GameIndexVM
				{
					Id = dto.Id,
					ChiName = dto.ChiName,
					Classification = dto.Classification,
					IsRestrict = dto.IsRestrict,
					CreatedBackendMemberName = dto.CreatedBackendMemberName,
					CreatedTime = dto.CreatedTime,
				});
		}

		public GameEditVM GetGameById(int id)
		{
			var game = _repo.GetGameById(id);
			if (game == null) { return null; }
			var test = new GameEditDto
			{
				Id = game.Id,
				ChiName = game.ChiName,
				EngName = game.EngName,
				Description = game.Description,
				IsRestrict = game.IsRestrict,
				ModifiedTime = game.ModifiedTime,
				ModifiedBackendMemberId = 1 //之後要改
			};
			return test.ToViewModel();
		}

		public Result UpdateGame(GameEditVM vm)
		{
			var dto = vm.ToDto();
			var updateResult = _repo.UpddateGame(dto);
			if (!updateResult)
			{
				return Result.Fail("更新失敗");
			}
			return Result.Success();
		}

		public Result CreateGame(GameCreateVM vm)
		{
			var dto = vm.ToDto();
			var game = _repo.GetGameByName(dto.ChiName, dto.EngName);
			if (game != null)
			{
				if (dto.ChiName == game.ChiName || dto.EngName == game.EngName)
				{
					return Result.Fail("該遊戲已存在！");
				}
			}

			var createResult = _repo.Create(dto);
			if (!createResult)
			{
				return Result.Fail("新增遊戲失敗！");
			}
			return Result.Success();
		}

		public List<GameClassificationsCode> GetGameClassifications()
		{
			return _repo.GetGameClassifications();
		}

		public GameEditCoverImgVM GetGameById2(int id)
		{
			var game = _repo.GetGameById2(id);
			if (game == null) { return null; }
			return game.ToViewModel();
		}

		public Result EditGameCover(GameEditCoverImgVM vm)
		{
			var editCover = vm.ToDto();
			var editCoverResult = _repo.EditGameCover(editCover);
			if (!editCoverResult)
			{
				return Result.Fail("遊戲封面更新失敗！");
			}
			return Result.Success();
		}
	}
}