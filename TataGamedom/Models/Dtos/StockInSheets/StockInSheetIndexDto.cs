using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
}