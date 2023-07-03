using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TataGamedom.Models.Infra;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.ViewModels.GameComments;

namespace TataGamedom.Models.Services
{
	public class GameCommentService
	{
		private IGameCommentRepository _repo;
		public GameCommentService(IGameCommentRepository repo)
		{
			_repo = repo;
		}

		public IEnumerable<GameCommentIndexVM> Get(int? id)
		{
			return _repo.Get(id);
		}

		public GameCommentDetailVM GetCommentById(int id)
		{
			var result = _repo.GetCommentById(id);
			if (result == null)
			{
				return null;
			}
			return result;
		}

		public Result Delete(GameCommentDetailVM vm)
		{
			var result = _repo.Delete(vm);
			if (!result)
			{
				return Result.Fail("刪除評論失敗");
			}
			return Result.Success();
		}

		public Result Restore(GameCommentDetailVM vm)
		{
			var result = _repo.Restore(vm);
			if (!result)
			{
				return Result.Fail("復原評論失敗");
			}
			return Result.Success();
		}
	}
}