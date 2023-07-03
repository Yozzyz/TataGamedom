using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TataGamedom.Models.Dtos.StockInSheets;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.Interfaces;

namespace TataGamedom.Models.Infra.DapperRepositories
{
	public class StockInSheetRepository : IStockInSheetRepository
	{
		private string Connstr => System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ToString();
		public void Create(StockInSheetDto dto)
		{
			using (var connection = new SqlConnection(Connstr)) 
			{
				string sql = @"
INSERT INTO StockInSheets
([Index], [StockInStatusId], [SupplierId], [Quantity], [OrderRequestDate], 
[ArrivedAt])
VALUES 
(@Index, @StockInStatusId, @SupplierId, @Quantity, @OrderRequestDate, @ArrivedAt)";

				connection.Execute(sql, dto);
			}
		}
		public int GetMaxIdInDb()
		{
			using (var connection = new SqlConnection(Connstr))
			{
				string sql = "SELECT MAX(Id) FROM StockInSheets";
				int maxId = connection.QuerySingle<int>(sql);

				return maxId;
			}
		}

		public IEnumerable<StockInSheetIndexDto> Search()
		{
			using (var connection = new SqlConnection(Connstr)) 
			{
				string sql = @"
SELECT 
SIS.[Index], SIS.OrderRequestDate, SIS.ArrivedAt, SIS.Quantity, SISC.[Name] AS StockInStatusCodeName,
S.[Name] AS SupplierName, SIS.Id
FROM StockInSheets AS SIS
JOIN StockInStatusCodes AS SISC ON SIS.StockInStatusId = SISC.Id 
JOIN Suppliers AS S ON SIS.SupplierId = S.Id
ORDER BY SIS.OrderRequestDate DESC
";
				var stockInSheet = connection.Query<StockInSheetIndexDto>(sql);
				return stockInSheet;
			}
		}

		public StockInSheetDto GetById(int? id)
		{
			using (var connection = new SqlConnection(Connstr))
			{
				string sql = @"SELECT * FROM StockInSheets WHERE [Id] = @id";
				var stockInSheet = connection.QuerySingleOrDefault<StockInSheet>(sql, new { Id = id });
				return stockInSheet.ToDto();
			}
		}

		public void Update(StockInSheetDto dto)
		{
			using (var connection = new SqlConnection(Connstr)) 
			{
				string sql = @"
UPDATE StockInSheets SET
[StockInStatusId] = @StockInStatusId , [SupplierId]= @SupplierId, [Quantity]= @Quantity, [OrderRequestDate]= @OrderRequestDate, [ArrivedAt] = @ArrivedAt
WHERE Id = @id";

				connection.ExecuteScalar(sql, dto);
			}
		}
	}
}