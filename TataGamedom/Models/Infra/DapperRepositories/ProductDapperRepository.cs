using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
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
			using(var conn = new SqlConnection(_connStr))
			{
				string sql = @"SELECT P.[Index] AS [Index],G.ChiName AS GameName ,GPC.Name AS GamePlatformName,P.IsVirtual AS IsVirtual,P.Price AS Price,CONVERT(DATE,P.SaleDate)AS SaleDate,PSC.Name AS ProductStatus,BM.Name AS CreatedBackendMemberName,P.CreatedTime AS CreatedTime
FROM Products AS P
JOIN Games AS G ON P.GameId=G.Id
JOIN GamePlatformsCodes AS GPC ON GPC.Id = P.GamePlatformId
JOIN ProductStatusCodes AS PSC ON PSC.Id = P.ProductStatusId
JOIN BackendMembers AS BM ON BM.Id = P.CreatedBackendMemberId";
				return conn.Query<ProductIndexVM>(sql);
			}
		}
	}
}