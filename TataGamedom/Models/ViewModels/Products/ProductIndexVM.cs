using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TataGamedom.Models.ViewModels.Products
{
	public class ProductIndexVM
	{
		public int Id { get; set; }
		[Display(Name="編號")]
		[Required]
		[StringLength(20)]
		public string Index { get; set; }
		[Display(Name = "遊戲名稱")]
		public string GameName { get; set; }
		[Display(Name = "虛擬商品")]
		public bool IsVirtual { get; set; }
		[Display(Name = "售價")]
		public int Price { get; set; }
		[Display(Name = "平台")]
		public string GamePlatformName { get; set; }
		[Display(Name = "狀態")]
		[Required]
		[StringLength(1500)]

		public string ProductStatus { get; set; }
		[Display(Name = "發售日")]
		public DateTime SaleDate { get; set; }
		[Display(Name = "創建者")]
		public string CreatedBackendMemberName { get; set; }
		[Display(Name = "創建時間")]
		public DateTime CreatedTime { get; set; }

	}
}