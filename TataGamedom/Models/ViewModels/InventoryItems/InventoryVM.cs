using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TataGamedom.Models.ViewModels.InventoryItems
{
    public class InventoryItemVM
    {
        public int Id { get; set; }

        public string Index { get; set; }

        public int ProductId { get; set; }

        public int StockInSheetId { get; set; }

        public decimal Cost { get; set; }

        public string GameKey { get; set; }
    }

    public class InventoryVM 
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string GameName { get; set; }

        public bool ProductIsVirtual { get; set; }

        public IEnumerable<InventoryItemVM> InventoryItems { get; set; }

        public int Count => InventoryItems.Where(item => item.ProductId == ProductId).Count();

        public decimal Total => InventoryItems.Where(item => item.ProductId == ProductId).Sum(item => item.Cost);

    }
}