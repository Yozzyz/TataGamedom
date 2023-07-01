using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.Infra.DapperRepositories;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.Services;
using TataGamedom.Models.ViewModels.GameComments;

namespace TataGamedom.Controllers
{
    public class GameCommentsController : Controller
    {
        // GET: GameComments
        public ActionResult Index()
        {
            IEnumerable<GameCommentIndexVM> comments = GetComments();
            return View(comments);
        }

		private IEnumerable<GameCommentIndexVM> GetComments()
		{
			IGameCommentRepository repo = new GameCommentRepository();
            GameCommentService service = new GameCommentService(repo);
            return service.Get();
		}
	}
}