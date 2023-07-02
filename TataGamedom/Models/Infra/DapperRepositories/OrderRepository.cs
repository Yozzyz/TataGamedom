using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using TataGamedom.Models.Dtos.Orders;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.Services;
using static TataGamedom.Models.Services.OrderService;

namespace TataGamedom.Models.Infra.DapperRepositories
{
	public class OrderRepository : IOrderRepository
	{
		private string _connstr => System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ToString();

		
		public void Create(OrderDto dto)
		{
			using (var connection = new SqlConnection(_connstr))
			{
				string sql = @"
INSERT INTO Orders 
([Index],[MemberId],[OrderStatusId],[ShipmentStatusId],[PaymentStatusId],
[CreatedAt],[CompletedAt],[ShipmemtMethodId],[RecipientName],[ToAddress],
[SentAt],[DeliveredAt],[TrackingNum])
VALUES
(@Index,@MemberId,@OrderStatusId,@ShipmentStatusId,@PaymentStatusId,
@CreatedAt,@CompletedAt,@ShipmemtMethodId,@RecipientName,@ToAddress,
@SentAt,@DeliveredAt,@TrackingNum)";

				connection.Execute(sql, dto);
			}
		}

        public IEnumerable<OrderInfoDto> GetOrderItemsInfo(string index)
        {
			using (var connection = new SqlConnection(_connstr)) 
			{
				string sql = @"SELECT
OI.[Index] AS OrderItemIndex, OI.ProductPrice, O.[Index], O.CreatedAt, O.CompletedAt,
(SELECT SUM(OI.ProductPrice) FROM OrderItems AS OI WHERE OI.OrderId = O.Id) AS Total, 
IIT.GameKey, OSC.[Name] AS OrderStatusCodeName, PSC.[Name] AS PaymentStatusCodeName, SSC.[Name] AS ShipmentStatusCodeName,G.ChiName AS GameName,
G.GameCoverImg, C.[Description] AS CouponDescription, O.Id
FROM OrderItems AS OI
RIGHT JOIN Orders AS O ON OI.OrderId = O.Id
LEFT JOIN InventoryItems AS IIT ON OI.InventoryItemId = IIT.Id
LEFT JOIN OrderItemsCoupons AS OIC ON OI.Id = OIC.OrderItemId
LEFT JOIN Coupons AS C ON OIC.CouponId = C.Id
JOIN OrderStatusCodes AS OSC ON O.OrderStatusId = OSC.Id
JOIN PaymentStatusCodes AS PSC ON O.PaymentStatusId = PSC.Id
JOIN ShipmentStatusesCodes AS SSC ON O.ShipmentStatusId = SSC.Id
LEFT JOIN Products AS P ON　OI.ProductId = P.Id
LEFT JOIN Games AS G ON P.GameId = G.Id
WHERE O.[Index] = @Index
";

                IEnumerable<OrderInfoDto> orderInfo = connection.Query<OrderInfoDto>(sql, new { Index = index }).ToList();	
				return orderInfo;
			}
        }

        public int GetMaxIdInDb()
		{
			using (var connection = new SqlConnection(_connstr))
			{
				string sql = "SELECT MAX(Id) FROM Orders";
				int maxId = connection.QuerySingle<int>(sql);
				
				return maxId;
			}	
		}

		/// <summary>
		/// Query(篩選條件+排序)
		/// </summary>
		/// <param name="criteria"></param>
		/// <param name="sortInfo"></param>
		/// <returns></returns>
		public IEnumerable<OrderIndexDto> Search(Criteria criteria, SortInfo sortInfo)
		{
			using (var connection = new SqlConnection(_connstr))
			{
				IEnumerable<OrderIndexDto> orders = connection.Query<OrderIndexDto>(GetSql(criteria, sortInfo), criteria);
				return orders;
			}
		}

		/// <summary>
		/// Query(篩選條件+排序+分頁)
		/// </summary>
		/// <param name="criteria"></param>
		/// <param name="sortInfo"></param>
		/// <param name="sqlAddPage"></param>
		/// <returns></returns>
		public IEnumerable<OrderIndexDto> Search(Criteria criteria, SortInfo sortInfo, string sqlAddPage)
		{
			using (var connection = new SqlConnection(_connstr))
			{
				IEnumerable<OrderIndexDto> orders = connection.Query<OrderIndexDto>(sqlAddPage, criteria);
				return orders;
			}
		}

        public OrderDto GetByIndex(string index)
        {
			using (var connection = new SqlConnection(_connstr)) 
			{
				string sql = "SELECT * FROM Orders WHERE [Index] = @Index";
                var order = connection.QuerySingleOrDefault<Order>(sql, new { Index = index });
				return order.ToDto();
			}
        }

        public void Update(OrderDto dto)
        {
			using (var connection = new SqlConnection(_connstr)) 
			{
				string sql = @"UPDATE Orders SET 
MemberId = @MemberId , OrderStatusId = @OrderStatusId, ShipmentStatusId = @ShipmentStatusId , CreatedAt = @CreatedAt, CompletedAt = @CompletedAt,
ShipmemtMethodId = @ShipmemtMethodId, RecipientName = @RecipientName, ToAddress = @ToAddress, SentAt= @SentAt, DeliveredAt = @DeliveredAt, 
TrackingNum = @TrackingNum
WHERE [Index] = @Index";
				connection.ExecuteScalar(sql,dto);
			}
        }

   //     public void Delete(string index)
   //     {
   //         var dbContext = new AppDbContext();
			//var order = dbContext.Orders.SingleOrDefault(o => o.Index == index);
			//if (order == null) { return;}
			//dbContext.Orders.Remove(order);
			//dbContext.SaveChanges();
   //     }
    }
}