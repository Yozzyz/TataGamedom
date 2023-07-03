using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TataGamedom.Models.ViewModels.Games
{
	public class GameAddProductVM
	{
        public int Id { get; set; }
		[Display(Name = "遊戲編號")]		
		public int GameId { get; set; }
		[Display(Name = "中文名稱")]
		public string GameChiName { get; set; }
		[Display(Name = "英文名稱")]
		public string GameEngName { get; set; }
		[Display(Name = "遊戲簡介")]
		public string Description { get; set; }
		[Display(Name = "遊戲平台")]
		public int Platform { get; set; }
		[Display(Name = "是否為虛擬商品？")]
		public bool IsVirtual { get; set; }
		[Display(Name = "售價")]
		public int Price { get; set; }
		[Display(Name = "系統需求")]
		public string SystemRequire { get; set; }

		[Display(Name = "商品圖片")]
		public List<string> ProductImg { get; set; }
		[Display(Name = "發售日")]
		public DateTime SaleDate { get; set; }
		[Display(Name = "商品狀態")]
		public string Status { get; set; }

        public DateTime CreateTime { get; set;}

		public int CreateBackendMemberId { get; set; }


    }
}