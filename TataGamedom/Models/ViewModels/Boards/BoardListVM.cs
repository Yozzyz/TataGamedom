using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TataGamedom.Models.Dtos.Boards
{
	public class BoardListVM
	{
		public int Id { get; set; }	
		public string Name { get; set; }
		public string BoardAbout { get; set; }
		public string GameName { get; set; }
		public string BoardHeaderCoverImg { get; set; }
		public int FollowersCount { get; set; }
		public DateTime LastPostedAt { get; set; }
    }
}