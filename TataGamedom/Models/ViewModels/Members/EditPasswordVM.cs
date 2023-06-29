using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TataGamedom.Models.ViewModels
{
	public class EditPasswordVM
	{
		[Display(Name = "原始密碼")]
		[Required]
		[StringLength(50)]
		[DataType(DataType.Password)]
		public string OringinalPassword { get; set; }

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