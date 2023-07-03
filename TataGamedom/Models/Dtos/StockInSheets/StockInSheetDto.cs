using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TataGamedom.Models.EFModels;

namespace TataGamedom.Models.Dtos.StockInSheets
{
	public class StockInSheetDto
	{
		public int Id { get; set; }

		public string Index { get; set; }

		public int StockInStatusId { get; set; }

		public int SupplierId { get; set; }

		public int Quantity { get; set; }

		public DateTime OrderRequestDate { get; set; }

		public DateTime? ArrivedAt { get; set; }

	}
	public static class StockInSheetExts 
	{
		public static StockInSheetDto ToDto(this StockInSheet entity) 
		{
			return new StockInSheetDto
			{
				Id = entity.Id,
				Index = entity.Index,
				StockInStatusId = entity.StockInStatusId,
				SupplierId = entity.SupplierId,
				Quantity = entity.Quantity,
				OrderRequestDate = entity.OrderRequestDate,
				ArrivedAt = entity.ArrivedAt
			};
		}
	}


}