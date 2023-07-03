using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TataGamedom.Models.EFModels;

namespace TataGamedom.Models.Infra
{
	public class SimpleHelper
	{
		private AppDbContext db = new AppDbContext();
		public int FindBackendmemberIdByAccount(string account)
		{
			var backendMember = db.BackendMembers.FirstOrDefault(x => x.Account == account);
			int id = backendMember?.Id ?? 0;
			return id;
		}
	}
}