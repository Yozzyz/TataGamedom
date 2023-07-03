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

		public IEnumerable<GameCommentIndexVM> Get(int? id)
		{
			using (var conn = new SqlConnection(_connStr))
			{
				string sql = @"SELECT GC.Id, G.ChiName AS GameName, M.Name AS MemberName, GC.Content, GC.Score, GC.CreatedTime, GC.ActiveFlag, BM.Name AS DeleteBackendMemberName, GC.DeleteDatetime
FROM GameComments AS GC
JOIN Games AS G ON G.Id = GC.GameId
JOIN Members AS M ON M.Id = GC.MemberId
LEFT JOIN BackendMembers AS BM ON BM.Id = GC.DeleteBackendMemberId
WHERE (@Id IS NULL OR G.Id = @Id)";

				return conn.Query<GameCommentIndexVM>(sql, new { Id = id });
			}
		}

		public GameCommentDetailVM GetCommentById(int id)
		{
			using (var conn = new SqlConnection(_connStr))
			{
				string sql = @"SELECT GC.Id,G.ChiName AS GameName, M.Name AS MemberName,GC.ActiveFlag, GC.Content,GC.Score,GC.CreatedTime
FROM GameComments AS GC
JOIN Members AS M ON M.Id = GC.MemberId
JOIN Games AS G ON G.Id = GC.GameId
WHERE GC.Id=@Id";

				return conn.QueryFirstOrDefault<GameCommentDetailVM>(sql, new { Id = id });
			}
		}

		public bool Delete(GameCommentDetailVM vm)
		{
			using (var conn = new SqlConnection(_connStr))
			{
				string sql = @"UPDATE GameComments SET ActiveFlag = 0 , DeleteDateTime = GETDATE(), DeleteBackendMemberId = @DeleteBackendMemberId WHERE Id = @Id";
				var rowAffected = conn.Execute(sql, new { Id = vm.Id, DeleteBackendMemberId = vm.DeleteBackendMemberId });
				return rowAffected > 0;
			}
		}

		public bool Restore(GameCommentDetailVM vm)
		{
			using (var conn = new SqlConnection(_connStr))
			{
				string sql = @"UPDATE GameComments SET ActiveFlag = 1 , DeleteDateTime = GETDATE(), DeleteBackendMemberId = @DeleteBackendMemberId WHERE Id = @Id";
				var rowAffected = conn.Execute(sql, new { Id = vm.Id, DeleteBackendMemberId = vm.DeleteBackendMemberId });
				return rowAffected > 0;
			}
		}
	}
}