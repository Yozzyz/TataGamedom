using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TataGamedom.Models.ViewModels
{
	public class RegisterVM
	{
		public int Id { get; set; }
		[Display(Name = "姓名")]
		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		[Display(Name = "帳號")]
		[Required]
		[StringLength(30)]
		public string Account { get; set; }

		[Display(Name = "密碼")]
		[Required]
		[StringLength(70)]
		[DataType(DataType.Password)]
		public string CreatePassword { get; set; }  //辦帳號的時候用

		//public string Password { get; set; } <<之後雜湊用

		[Display(Name = "確認密碼")]
		[Required]
		[StringLength(70)]
		[Compare("CreatePassword", ErrorMessage = "必須與'密碼'欄位值相同")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }

		[Display(Name = "生日")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
		public DateTime Birthday { get; set; }

		[Display(Name = "信箱")]
		[Required]
		[StringLength(150)]
		[EmailAddress(ErrorMessage = "Email格式有誤")]
		public string Email { get; set; }

		[Display(Name = "手機")]
		[Required]
		[StringLength(10)]
		public string Phone { get; set; }

		[Display(Name = "創建時間")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
		public DateTime RegistrationDate { get; set; }

		[Display(Name = "大頭貼")]
		[StringLength(100)]
		public string IconImg { get; set; }
	}
}