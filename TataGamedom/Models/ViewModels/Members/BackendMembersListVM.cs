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
		public int Id { get; set; }
		[Display(Name = "管理員名稱")]

		[StringLength(50)]
		public string Name { get; set; }
		[Display(Name = "管理員帳號")]

		[StringLength(30)]
		public string Account { get; set; }


		[StringLength(70)]
		public string Password { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
		public DateTime Birthday { get; set; }

		[Display(Name = "信箱")]
		[StringLength(150)]
		public string Email { get; set; }
		[Display(Name = "手機")]

		[StringLength(10)]
		public string Phone { get; set; }

		public int BackendMembersRoleId { get; set; }

		[Display(Name="權限名稱")]
		public string BackendMembersRoleName { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
		public DateTime? RegistrationDate { get; set; }
		[Display(Name = "帳號狀態")]
		public bool ActiveFlag { get; set; }

		public string ActiveFlagText
		{
			get
			{
				return ActiveFlag == true ? "使用中" : "停權中";
			}
		}

		public virtual BackendMembersRolesCode BackendMembersRolesCode { get; set; }
	}
}