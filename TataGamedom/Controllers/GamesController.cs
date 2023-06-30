using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.Infra;
using TataGamedom.Models.Infra.DapperRepositories;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.Services;
using TataGamedom.Models.ViewModels.Games;

namespace TataGamedom.Controllers
{
	public class GamesController : Controller
	{
		private AppDbContext db = new AppDbContext();
		// GET: Games
		[Authorize]
		public ActionResult Index()
		{
			IEnumerable<GameIndexVM> games = GetGames();
			return View(games);
		}

		private IEnumerable<GameIndexVM> GetGames()
		{
			IGameRepository repo = new GameDapperRepository();
			GameService service = new GameService(repo);
			return service.Search();
		}
		[Authorize]
		public ActionResult Create()
		{
			var gameClassifications = GetGameClassifications();
			GameCreateVM model = new GameCreateVM
			{
				GameClassification = gameClassifications
			};
			return View(model);
		}

		private List<GameClassificationsCode> GetGameClassifications()
		{
			IGameRepository repo = new GameDapperRepository();
			GameService service = new GameService(repo);
			return service.GetGameClassifications();
		}
		
		[HttpPost]
		public ActionResult Create(GameCreateVM vm, HttpPostedFileBase file1)
		{
			var currentUserAccount = User.Identity.Name;
			var memberInDb = db.BackendMembers.FirstOrDefault(m => m.Account == currentUserAccount);

			var savedFileName = SaveFile(file1);
			if (savedFileName == null)
			{
				ModelState.AddModelError("GameCoverImg", "請選擇檔案");
				return View(vm);
			}
			vm.GameCoverImg = savedFileName;
			vm.CreatedBackendMemberId = memberInDb.Id;
			if (ModelState.IsValid)
			{
				List<int> selectedGameClassifications = vm.SelectedGameClassification;
				if (selectedGameClassifications.Count > 2)
				{
					ModelState.AddModelError("SelectedGameClassification", "最多只能選擇兩個遊戲分類！");
					List<GameClassificationsCode> gameClassifications = GetGameClassifications();
					GameCreateVM model = new GameCreateVM
					{
						GameClassification = gameClassifications
					};
					return View(model);

				}
				Result createResult = CreateGame(vm);
				if (createResult.IsSuccess)
				{
					//新增遊戲類別
					CreateGameClassification(vm);
					//新增遊戲討論版
					CreateGameBoard(vm);

					return RedirectToAction("Index");
				}
				ModelState.AddModelError(string.Empty, createResult.ErrorMessage);
				return View(vm);
			}
			return View(vm);
		}

		private void CreateGameBoard(GameCreateVM vm)
		{
			IGameRepository repo = new GameDapperRepository();
			GameService service = new GameService(repo);
			service.CreateBoard(vm.ChiName);
		}

		private void CreateGameClassification(GameCreateVM vm)
		{
			IGameRepository repo = new GameDapperRepository();
			GameService service = new GameService(repo);
			service.CreateClassification(vm);
		}

		private Result CreateGame(GameCreateVM vm)
		{
			IGameRepository repo = new GameDapperRepository();
			GameService service = new GameService(repo);
			return service.CreateGame(vm);
		}

		private string SaveFile(HttpPostedFileBase file1)
		{
			var path = Server.MapPath("~/Files/Uploads");

			var helper = new UploadFileHelper(new GuidRenameProvider(),
											new RequiredValidator(), // 必需上傳檔案
											new ImageValidator(), // 必需是圖片檔案
											new FileSizeValidator(1920 * 1920) // 必需小於1MB		
											);
			var fileName = helper.SaveFile(file1, path);

			return fileName ?? string.Empty;
		}
		[Authorize]
		public ActionResult Edit(int id)
		{
			var gameClassifications = GetGameClassifications();
			IGameRepository repo = new GameDapperRepository();
			GameService service = new GameService(repo);
			var game = service.GetGameById(id);
			GameEditVM model = new GameEditVM
			{
				GameClassification = gameClassifications,
				SelectedGameClassification = game.SelectedGameClassification,
				Id = game.Id,
				ChiName = game.ChiName,
				EngName = game.EngName,
				Description = game.Description,
				IsRestrict = game.IsRestrict,
				ModifiedTime = game.ModifiedTime,
				ModifiedBackendMemberName = game.ModifiedBackendMemberName,
				ModifiedBackendMemberId = game.ModifiedBackendMemberId
			};
			return View(model);
		}
		[HttpPost]
		public ActionResult Edit(GameEditVM vm)
		{
			if (ModelState.IsValid)
			{
				var currentUserAccount = User.Identity.Name;
				var memberInDb = db.BackendMembers.FirstOrDefault(m => m.Account == currentUserAccount);

				List<int> selectedGameClassifications = vm.SelectedGameClassification;
				if (selectedGameClassifications.Count > 2)
				{
					ModelState.AddModelError("SelectedGameClassification", "最多只能選擇兩個遊戲分類！");
					List<GameClassificationsCode> gameClassifications = GetGameClassifications();
					GameEditVM model = new GameEditVM
					{
						GameClassification = gameClassifications
					};
					return View(model);
				}
				vm.ModifiedBackendMemberId =memberInDb.Id;
				Result editResult = UpdateGames(vm);
				if (editResult.IsSuccess)
				{
					//更新遊戲類別
					UpdateGameClassification(vm);
					CreateGameClassification(vm);


					return RedirectToAction("Index");
				}
				ModelState.AddModelError(string.Empty, editResult.ErrorMessage);
				return View(vm);
			}
			return View(vm);
		}
		private void CreateGameClassification(GameEditVM vm)
		{
			IGameRepository repo = new GameDapperRepository();
			GameService service = new GameService(repo);
			service.CreateClassification(vm);
		}
		private void UpdateGameClassification(GameEditVM vm)
		{
			IGameRepository repo = new GameDapperRepository();
			GameService service = new GameService(repo);
			service.UpdateClassification(vm);
		}

		private Result UpdateGames(GameEditVM vm)
		{
			IGameRepository repo = new GameDapperRepository();
			GameService service = new GameService(repo);
			return service.UpdateGame(vm);
		}
		[Authorize]
		public ActionResult EditGameCover(int id)
		{
			IGameRepository repo = new GameDapperRepository();
			GameService service = new GameService(repo);
			var game = service.GetGameById2(id);
			return View(game);
		}

		[HttpPost]
		public ActionResult EditGameCover(GameEditCoverImgVM vm, HttpPostedFileBase file1)
		{
			if (file1 != null) // 檢查是否選擇了新的檔案
			{
				var savedFileName = SaveFile(file1);
				if (savedFileName == null)
				{
					ModelState.AddModelError("CoverImg", "請選擇檔案");
					return View(vm);
				}
				vm.GameCoverImg = savedFileName;
			}
			if (ModelState.IsValid)
			{
				var currentUserAccount = User.Identity.Name;
				var memberInDb = db.BackendMembers.FirstOrDefault(m => m.Account == currentUserAccount);
				vm.ModifiedBackendMemberId= memberInDb.Id;
				IGameRepository repo = new GameDapperRepository();
				GameService service = new GameService(repo);
				service.EditGameCover(vm);
				return RedirectToAction("Index");
			}
			return View(vm);
		}
		public ActionResult CreateProduct()
		{
			return View();
		}
	}
}