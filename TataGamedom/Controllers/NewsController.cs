using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Dapper;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.ViewModels.News;

namespace TataGamedom.Controllers
{
	public class NewsController : Controller
	{
		private string _connstr = System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ToString();
		private AppDbContext db = new AppDbContext();


		// GET: News
		[Authorize]
		public ActionResult Index()
		{
			using (var con = new SqlConnection(_connstr))
			{

				string sql = @"SELECT n.Id,n.Title,n.ScheduleDate, b.Name AS BackendMemberName, gc.Name AS GameClassificationName,
			COUNT(nv.MemberId) AS ViewCount, COUNT(nl.MemberId) AS LikeCount, n.ActiveFlag
			FROM news AS n
			JOIN BackendMembers AS b ON b.Id = n.BackendMemberId 
			JOIN GameClassificationsCodes AS gc ON gc.Id = n.GamesId
			LEFT JOIN NewsViews AS nv ON nv.NewsId = n.Id
			LEFT JOIN NewsLikes AS nl ON nl.NewsId = n.Id
			GROUP BY n.Id,n.Title,n.ScheduleDate, b.Name, gc.Name, n.ActiveFlag
			ORDER BY N.ScheduleDate DESC";

				var gameOptions = db.GameClassificationsCodes
	.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name })
	.ToList();

				// 將遊戲類別選項清單傳遞到視圖
				ViewBag.GameOptions = gameOptions;

				var news = con.Query<NewsIndexVM>(sql);

				return View(news);
			}
		}

		public ActionResult Create()
		{
			ViewBag.BackendMemberId = new SelectList(db.BackendMembers, "Id", "Name");
			//ViewBag.DeleteBackendMemberId = new SelectList(db.BackendMembers, "Id", "Name");
			// ViewBag.GamesId = new SelectList(db.Games, "Id", "ChiName");
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

			string sql = @"SELECT n.Id, n.Title, n.Content, n.BackendMemberId, n.NewsCategoryId, n.GamesId, n.CoverImg, 
                   n.ScheduleDate, n.ActiveFlag, n.DeleteDatetime, n.DeleteBackendMemberId,
                   b.Name AS BackendMemberName, gc.Name AS GameClassificationName
                   FROM News AS n
                   JOIN BackendMembers AS b ON b.Id = n.BackendMemberId
                   JOIN GameClassificationsCodes AS gc ON gc.Id = n.GamesId
                   WHERE n.Id = @Id";

			using (var con = new SqlConnection(_connstr))
			{
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
				string sql = @"UPDATE News SET Title = @Title, Content = @Content, BackendMemberId = @BackendMemberId,
                NewsCategoryId = @NewsCategoryId, GamesId = @GamesId, CoverImg = @CoverImg,
                ScheduleDate = @ScheduleDate, ActiveFlag = @ActiveFlag, DeleteDatetime = @DeleteDatetime,
                DeleteBackendMemberId = @DeleteBackendMemberId
                WHERE Id = @Id";

				using (var con = new SqlConnection(_connstr))
				{
					con.Execute(sql, news);

					return RedirectToAction("Index");
				}
			}

			ViewBag.BackendMemberId = new SelectList(db.BackendMembers, "Id", "Name", news.BackendMemberId);
			ViewBag.DeleteBackendMemberId = new SelectList(db.BackendMembers, "Id", "Name", news.DeleteBackendMemberId);
			ViewBag.GamesId = new SelectList(db.Games, "Id", "ChiName", news.GamesId);
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

		// POST: News/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			using (var con = new SqlConnection(_connstr))
			{
				string sql = @"UPDATE News SET ActiveFlag = 0 WHERE Id = @Id";

				con.Execute(sql, new { Id = id });
			}

			return RedirectToAction("Index");
		}
	}
}