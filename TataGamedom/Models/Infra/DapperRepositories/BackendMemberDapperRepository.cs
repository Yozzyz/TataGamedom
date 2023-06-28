using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using TataGamedom.Models.Dtos;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.Interfaces;

namespace TataGamedom.Models.Infra.DapperRepositories
{
	public class BackendMemberDapperRepository : IBackendMemberRepositiry
	{
		private string _connstr;

		public BackendMemberDapperRepository()
		{
			_connstr = System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ToString();
		}
		public BackendMember ValidLogin(LoginDto dto)
		{
			using (var conn = new SqlConnection(_connstr))
			{
				string sql = @"SELECT * FROM BackendMembers WHERE Account=@Account";
				return conn.QueryFirstOrDefault<BackendMember>(sql, new { Account = dto.Account });
			}
		}
		//public bool ActiveBackendMember(int memberId, string confirmCode)
		//{
		//	throw new NotImplementedException();
		//}

		public bool ExistAccount(string account)
		{
			using (var conn = new SqlConnection(_connstr))
			{
				string sql = "SELECT Count(*) FROM BackendMembers WHERE Account = @Account";

				int count = conn.ExecuteScalar<int>(sql, new { Account = account });
				return count > 0;

			};
		}

		public BackendMember GetBackendMemberProfile(string account)
		{
			using (var conn = new SqlConnection(_connstr))
			{
				string sql = @"SELECT * FROM BackendMembers WHERE Account=@Account";
				return conn.QueryFirstOrDefault<BackendMember>(sql, new { Account = account });
			}
		}

		public void Register(RegisterDto dto)
		{
			throw new NotImplementedException();
			//先不創帳號
		}


	}
}