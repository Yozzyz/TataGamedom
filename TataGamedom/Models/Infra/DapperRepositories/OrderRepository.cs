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

		//
		public string Create(OrderDto dto)
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
@CreatedAt,@CompletedAt,@ShipmemtMethodId,@RecipientName,@ToAddress)";

				var entity = connection.QuerySingle<Order>(sql, dto);
				return entity.Index;
				
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

	}
}