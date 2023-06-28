using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TataGamedom.Models.ViewModels
{
	public class EditProfileVM
	{
		public int Id { get; set; }

		[Required]
		[StringLength(256)]
		[EmailAddress(ErrorMessage = "Email格式有誤")]
		public string Email { get; set; }

		[Display(Name = "姓名")]
		[Required]
		[StringLength(30)]
		public string Name { get; set; }

		[Display(Name = "手機")]
		[StringLength(10)]
		public string Phone { get; set; }
	}
}
