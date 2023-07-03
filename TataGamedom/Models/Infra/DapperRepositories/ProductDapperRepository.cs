using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.ViewModels.Products;

namespace TataGamedom.Models.Infra.DapperRepositories
{
	public class ProductDapperRepository : IProductRepository
	{
		private string _connStr;
		public ProductDapperRepository()
		{
			_connStr = System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ToString();
		}

		public IEnumerable<ProductIndexVM> Get()
		{
			using (var conn = new SqlConnection(_connStr))
			{
				string sql = @"SELECT P.Id,P.[Index] AS [Index],G.ChiName AS GameName ,GPC.Name AS GamePlatformName,P.IsVirtual AS IsVirtual,P.Price AS Price,CONVERT(DATE,P.SaleDate)AS SaleDate,PSC.Name AS ProductStatus,BM.Name AS CreatedBackendMemberName,P.CreatedTime AS CreatedTime
FROM Products AS P
JOIN Games AS G ON P.GameId=G.Id
JOIN GamePlatformsCodes AS GPC ON GPC.Id = P.GamePlatformId
JOIN ProductStatusCodes AS PSC ON PSC.Id = P.ProductStatusId
JOIN BackendMembers AS BM ON BM.Id = P.CreatedBackendMemberId
ORDER BY P.CreatedTime desc";
				return conn.Query<ProductIndexVM>(sql);
			}
		}

		public ProductEditVM GetGameById(int id)
		{
			using (var conn = new SqlConnection(_connStr))
			{
				string sql = @"select P.Id,P.[Index],G.ChiName AS GameName,G.Description,GPC.Id AS GamePlatform,P.IsVirtual,P.Price,P.SystemRequire,P.SaleDate,PSC.Id AS ProductStatus,BM.Name AS ModifiedBackendMemberName,P.ModifiedTime
from Products AS P
JOIN Games AS G ON G.Id = P.GameId
JOIN GamePlatformsCodes AS GPC ON GPC.Id = P.GamePlatformId
LEFT JOIN BackendMembers AS BM ON BM.Id = P.ModifiedBackendMemberId
JOIN ProductStatusCodes AS PSC ON PSC.Id = P.ProductStatusId
WHERE P.Id =@Id";
				return conn.QueryFirstOrDefault<ProductEditVM>(sql, new { Id = id });
			}
		}
	}
}