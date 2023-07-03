using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TataGamedom.Models.ViewModels.Boards
{
	public class BoardCreateEditorVM
	{
		public int? Id { get; set; }
		[Required]
        public string Name { get; set; }
		[Required]
		public string BoardAbout { get; set; }
		public string BoardHeaderCoverImg { get; set; }
		public string BoardHeaderCoverImgPath { get; set; }
	}

}