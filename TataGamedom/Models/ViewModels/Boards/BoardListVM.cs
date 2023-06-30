using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TataGamedom.Models.Dtos.Boards
{
	public class BoardListDto
	{
		public int Id { get; set; }	
		public string BoardName { get; set; }
		public string BoardAboud { get; set; }
		public string GameName { get; set; }
		public string Cover { get; set; }
		public int FollowersCount { get; set; }
		public DateTime LastPostedAt { get; set; }
    }
}