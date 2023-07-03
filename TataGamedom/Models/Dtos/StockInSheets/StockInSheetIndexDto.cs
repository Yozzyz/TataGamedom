using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TataGamedom.Models.ViewModels.StockInSheets;

namespace TataGamedom.Models.Dtos.StockInSheets
{
	public class StockInSheetIndexDto
	{
		public int Id { get; set; }

		public string Index { get; set; }

		public DateTime OrderRequestDate { get; set; }

		public DateTime? ArrivedAt { get; set; }

		public int Quantity { get; set; }

		public string StockInStatusCodeName { get; set; }

		public string SupplierName { get; set; }
	}

	public static class StockInSheetIndexExts 
	{
		public static StockInSheetIndexVM ToVM(this StockInSheetIndexDto dto) 
		{
			return new StockInSheetIndexVM
			{
				Id = dto.Id,
				Index = dto.Index,
				OrderRequestDate = dto.OrderRequestDate,
				ArrivedAt = dto.ArrivedAt,
				Quantity = dto.Quantity,
				StockInStatusCodeName = dto.StockInStatusCodeName,
				SupplierName = dto.SupplierName
			};
		}
	}
}