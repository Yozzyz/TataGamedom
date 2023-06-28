using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TataGamedom.Models.ViewModels;

namespace TataGamedom.Models.Dtos
{
	public class RegisterDto
	{
		public int Id { get; set; }

		public string Account { get; set; }

		public string CreatePassword { get; set; }

		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public DateTime Birthday { get; set; }
		public string Email { get; set; }

		public string Name { get; set; }
		public DateTime RegistrationDate { get; set; }
		public string Phone { get; set; }
		public bool IsConfirmed { get; set; }
		public string ConfirmCode { get; set; }
		public string IconImg { get; set; }
	}

	public static class RegisterExts
	{
		public static RegisterDto ToDto(this RegisterVM vm)
		{
			return new RegisterDto()
			{
				Id = vm.Id,
				Account = vm.Account,
				Password = vm.CreatePassword,
				Email = vm.Email,
				Name = vm.Name,
				Phone = vm.Phone,
				RegistrationDate = DateTime.Now,
				Birthday = vm.Birthday,
			};
		}
	}
}