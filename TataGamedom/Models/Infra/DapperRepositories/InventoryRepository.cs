using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Web;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.ViewModels.InventoryItems;

namespace TataGamedom.Models.Infra.DapperRepositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private string _connstr => System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ToString();

        public IEnumerable<InventoryVM> Search()
        {
            using (var connection = new SqlConnection(_connstr)) 
            {
                string sql = @"SELECT 
SUM(IIT.Cost) AS Total, COUNT(IIT.ProductId) AS [Count], P.IsVirtual AS ProductIsVirtual, G.ChiName AS GameName, G.GameCoverImg AS GameCoverImage,
P.[Index] AS ProductIndex, P.Id AS ProductId
FROM InventoryItems AS IIT
RIGHT JOIN Products AS P ON　IIT.ProductId = P.Id
RIGHT JOIN Games AS G ON P.GameId = G.Id 
GROUP BY IIT.ProductId,P.IsVirtual, G.ChiName, G.GameCoverImg,P.[Index],P.Id
ORDER BY COUNT(IIT.ProductId) DESC
";
                return connection.Query<InventoryVM>(sql);
            }
        }
    }
}