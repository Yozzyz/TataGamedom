using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TataGamedom.Models.Dtos;
using TataGamedom.Models.EFModels;

namespace TataGamedom.Models.Interfaces
{
	public interface IBackendMemberRepositiry
	{
		void Register(RegisterDto dto);
		bool ExistAccount(string account);//判斷帳號是否存在

		//判斷ID不等於excludeId的紀錄裡，帳號是否存在
		//如果不允許更新帳號，這隻就不必寫
		//bool ExistAccount(string account, int excludeId);

		//bool ActiveBackendMember(int memberId, string confirmCode);

		BackendMember ValidLogin(LoginDto dto);

		BackendMember GetBackendMemberProfile(string account);
	}
}