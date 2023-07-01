using Dapper;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.ViewModels.News;
using PagedList;



namespace TataGamedom.Controllers
{
	public class NewsController : Controller
	{
		private string _connstr = System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ToString();
		private AppDbContext db = new AppDbContext();


		// GET: News

		[Authorize]
		public ActionResult Index(NewsCriteria newsCriteria, int? page)
		{
			PrepareNewsDataSource(newsCriteria.GamesId);
			ViewBag.NewsCriteria = newsCriteria;

			using (var con = new SqlConnection(_connstr))
			{
				string sql = @"SELECT n.Id, n.Title, n.ScheduleDate, b.Name AS BackendMemberName, gc.Name AS GameClassificationName,
            COUNT(nv.MemberId) AS ViewCount, COUNT(nl.MemberId) AS LikeCount, n.ActiveFlag
            FROM news AS n
            JOIN BackendMembers AS b ON b.Id = n.BackendMemberId 
            LEFT JOIN GameClassificationsCodes AS gc ON gc.Id = n.GamesId
            LEFT JOIN NewsViews AS nv ON nv.NewsId = n.Id
            LEFT JOIN NewsLikes AS nl ON nl.NewsId = n.Id
            WHERE 1 = 1";

				// 動態建立條件參數
				DynamicParameters parameters = new DynamicParameters();

				if (newsCriteria.GamesId.HasValue && newsCriteria.GamesId.Value != 0)
				{
					if (newsCriteria.GamesId.Value == 1)
					{
						// 當 GamesId 為 1 時不加入篩選條件，列出全部
					}
					else
					{
						sql += " AND n.GamesId = @GamesId";
						parameters.Add("@GamesId", newsCriteria.GamesId.Value);
					}
				}

				// 如果有其他搜尋條件，請繼續在此處新增條件
				if (!string.IsNullOrEmpty(newsCriteria.Title))
				{
					sql += " AND n.Title LIKE @Title";
					parameters.Add("@Title", "%" + newsCriteria.Title + "%");
				}

				sql += @" GROUP BY n.Id, n.Title, n.ScheduleDate, b.Name, gc.Name, n.ActiveFlag
            ORDER BY n.Id DESC";

				var news = con.Query<NewsIndexVM>(sql, parameters);

				// 取得當前頁數
				int pageNumber = page ?? 1;

				// 每頁顯示的項目數量
				int pageSize = 10;

				// 將資料列表轉換為分頁結果
				var pagedNews = news.ToPagedList(pageNumber, pageSize);

				return View(pagedNews);
			}
		}


		public ActionResult Create()
		{
			ViewBag.BackendMemberId = new SelectList(db.BackendMembers, "Id", "Name");
			//ViewBag.DeleteBackendMemberId = new SelectList(db.BackendMembers, "Id", "Name");
			ViewBag.GamesId = new SelectList(db.GameClassificationsCodes, "Id", "Name");
			ViewBag.NewsCategoryId = new SelectList(db.NewsCategoryCodes, "Id", "Name");
			return View();
		}


		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(NewsCreateVM newsCreateVM)
		{
			if (ModelState.IsValid)
			{
				DateTime now = DateTime.Now;

				if (newsCreateVM.ScheduleDate <= now)
				{
					newsCreateVM.ActiveFlag = true;
				}
				else
				{
					newsCreateVM.ActiveFlag = false;
				}

				// 取得管理者的BackendMember ID
				var currentUserAccount = User.Identity.Name;
				var backendMember = db.BackendMembers.FirstOrDefault(m => m.Account == currentUserAccount);

				if (backendMember != null)
				{
					newsCreateVM.BackendMemberId = backendMember.Id;

					using (var con = new SqlConnection(_connstr))
					{
						string sql = @"INSERT INTO News (Title, Content, BackendMemberId, NewsCategoryId, GamesId, CoverImg, ScheduleDate, ActiveFlag)
                               VALUES (@Title, @Content, @BackendMemberId, @NewsCategoryId, @GamesId, @CoverImg, @ScheduleDate, @ActiveFlag);
                               SELECT CAST(SCOPE_IDENTITY() AS INT)";

						var id = con.Query<int>(sql, newsCreateVM).Single();
						newsCreateVM.Id = id;
					}

					return RedirectToAction("Index");
				}
			}

			ViewBag.BackendMemberId = new SelectList(db.BackendMembers, "Id", "Name");
			ViewBag.GamesId = new SelectList(db.GameClassificationsCodes, "Id", "Name");
			ViewBag.NewsCategoryId = new SelectList(db.NewsCategoryCodes, "Id", "Name");

			return View(newsCreateVM);
		}


		// GET: News/Edit
		[Authorize]
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			using (var con = new SqlConnection(_connstr))
			{
				string sql = @"SELECT n.Id, n.Title, n.Content, n.BackendMemberId, n.NewsCategoryId, n.GamesId, n.CoverImg, 
                   n.ScheduleDate, n.ActiveFlag, n.DeleteDatetime, n.DeleteBackendMemberId,
                   b.Name AS BackendMemberName, gc.Name AS GameClassificationName
                   FROM News AS n
                   JOIN BackendMembers AS b ON b.Id = n.BackendMemberId
                   LEFT JOIN GameClassificationsCodes AS gc ON gc.Id = n.GamesId
                   WHERE n.Id = @Id";

				var news = con.Query<NewsEditVM>(sql, new { Id = id }).SingleOrDefault();

				if (news == null)
				{
					return HttpNotFound();
				}


				ViewBag.BackendMemberId = new SelectList(db.BackendMembers, "Id", "Name", news.BackendMemberId);
				ViewBag.DeleteBackendMemberId = new SelectList(db.BackendMembers, "Id", "Name", news.DeleteBackendMemberId);
				ViewBag.GamesId = new SelectList(db.GameClassificationsCodes, "Id", "Name", news.GamesId);
				ViewBag.NewsCategoryId = new SelectList(db.NewsCategoryCodes, "Id", "Name", news.NewsCategoryId);

				return View(news);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(NewsEditVM news)
		{
			if (ModelState.IsValid)
			{

				// 取得管理者的BackendMember ID
				var currentUserAccount = User.Identity.Name;
				var backendMember = db.BackendMembers.FirstOrDefault(m => m.Account == currentUserAccount);

				if (backendMember != null)
				{
					news.BackendMemberId = backendMember.Id;
					using (var con = new SqlConnection(_connstr))
					{
						string sql = @"UPDATE News SET Title = @Title, Content = @Content, BackendMemberId = @BackendMemberId,
                NewsCategoryId = @NewsCategoryId, GamesId = @GamesId, CoverImg = @CoverImg,
                ScheduleDate = @ScheduleDate, ActiveFlag = @ActiveFlag, DeleteDatetime = @DeleteDatetime,
                DeleteBackendMemberId = @DeleteBackendMemberId
                WHERE Id = @Id";

						con.Execute(sql, news);

						return RedirectToAction("Index");
					}
				}
			}

			ViewBag.BackendMemberId = new SelectList(db.BackendMembers, "Id", "Name", news.BackendMemberId);
			ViewBag.DeleteBackendMemberId = new SelectList(db.BackendMembers, "Id", "Name", news.DeleteBackendMemberId);
			//ViewBag.GamesId = new SelectList(db.Games, "Id", "ChiName", news.GamesId);
			ViewBag.NewsCategoryId = new SelectList(db.NewsCategoryCodes, "Id", "Name", news.NewsCategoryId);

			return View(news);
		}


		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			News news = db.News.Find(id);
			if (news == null)
			{
				return HttpNotFound();
			}
			return View(news);
		}


		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			var currentUserAccount = User.Identity.Name;
			var backendMember = db.BackendMembers.FirstOrDefault(m => m.Account == currentUserAccount);

			if (backendMember != null)
			{
				using (var con = new SqlConnection(_connstr))
				{
					string sql = @"UPDATE News SET ActiveFlag = 0, DeleteDatetime = GETDATE(), DeleteBackendMemberId = @BackendMemberId WHERE Id = @Id";

					con.Execute(sql, new { BackendMemberId = backendMember.Id, Id = id });
				}
			}
			return RedirectToAction("Index");
		}


		public ActionResult Reduction(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			News news = db.News.Find(id);
			if (news == null)
			{
				return HttpNotFound();
			}
			return View(news);
		}

		[HttpPost, ActionName("Reduction")]
		[ValidateAntiForgeryToken]
		public ActionResult reductionConfirmed(int id)
		{
			var currentUserAccount = User.Identity.Name;
			var backendMember = db.BackendMembers.FirstOrDefault(m => m.Account == currentUserAccount);

			if (backendMember != null)
			{
				using (var con = new SqlConnection(_connstr))
				{
					string sql = @"UPDATE News SET ActiveFlag = 1, DeleteDatetime = NULL, DeleteBackendMemberId = NULL WHERE Id = @Id";

					con.Execute(sql, new { BackendMemberId = backendMember.Id, Id = id });
				}
			}
			return RedirectToAction("Index");
		}

		public ActionResult Show(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			using (var con = new SqlConnection(_connstr))
			{
				string sql = @"SELECT n.Id, n.Title, n.Content, n.BackendMemberId, n.NewsCategoryId, n.GamesId, n.CoverImg, 
                   n.ScheduleDate, n.ActiveFlag, n.DeleteDatetime, n.DeleteBackendMemberId,
                   b.Name AS BackendMemberName, gc.Name AS GameClassificationName
                   FROM News AS n
                   JOIN BackendMembers AS b ON b.Id = n.BackendMemberId
                   LEFT JOIN GameClassificationsCodes AS gc ON gc.Id = n.GamesId
                   WHERE n.Id = @Id";

				var news = con.Query<NewsEditVM>(sql, new { Id = id }).SingleOrDefault();

				if (news == null)
				{
					return HttpNotFound();
				}

				return View(news);
			}
		}

		private void PrepareNewsDataSource(int? gamesId)
		{

			var categories = db.GameClassificationsCodes.ToList().Prepend(new GameClassificationsCode());
			ViewBag.GamesId = new SelectList(categories, "Id", "Name", gamesId);
			//ViewBag.GamesId = new SelectList(
			//	db.GameClassificationsCodes
			//	.ToList()
			//	.Prepend(new GameClassificationsCode { Id = 0, Name=""  }), "Id", "Name", gamesId
			//	);
		}
	}
}