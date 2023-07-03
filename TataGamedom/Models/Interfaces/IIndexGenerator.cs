using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TataGamedom.Models.Dtos.Orders;
using TataGamedom.Models.EFModels;

namespace TataGamedom.Models.Interfaces
{
	//Orders "命名規則: CreatedAt+ ShipmentMethodId + MemberId + Id"
	// e.g.             20230630         1             2        1
	//				=>20230630121
	//OrderItemReturns: "命名規則: IssuedAt + OrderItemsIndex + Id"
	//InventoryItems:"Display(Name='SKU') , 命名規則: 1/0(Isvirtual) + ProductIndex + Id"
	//StockInSheets : "命名規則: OrderRequestDate + InventoryItemSKU + Id"
	//Product: ?

	public interface IIndexGenerator
	{
		string GetOrderIndex(OrderDto dto);
	}

}
