using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TataGamedom.Models.Dtos.Orders;
using TataGamedom.Models.Infra;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.ViewModels.Orders;

namespace TataGamedom.Models.Services
{
	public class OrderService
	{
		private readonly IOrderRepository _repo;

		public OrderService(IOrderRepository repo)
		{
			_repo = repo;
		}
		public IEnumerable<OrderIndexDto> Search(Criteria criteria, SortInfo sortInfo) => _repo.Search(criteria, sortInfo);
		public IEnumerable<OrderIndexDto> Search(Criteria criteria, SortInfo sortInfo, string sqlAddPage) => _repo.Search(criteria, sortInfo, sqlAddPage);

		public static string GetSql(Criteria criteria, SortInfo sortInfo)
		{
			var sql = new StringBuilder();

			string sqlJoin = @"
SELECT 
O.Id, O.[Index], O.CreatedAt, OSC.[Name] AS OrderStatusCodeName, PSC.[Name] AS PaymentStatusCodeName, SSC.[Name] AS ShipmentStatusCodeName,
M.[Name] AS MemberName, SUM(OI.ProductPrice) AS Total
FROM Orders AS O 
JOIN OrderStatusCodes AS OSC ON O.OrderStatusId = OSC.Id
JOIN PaymentStatusCodes AS PSC ON O.PaymentStatusId = PSC.Id
JOIN ShipmentStatusesCodes AS SSC ON O.ShipmentStatusId= SSC.Id
JOIN Members AS M ON O.MemberId = M.Id
LEFT JOIN OrderItems AS OI ON O.Id = OI.OrderId";

			string sqlSort = sortInfo.sqlSort();

			string sqlGroupByOrderby = $@"GROUP BY
O.[Index], O.CreatedAt, OSC.[Name], PSC.[Name], SSC.[Name],
M.[Name], O.OrderStatusId, O.ShipmentStatusId, O.PaymentStatusId,O.Id
{sqlSort}";

			if (criteria.OrderStatusId != null && criteria.OrderStatusId.Value > 0 && criteria.Index == null)
			{
				string sqlWhere = "WHERE O.OrderStatusId = @OrderStatusId";
				return sql.Append(sqlJoin)
						.AppendLine()
						.Append(sqlWhere)
						.AppendLine()
						.Append(sqlGroupByOrderby).ToString();
			}
			if (criteria.OrderStatusId != null && criteria.OrderStatusId.Value > 0 && criteria.Index != null)
			{
				string sqlWhere = "WHERE O.OrderStatusId = @OrderStatusId";
				string sqlAND = "AND O.[Index] LIKE '%' + @Index + '%'";
				return sql.Append(sqlJoin)
						.AppendLine()
						.Append(sqlWhere)
						.AppendLine()
						.Append(sqlAND)
						.AppendLine()
						.Append(sqlGroupByOrderby).ToString();
			}
			if (criteria.OrderStatusId == 0 && criteria.Index != null)
			{
				string sqlAND = "WHERE O.[Index] LIKE '%' + @Index + '%'";
				return sql.Append(sqlJoin)
						.AppendLine()
						.Append(sqlAND)
						.AppendLine()
						.Append(sqlGroupByOrderby).ToString();
			}
			else
			{
				return sql.Append(sqlJoin).AppendLine().Append(sqlGroupByOrderby).ToString();
			}

		}

		public Result Create(OrderDto dto)
		{
			int maxId =  _repo.GetMaxIdInDb();
			var indexGenerator = new IndexGenerator(maxId);

			dto.Index = indexGenerator.GetOrderIndex(dto);
			_repo.Create(dto);
			return Result.Success();
		}

        public IEnumerable<OrderInfoDto> GetOrderItemsInfo(string index) => _repo.GetOrderItemsInfo(index);

		public OrderDto GetByIndex(string index) => _repo.GetByIndex(index);

		public Result Update(OrderDto dto)
		{
			_repo.Update(dto);
			return Result.Success();
		}

		public void Delete(string index) => _repo.Delete(index);
    }

	/// <summary>
	/// 篩選條件: 訂單狀態,訂單編號
	/// </summary>
	public class Criteria
	{
		public int? OrderStatusId { get; set; }
		public string Index { get; set; }
	}

	/// <summary>
	/// 排序依據: 欄位,方向
	/// </summary>
	public class SortInfo
	{
		public EnumColumn ColumnName { get; set; }
		public EnumDirection Direction { get; set; }
		public string UrlTemplate { get; set; } = "/Orders/Index?{0}";

		private Criteria _criteria;

		public SortInfo(string columnName, string directionName, Criteria criteria)
		{

			if (Enum.TryParse(columnName, out EnumColumn colValue))
			{
				this.ColumnName = colValue;
			}
			else
			{
				this.ColumnName = EnumColumn.OrderStatusCodeName;
			}

			if (Enum.TryParse(directionName, out EnumDirection directionValue))
			{
				this.Direction = directionValue;
			}
			else
			{
				this.Direction = EnumDirection.ASC;
			}

			_criteria = criteria;
		}

		/// <summary>
		/// 取得排序sql
		/// </summary>
		/// <returns></returns>
		public string sqlSort()
		{
			switch (this.ColumnName)
			{
				case EnumColumn.Index:
					return (this.Direction == EnumDirection.ASC)
						? "ORDER BY O.[Index], O.OrderStatusId , O.ShipmentStatusId"
						: "ORDER BY O.[Index] DESC , O.OrderStatusId , O.ShipmentStatusId";
				case EnumColumn.CreatedAt:
					return (this.Direction == EnumDirection.ASC)
						? "ORDER BY O.CreatedAt, O.OrderStatusId , O.ShipmentStatusId"
						: "ORDER BY O.CreatedAt DESC, O.OrderStatusId DESC , O.ShipmentStatusId";
				case EnumColumn.MemberName:
					return (this.Direction == EnumDirection.ASC)
						? "ORDER BY M.[Name], O.OrderStatusId , O.ShipmentStatusId"
						: "ORDER BY M.[Name] DESC, O.OrderStatusId DESC , O.ShipmentStatusId";
				case EnumColumn.OrderStatusCodeName:
					return (this.Direction == EnumDirection.ASC)
						? "ORDER BY O.OrderStatusId, O.ShipmentStatusId"
						: "ORDER BY O.OrderStatusId DESC, O.ShipmentStatusId";
				case EnumColumn.ShipmentStatusCodeName:
					return (this.Direction == EnumDirection.ASC)
						? "ORDER BY O.ShipmentStatusId, O.OrderStatusId"
						: "ORDER BY O.ShipmentStatusId DESC, O.OrderStatusId DESC";
				case EnumColumn.PaymentStatusCodeName:
					return (this.Direction == EnumDirection.ASC)
						? "ORDER BY O.PaymentStatusId, O.OrderStatusId , O.ShipmentStatusId"
						: "ORDER BY O.PaymentStatusId DESC, O.OrderStatusId DESC , O.ShipmentStatusId";
				case EnumColumn.Total:
					return (this.Direction == EnumDirection.ASC)
						? "ORDER BY SUM(OI.ProductPrice), O.OrderStatusId , O.ShipmentStatusId"
						: "ORDER BY SUM(OI.ProductPrice) DESC, O.OrderStatusId DESC , O.ShipmentStatusId";
			}
			return "ORDER BY O.OrderStatusId , O.ShipmentStatusId";
		}

		public MvcHtmlString RenderItem(EnumColumn column)
		{
			string template = "<a href=\"{0}\">{1}</a>&nbsp;<a href=\"{2}\">{3}</a>";
			string urlAsc = GetUrl(column, EnumDirection.ASC);
			string urlDesc = GetUrl(column, EnumDirection.Desc);
			string iconAsc = RenderIcon(column, EnumDirection.ASC);
			string iconDesc = RenderIcon(column, EnumDirection.Desc);

			return new MvcHtmlString(string.Format(template, urlAsc, iconAsc, urlDesc, iconDesc));
		}

		public string GetUrl(EnumColumn column, EnumDirection direction)
		{
			if (this.ColumnName == column && this.Direction == direction)
			{
				return string.Empty;
			}

			string args = $"Index={_criteria.Index}&orderStatusId={_criteria.OrderStatusId}&columnName={column}&directionName={direction}";

			return string.Format(UrlTemplate, args);
		}

		/// <summary>
		/// 用於View，整合分頁+篩選+排序Url
		/// </summary>
		/// <returns></returns>
		public string GetQueryString()
		{
			string template = $"Index={_criteria.Index}&orderStatusId={_criteria.OrderStatusId}&ColumnName={ColumnName}&Direction={Direction}";
			return template;

		}
		private string RenderIcon(EnumColumn column, EnumDirection direction)
		{
			if (ColumnName == column && Direction == direction)
			{
				return (direction == EnumDirection.ASC)
					? "<i class=\"fas fa-caret-up \" style='color:red;'></i>"
					: "<i class=\"fas fa-caret-down \" style='color:red;'></i>";
			}

			return (direction == EnumDirection.ASC)
				? "<i class=\"fas fa-caret-up\"></i>"
				: "<i class=\"fas fa-caret-down\"></i>";
		}

		public enum EnumColumn
		{
			Index,
			CreatedAt,
			MemberName,
			OrderStatusCodeName,
			ShipmentStatusCodeName,
			PaymentStatusCodeName,
			Total
		}
		public enum EnumDirection
		{
			ASC,
			Desc
		}
	}

	/// <summary>
	/// 分頁
	/// </summary>
	public class PageInfo
	{
		public int PageNum { get; set; }
		public int PageSize { get; set; }
		public int TotalRecords { get; set; }
		public int Pages => (int)Math.Ceiling((double)TotalRecords / PageSize);
		public int PageItemCount => 5;
		public int PageBarStartNumber
		{
			get
			{
				int startNumber = PageNum - ((int)Math.Floor((double)this.PageItemCount / 2));
				return startNumber < 1 ? 1 : startNumber;
			}
		}
		public int PageItemPrevNumber => (PageBarStartNumber <= 1) ? 1 : PageBarStartNumber - 1;

		public int PageBarItemCount => PageBarStartNumber + PageItemCount > Pages
			? Pages - PageBarStartNumber + 1
			: PageItemCount;

		// Todo 新增能到第一頁跟最後一頁的
		public int PageItemNextNumber => (PageBarStartNumber + PageItemCount >= Pages) ? Pages : PageBarStartNumber + PageItemCount;

		private string urlTemplate;
		public PageInfo(int totalRecords, int pageNum, int pageSize, string urlTemplate)
		{
			TotalRecords = totalRecords < 0 ? 0 : totalRecords;
			PageNum = pageNum < 1 ? 1 : pageNum;
			PageSize = pageSize < 1 ? 20 : pageSize;
			this.urlTemplate = urlTemplate;
		}

		/// <summary>
		/// 於View整併Url
		/// </summary>
		/// <param name="pageNum"></param>
		/// <returns></returns>
		public string GetUrl(int pageNum) => string.Format(urlTemplate, pageNum);

		public string sqlPage(Criteria criteria, SortInfo sortInfo)
		{
			int recordStartIndex = (PageNum - 1) * PageSize;
			return $@"
OFFSET {recordStartIndex} ROWS
FETCH NEXT {PageSize} ROWS ONLY";
		}
	}
	
}