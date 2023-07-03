using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Web;
using TataGamedom.Models.Dtos.InventoryItems;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.ViewModels.InventoryItems;

namespace TataGamedom.Models.Infra.DapperRepositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private string Connstr => System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ToString();

        public IEnumerable<InventoryVM> Search()
        {
            using (var connection = new SqlConnection(Connstr)) 
            {
                string sql = @"SELECT 
SUM(IIT.Cost) AS Total, P.IsVirtual AS ProductIsVirtual, G.ChiName AS GameName, G.GameCoverImg AS GameCoverImage,
P.[Index] AS ProductIndex,IIT.ProductId,
(SELECT COUNT(*) FROM OrderItems AS OI RIGHT JOIN InventoryItems AS II ON OI.InventoryItemId = II.Id
WHERE II.ProductId = P.Id AND OI.ProductId IS NULL) AS [Count], P.Id
FROM InventoryItems AS IIT
RIGHT JOIN Products AS P ON　IIT.ProductId = P.Id
RIGHT JOIN Games AS G ON P.GameId = G.Id 
GROUP BY IIT.ProductId,P.IsVirtual, G.ChiName, G.GameCoverImg,P.[Index],P.Id
ORDER BY COUNT(IIT.ProductId) DESC
";
                return connection.Query<InventoryVM>(sql);
            }
        }

        public IEnumerable<InventoryItemVM> Info(int? productId)
        {
            using (var connection = new SqlConnection(Connstr))
            {
                string sql = @"
SELECT IIT.[Index] AS SKU, IIT.Cost AS Cost, IIT.GameKey AS GameKey, SIS.[Index] AS StockInSheetIndex, G.ChiName AS GameName
FROM InventoryItems AS IIT
JOIN StockInSheets AS SIS ON IIT.StockInSheetId = SIS.Id
JOIN Products AS P ON IIT.ProductId = P.Id
JOIN Games AS G ON P.GameId = G.Id
WHERE IIT.ProductId = @productId
ORDER BY SKU ";

                return connection.Query<InventoryItemVM>(sql, new { ProductId = productId });
            }
        }

		public int GetMaxIdInDb()
		{
			using (var connection = new SqlConnection(Connstr))
			{
				string sql = "SELECT MAX(Id) FROM InventoryItems";
				int maxId = connection.QuerySingle<int>(sql);

				return maxId;
			}
		}
		public string GetProductIndex(int productId)
		{
			using (var connection = new SqlConnection(Connstr))
			{
				string sql = @"
SELECT DISTINCT p.[Index]
FROM Products AS p JOIN InventoryItems AS II ON II.ProductId = P.Id
WHERE II.ProductId = @productId";
				return connection.QuerySingleOrDefault<InventoryItemCreateDto>(sql, new { ProductId = productId }).Index;
			}
		}

		public void Create(InventoryItemCreateDto dto)
		{
			using (var connection = new SqlConnection(Connstr)) 
			{
				string sql = @"
INSERT INTO InventoryItems
([Index],[ProductId],[StockInSheetId] ,[Cost],[GameKey])
VALUES
(@Index, @ProductId, @StockInSheetId, @Cost, @GameKey)";
				connection.Execute(sql, dto);
			}
		}

	}
}