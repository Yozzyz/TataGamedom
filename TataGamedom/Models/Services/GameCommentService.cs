using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

		public IEnumerable<GameCommentIndexVM> Get()
		{
			return _repo.Get();
		}
	}
}