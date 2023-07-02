using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Web;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.ViewModels.InventoryItems;

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

    public static class InventoryExts 
    {
        public static InventoryItemVM ToVM(this InventoryItemDto dto)
        {
            return new InventoryItemVM 
            {
                Id = dto.Id,
                Index = dto.Index,
                ProductId = dto.ProductId,
                StockInSheetId = dto.StockInSheetId,
                Cost = dto.Cost,
                GameKey = dto.GameKey
            };
        }

        public static InventoryItemDto ToDto(this InventoryItem entity) 
        {
            return new InventoryItemDto
            {
                Id = entity.Id,
                Index = entity.Index,
                ProductId = entity.ProductId,
                StockInSheetId= entity.StockInSheetId,
                Cost = entity.Cost,
                GameKey = entity.GameKey
            };
        }
    }
}