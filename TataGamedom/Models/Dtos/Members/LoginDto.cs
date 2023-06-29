using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TataGamedom.Models.ViewModels;

namespace TataGamedom.Models.Dtos
{
	public class LoginDto
	{
		public string Account { get; set; }

		public string Password { get; set; }
	}

	public static class LoginExts
	{
		public static LoginDto ToDto(this LoginVM vm)
		{
			return new LoginDto { Account = vm.Account, Password = vm.Password };
		}
	}
}