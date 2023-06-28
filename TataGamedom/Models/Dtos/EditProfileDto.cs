using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TataGamedom.Models.ViewModels;

namespace TataGamedom.Models.Dtos
{
		public class EditProfileDto
		{
			public int Id { get; set; }

			public string Email { get; set; }

			public string Name { get; set; }

			public string Phone { get; set; }
		}

		public static class EditProfileExts
		{
			public static EditProfileDto ToDto(this EditProfileVM vm)
			{
				return new EditProfileDto
				{
					Id = vm.Id,
					Email = vm.Email,
					Name = vm.Name,
					Phone = vm.Phone
				};
			}
		}
}