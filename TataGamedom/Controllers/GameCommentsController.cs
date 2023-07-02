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
		private AppDbContext db = new AppDbContext();
		// GET: GameComments
		public ActionResult Index(int? id)
		{
			IEnumerable<GameCommentIndexVM> comments = GetComments(id);
			return View(comments);
		}

		private IEnumerable<GameCommentIndexVM> GetComments(int? id)
		{
			IGameCommentRepository repo = new GameCommentRepository();
			GameCommentService service = new GameCommentService(repo);
			return service.Get(id);
		}
		[Authorize]
		public ActionResult Detail(int id)
		{
			var comment = GetCommentById(id);
			return View(comment);
		}

		private GameCommentDetailVM GetCommentById(int id)
		{
			IGameCommentRepository repo = new GameCommentRepository();
			GameCommentService service = new GameCommentService(repo);
			return service.GetCommentById(id);
		}
		[HttpPost]
		public ActionResult Delete(GameCommentDetailVM vm)
		{
			var currentUserAccount = User.Identity.Name;
			var memberInDb = db.BackendMembers.FirstOrDefault(m => m.Account == currentUserAccount);

			IGameCommentRepository repo = new GameCommentRepository();
			GameCommentService service = new GameCommentService(repo);
			vm.DeleteBackendMemberId = memberInDb.Id;
			var result = service.Delete(vm);
			if (result.IsFail)
			{
				return View("Error");
				//return HttpNotFound("刪除評論失敗");
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		public ActionResult Restore(GameCommentDetailVM vm)
		{
			var currentUserAccount = User.Identity.Name;
			var memberInDb = db.BackendMembers.FirstOrDefault(m => m.Account == currentUserAccount);

			IGameCommentRepository repo = new GameCommentRepository();
			GameCommentService service = new GameCommentService(repo);
			vm.DeleteBackendMemberId = memberInDb.Id;
			var result = service.Restore(vm);
			if (result.IsFail)
			{
				return View("Error");
				//return HttpNotFound("復原評論失敗");
			}
			return RedirectToAction("Index");
		}
	}
}