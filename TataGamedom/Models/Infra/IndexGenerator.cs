using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TataGamedom.Models.Dtos.InventoryItems;
using TataGamedom.Models.Dtos.Orders;
using TataGamedom.Models.Dtos.StockInSheets;
using TataGamedom.Models.Interfaces;

namespace TataGamedom.Models.Infra
{
    public class IndexGenerator : IIndexGenerator
    {
        private int _Id;
        public IndexGenerator(int id)
        {
            _Id = id;
        }
     
        /// <summary>
        /// "命名規則: CreatedAt+ ShipmentMethodId + MemberId + Id"
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public string GetOrderIndex(OrderDto dto) => string.Concat(dto.CreatedAt.ToString("yyyyMMdd"), dto.ShipmemtMethodId, dto.MemberId, _Id + 1);


		/// <summary>
		/// "Display(Name='SKU') , 命名規則: ProductIndex + StockInSheetIndex + Id"
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		public string GetSKU(InventoryItemCreateDto dto ,string productIndex) => string.Concat(productIndex, dto.StockInSheetIndex, _Id + 1);

		/// <summary>
		/// "命名規則: OrderRequestDate + SupplierId + Id"
		/// </summary>
		/// <returns></returns>
		public string GetStockInSheetIndex(StockInSheetDto dto) => string.Concat(dto.OrderRequestDate.ToString("yyyyMMdd"), dto.SupplierId, _Id + 1);
	}
}