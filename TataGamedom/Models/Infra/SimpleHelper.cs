using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.ViewModels.Boards;

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