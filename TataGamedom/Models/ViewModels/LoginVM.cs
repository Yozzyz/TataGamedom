using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TataGamedom.Models.ViewModels
{
	public class LoginVM
	{
		[Display(Name = "帳號")]
		[Required]
		public string Account { get; set; }


		[Display(Name = "密碼")]
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}