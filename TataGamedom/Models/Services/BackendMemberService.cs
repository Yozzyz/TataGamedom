using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TataGamedom.Models.Dtos;
using TataGamedom.Models.Infra;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.ViewModels;

namespace TataGamedom.Models.Services
{
	public class BackendMemberService
	{
		private IBackendMemberRepositiry _repo;
		public BackendMemberService(IBackendMemberRepositiry repo)
		{
			_repo = repo;
		}

		public Result ValidLogin(LoginDto dto)
		{
			var backendmember = _repo.ValidLogin(dto);

			if (backendmember == null)
			{
				return Result.Fail("帳密錯誤");
			}
			var salt = HashUtility.GetSalt();
			var hashPwd = HashUtility.ToSHA256(dto.Password, salt);

			return string.Compare(backendmember.Password, hashPwd) == 0 ? Result.Success() : Result.Fail("帳密有誤");
		}

		public EditProfileVM GetBackendMemberProfile(string account)
		{
			var member = _repo.GetBackendMemberProfile(account);

			if (member == null)
			{
				return null;
			}

			var editProfileVM = new EditProfileVM
			{
				Id = member.Id,
				Email = member.Email,
				Name = member.Name,
				Phone = member.Phone
			};

			return editProfileVM;
		}
	}
}