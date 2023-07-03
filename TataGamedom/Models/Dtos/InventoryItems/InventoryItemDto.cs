using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Web;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.ViewModels.InventoryItems;
using static Dapper.SqlMapper;

namespace TataGamedom.Models.Dtos.InventoryItems
{
    public class InventoryItemDto
    {
        public int Id { get; set; }

        public string Index { get; set; }

        public int ProductId { get; set; }

        public string StockInSheetIndex { get; set; }

        public decimal Cost { get; set; }

        public string GameKey { get; set; }

        public string GameName { get; set; } 
    }
	public class InventoryItemCreateDto
	{
		public int Id { get; set; }

		public string Index { get; set; }

		public int ProductId { get; set; }
        public int StockInSheetId { get; set; }
        public string StockInSheetIndex { get; set; }

		public decimal Cost { get; set; }

		public string GameKey { get; set; }

	}

	public static class InventoryItemExts 
    {
        public static InventoryItemVM ToVM(this InventoryItemDto dto)
        {
            return new InventoryItemVM 
            {
                Id = dto.Id,
                SKU = dto.Index,
                ProductId = dto.ProductId,
                StockInSheetIndex = dto.StockInSheetIndex,
                Cost = dto.Cost,
                GameKey = dto.GameKey,
                GameName = dto.GameName
            };
        }

        public static InventoryItemDto ToDto(this InventoryItem entity) 
        {
            return new InventoryItemDto
            {
                Id = entity.Id,
                Index = entity.Index,
                ProductId = entity.ProductId,
                StockInSheetIndex = entity.StockInSheet.Index,
                Cost = entity.Cost,
                GameKey = entity.GameKey,
                GameName = entity.Product.Game.ChiName
            };
        }
        public static InventoryItemCreateDto ToDto(this InventoryItemVM vm) 
        {
            return new InventoryItemCreateDto
			{
				Id = vm.Id,
				Index = vm.SKU,
				ProductId = vm.ProductId,
                StockInSheetId = vm.StockInSheetId,
				StockInSheetIndex = vm.StockInSheetIndex,
				Cost = vm.Cost,
				GameKey = vm.GameKey,
			};
        }
    }
}