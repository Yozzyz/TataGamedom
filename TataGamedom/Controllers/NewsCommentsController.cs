using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.ViewModels.News;
using Dapper;

namespace TataGamedom.Controllers
{
	public class NewsCommentsController : Controller
	{
		private string _connstr = System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ToString();
		private AppDbContext db = new AppDbContext();

		// GET: NewsComments
		[Authorize]
		public ActionResult Index(int? newsId)
		{
			using (var con = new SqlConnection(_connstr))
			{
				string sql = @"SELECT M.Name as MemberName, NC.Time, NC.Content, NC.ActiveFlag, N.Title as Title
FROM NewsComments AS NC
JOIN Members AS M ON M.Id = NC.MemberID
JOIN News AS N ON N.Id = NC.NewsId
";

				if (newsId.HasValue)
				{
					sql += " WHERE N.Id = @NewsId";
					var newsComments = con.Query<NewsCommentIndexVM>(sql, new { NewsId = newsId.Value });
					return View(newsComments);
				}
				else
				{
					var newsComments = con.Query<NewsCommentIndexVM>(sql);
					return View(newsComments);
				}
			}
		}

	}
}