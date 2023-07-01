using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.ViewModels.GameComments;

namespace TataGamedom.Models.Infra.DapperRepositories
{
	public class GameCommentRepository : IGameCommentRepository
	{
		private string _connStr;
		public GameCommentRepository()
		{
			_connStr = System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ToString();
		}

		public IEnumerable<GameCommentIndexVM> Get()
		{
			using (var conn = new SqlConnection(_connStr))
			{
				string sql = @"SELECT GC.Id,G.ChiName AS GameName,M.Name AS MemberName,GC.Content,GC.Score,GC.CreatedTime,GC.ActiveFlag,BM.Name AS DeleteBackendMemberName,GC.DeleteDatetime
FROM GameComments AS GC
JOIN Games AS G ON G.Id = GC.GameId
JOIN Members AS M ON M.Id = GC.MemberId
LEFT JOIN BackendMembers AS BM ON BM.Id = GC.DeleteBackendMemberId;
";
				return conn.Query<GameCommentIndexVM>(sql);
			}
		}
	}
}