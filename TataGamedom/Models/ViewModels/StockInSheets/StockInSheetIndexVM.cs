using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TataGamedom.Models.ViewModels.StockInSheets
{
	public class StockInSheetIndexVM
	{
        public int Id { get; set; }

		[Display(Name = "進貨單編號")]
		public string Index { get; set; }

		[Display(Name = "下單日")]
		[DataType(DataType.Date)]
		public DateTime OrderRequestDate { get; set; }

		[Display(Name = "到貨日")]
		[DataType(DataType.Date)]
		public DateTime? ArrivedAt { get; set; }

		[Display(Name = "數量")]
		public int Quantity { get; set; }

		[Display(Name = "進貨狀態")]
		public string StockInStatusCodeName { get; set; }

		[Display(Name = "供應商")]
		public string SupplierName { get; set; }
    }
}