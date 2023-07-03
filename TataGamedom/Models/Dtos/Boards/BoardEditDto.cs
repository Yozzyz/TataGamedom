using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TataGamedom.Models.Dtos.Boards
{
	public class BoardEditDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string BoardAbout { get; set; }
	}
}