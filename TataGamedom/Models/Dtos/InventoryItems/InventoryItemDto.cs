using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TataGamedom.Models.Dtos.InventoryItems
{
    public class InventoryItemDto
    {
        public int Id { get; set; }

        public string Index { get; set; }

        public int ProductId { get; set; }

        public int StockInSheetId { get; set; }

        public decimal Cost { get; set; }

        public string GameKey { get; set; }
    }
}