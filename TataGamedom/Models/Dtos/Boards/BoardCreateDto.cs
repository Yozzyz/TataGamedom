using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using TataGamedom.Models.EFModels;

namespace TataGamedom.Models.Dtos.Boards
{
	public class BoardCreateDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string BoardAbout { get; set; }
		public string BoardHeaderCoverImg { get; set; }
		public int CreatedBackendMemberId { get; set; }
		public DateTime CreatedTime { get; set; }
	};

}