using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TataGamedom.Models.ViewModels.Games
{
	public class GameEditCoverImgVM
	{
		public int Id { get; set; }

		[Display(Name = "商品照片")] public string GameCoverImg { get; set; }

		public DateTime? ModifiedTime { get; set; }
		[Display(Name = "最後修改者")]

		public string ModifiedBackendMemberName { get; set; }
		public int ModifiedBackendMemberId { get; set; }
	}
}