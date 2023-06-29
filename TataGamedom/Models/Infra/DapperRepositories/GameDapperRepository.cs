using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TataGamedom.Models.Dtos.Games;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.Interfaces;
using Dapper;

namespace TataGamedom.Models.Infra.DapperRepositories
{
	public class GameDapperRepository : IGameRepository
	{
		private string _connStr;
		public GameDapperRepository()
		{
			_connStr = System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ToString();
		}

		public bool Create(GameCreateDto dto)
		{
			using (var conn = new SqlConnection(_connStr))
			{
				string sql = @"INSERT INTO Games(ChiName,EngName,Description,IsRestrict
  ,GameCoverImg,CreatedTime,CreatedBackendMemberId)
  VALUES(@ChiName,@EngName,@Description,@IsRestrict
  ,@GameCoverImg,@CreatedTime,@CreatedBackendMemberId)";

				var rowAffected = conn.Execute(sql, dto);
				return rowAffected > 0;
			}
		}

		public List<GameClassificationsCode> GetGameClassifications()
		{
			using (var conn = new SqlConnection(_connStr))
			{
				string sql = @"SELECT * FROM GameClassificationsCodes";
				return conn.Query<GameClassificationsCode>(sql).ToList();
			}
		}

		public Game GetGameById(int id)
		{
			using (var conn = new SqlConnection(_connStr))
			{
				//string sql = @"SELECT ChiName,EngName,Description,IsRestrict,ModifiedTime,BM.Name AS ModifiedBackendMemberName
				//FROM Games AS G
				//LEFT JOIN BackendMembers AS BM ON BM.Id=G.ModifiedBackendMemberId
				//WHERE G.Id = @Id";

				string sql = @"SELECT ChiName, EngName, STRING_AGG(GCC.Name, ' , ') AS Classification,Description, G.IsRestrict AS IsRestrict,BM.Name AS ModifiedBackendMemberName, G.ModifiedTime AS ModifiedTime
FROM Games AS G
LEFT JOIN GameClassificationGames AS GCG ON GCG.GameId = G.Id
LEFT JOIN GameClassificationsCodes AS GCC ON GCC.Id = GCG.GameClassificationId
LEFT JOIN BackendMembers AS BM ON BM.Id = G.ModifiedBackendMemberId
WHERE G.Id = @Id
GROUP BY G.Id, G.ChiName,G.EngName,G.Description, G.IsRestrict, G.ModifiedTime, BM.Name
";
				return conn.QueryFirstOrDefault<Game>(sql, new { Id = id });
			}
		}

		public GameCreateDto GetGameByName(string chi, string eng)
		{
			using (var conn = new SqlConnection(_connStr))
			{
				string sql = @"SELECT * FROM Games WHERE ChiName=@ChiName OR EngName=@EngName";
				return conn.QueryFirstOrDefault<GameCreateDto>(sql, new { ChiName = chi, EngName = eng });
			}
		}

		public IEnumerable<GameIndexDto> Search()
		{
			using (var conn = new SqlConnection(_connStr))
			{
				string sql = @"SELECT G.Id AS Id, G.ChiName AS ChiName, STRING_AGG(GCC.Name, ' , ') AS Classification, G.IsRestrict AS IsRestrict,BM.Name AS CreatedBackendMemberName, CONVERT(date,G.CreatedTime) AS CreatedTime
FROM Games AS G
LEFT JOIN GameClassificationGames AS GCG ON GCG.GameId = G.Id
LEFT JOIN GameClassificationsCodes AS GCC ON GCC.Id = GCG.GameClassificationId
JOIN BackendMembers AS BM ON BM.Id = G.CreatedBackendMemberId
GROUP BY G.Id, G.ChiName, G.IsRestrict, G.CreatedTime, BM.Name";

				return conn.Query<GameIndexDto>(sql);
			};
		}

		public bool UpddateGame(GameEditDto dto)
		{
			using (var conn = new SqlConnection(_connStr))
			{
				string sql = @"UPDATE Games SET ChiName = @ChiName,EngName=@EngName,Description=@Description,IsRestrict=@IsRestrict,ModifiedTime=@ModifiedTime,ModifiedBackendMemberId=@ModifiedBackendMemberId WHERE Id=@id";
				var rowAffected = conn.Execute(sql, dto);
				return rowAffected > 0;
			}
		}

		public GameEditCoverImgDto GetGameById2(int id)
		{
			using (var conn = new SqlConnection(_connStr))
			{
				string sql = @"SELECT * FROM Games WHERE Id=@id";
				return conn.QueryFirstOrDefault<GameEditCoverImgDto>(sql, new { Id = id });
			}
		}

		public bool EditGameCover(GameEditCoverImgDto dto)
		{
			using (var conn = new SqlConnection(_connStr))
			{
				string sql = @"UPDATE Games SET GameCoverImg = @GameCoverImg,ModifiedTime=@ModifiedTime, ModifiedBackendMemberId=@ModifiedBackendMemberId  WHERE Id = @Id";
				var rowAffected = conn.Execute(sql, dto);
				return rowAffected > 0;
			}
		}
	}
}