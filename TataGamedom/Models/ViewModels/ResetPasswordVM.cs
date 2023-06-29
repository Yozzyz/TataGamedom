using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TataGamedom.Models.ViewModels
{
	public class ResetPasswordVM
	{
		[Display(Name = "新密碼")]
		[Required]
		[StringLength(20)]
		[DataType(DataType.Password)]
		public string CreatePassword { get; set; }

		[Display(Name = "確認密碼")]
		[Required]
		[StringLength(20)]
		[Compare("CreatePassword", ErrorMessage = "必須與'密碼'欄位值相同")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}