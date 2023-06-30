using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using TataGamedom.Models.Dtos.Games;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.Infra;
using TataGamedom.Models.Infra.DapperRepositories;
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
			if (game != null)
			{
				if (!string.IsNullOrEmpty(game.SelectedGameClassificationString))
				{
					game.SelectedGameClassification = game.SelectedGameClassificationString
					?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
					.Select(int.Parse)
					.ToList();
				}
				else
				{
					game.SelectedGameClassification = new List<int>();
				}
			}
			else { return null; }
			return game;
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

		public Result CreateBoard(string name)
		{
			//用VM.chiname去搜尋遊戲
			//找到的話取得該遊戲ID
			//代入該遊戲資訊新增一筆Board
			var result = _repo.GetGameByName2(name);
			if (result == null)
			{
				return Result.Fail("討論版新增失敗");
			}
			var createBoard = _repo.CreateBoard(result);
			if (!createBoard)
			{
				return Result.Fail("討論版新增失敗");
			}
			return Result.Success();
		}

		public Result CreateClassification(GameCreateVM vm)
		{
			//用VM.chiname去搜尋遊戲
			//找到的話取得該遊戲ID
			//代入該遊戲資訊新增1/2筆資料至[GameClassificationGames]TABLE中
			var result = _repo.GetGameByName2(vm.ChiName);
			if (result == null)
			{
				return Result.Fail("遊戲類別新增失敗");
			}
			foreach (var classificationId in vm.SelectedGameClassification)
			{
				var createClassification = _repo.CreateClassification(result, classificationId);
				if (!createClassification)
				{
					return Result.Fail("遊戲類別新增失敗");
				}
			}
			return Result.Success();
		}

		public Result UpdateClassification(GameEditVM vm)
		{
			//將該遊戲已存在的類別全部刪除
			var games = _repo.GetGameClassificationGames(vm.Id);
			if (games != null)
			{
				var deleteResult = _repo.DeleteGameClassificationGames(vm.Id);
				if (!deleteResult)
				{
					return Result.Fail("更新失敗");
				}
			}
			//直接把新取得的遊戲類別值新增到TABLE中
			return Result.Success();
		}

		public Result CreateClassification(GameEditVM vm)
		{
			//用VM.chiname去搜尋遊戲
			//找到的話取得該遊戲ID
			//代入該遊戲資訊新增1/2筆資料至[GameClassificationGames]TABLE中
			var result = _repo.GetGameByName2(vm.ChiName);
			if (result == null)
			{
				return Result.Fail("遊戲類別新增失敗");
			}
			foreach (var classificationId in vm.SelectedGameClassification)
			{
				var createClassification = _repo.CreateClassification(result, classificationId);
				if (!createClassification)
				{
					return Result.Fail("遊戲類別新增失敗");
				}
			}
			return Result.Success();
		}
	}
}