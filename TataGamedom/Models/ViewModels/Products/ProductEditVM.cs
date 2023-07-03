using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TataGamedom.Models.ViewModels.Products
{
	public class ProductEditVM
	{
		public int Id { get; set; }
		[Display(Name = "商品a編號")]
		public string Index { get; set; }
		[Display(Name = "遊戲名稱")]
		public string GameName { get; set; }
        public string Description { get; set; }
        [Display(Name = "虛擬商品")]
		[Required]
		public bool IsVirtual { get; set; }
		[Display(Name = "售價")]
		[Required]
		public int Price { get; set; }
		[Display(Name = "平台")]
		[Required]
		public int GamePlatform { get; set; }
		[Display(Name = "狀態")]
		[Required]
		public int ProductStatus { get; set; }
		[Display(Name = "發售日")]
		[Required]
		public DateTime SaleDate { get; set; }
		[Display(Name = "最後修改者")]
		public string ModifiedBackendMemberName { get; set; }
		[Display(Name = "最後修改時間")]
		public DateTime? ModifiedTime { get; set; }
	}
}