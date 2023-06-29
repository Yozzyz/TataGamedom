using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TataGamedom.Models.ViewModels
{
	public class ForgetPasswordVM
	{
		[Display(Name = "帳號")]
		[Required(ErrorMessage = "{0} 必填")]
		[StringLength(30)]
		public string Account { get; set; }

		[Display(Name = "Email")]
		[Required(ErrorMessage = "{0} 必填")]
		[StringLength(256)]
		[EmailAddress(ErrorMessage = "Email格式有誤")]
		public string Email { get; set; }
	}
}