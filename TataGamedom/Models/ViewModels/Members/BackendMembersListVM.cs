using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TataGamedom.Models.EFModels;

namespace TataGamedom.Models.ViewModels.Members
{
	public class BackendMembersListVM
	{
		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		[Required]
		[StringLength(30)]
		public string Account { get; set; }

		public DateTime Birthday { get; set; }

		[Required]
		[StringLength(150)]
		public string Email { get; set; }

		[Required]
		[StringLength(10)]
		public string Phone { get; set; }

		public int BackendMembersRoleId { get; set; }

		public DateTime? RegistrationDate { get; set; }

		public bool ActiveFlag { get; set; }
	
		public virtual BackendMembersRolesCode BackendMembersRolesCode { get; set; }
	}
}