using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TataGamedom.Models.Dtos;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.Interfaces;

namespace TataGamedom.Models.Infra.DapperRepositories
{
	public class MemberDapperRepository : IMemberRepository
	{
		private string _connstr;

		public MemberDapperRepository()
		{
			_connstr = System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ToString();
		}
		public bool ActiveMember(int memberId, string confirmCode)
		{
			//根據傳入值去DB查詢是否有這一筆，找不到就return，若isConfirmed不是0 或不符合 就return
			//select * from Members where isConfirmed = 0 and confirmCode ='xxx'

			using (var conn = new SqlConnection(_connstr))
			{
				string sql = @"UPDATE Members
SET IsConfirmed = 1, ConfirmCode = NULL
WHERE Id = @Id AND ConfirmCode = @ConfirmCode AND IsConfirmed = 0;";
				var rowsAffected = conn.Execute(sql, new { Id = memberId, ConfirmCode = confirmCode });
				if (rowsAffected == 0)
				{
					return true; //就算找不到也傳回成功，不讓惡意使用者得知驗證的結果
				}
				return rowsAffected > 0;
			}
		}

		public bool ExistAccount(string account)
		{
			using (var conn = new SqlConnection(_connstr))
			{
				string sql = "SELECT Count(*) FROM Members WHERE Account = @Account";

				int count = conn.ExecuteScalar<int>(sql, new { Account = account });
				return count > 0;

			};
		}

		public Member GetMemberProfile(string account)
		{
			using (var conn = new SqlConnection(_connstr))
			{
				string sql = @"SELECT * FROM Members WHERE Account=@Account";
				return conn.QueryFirstOrDefault<Member>(sql, new { Account = account });
			}
		}

		public void Register(RegisterDto dto)
		{
			using (var conn = new SqlConnection(_connstr))
			{
				string sql = @"INSERT INTO Members (Account, Password,Birthday, Email, Name, Phone, RegistrationDate, IsConfirmed, ConfirmCode, ActiveFlag)
VALUES (@Account, @Password,@Birthday, @Email, @Name, @Phone, GETDATE(), @IsConfirmed, @ConfirmCode, 1)";

				conn.Execute(sql, dto);
			}


		}

		//public Result UpdateProfile(EditProfileDto dto)
		//{
		//	using (var conn = new SqlConnection(_connstr))
		//	{
		//		string sql = @"UPDATE Members SET Email = @Email, Name=@Name,Mobile=@Mobile WHERE Account=@Account";
		//		var rowAffected = conn.Execute(sql, new {Account = dto.})
		//	}
		//}

		public Member ValidLogin(LoginDto dto)
		{
			using (var conn = new SqlConnection(_connstr))
			{
				string sql = @"SELECT * FROM Members WHERE Account=@Account";
				return conn.QueryFirstOrDefault<Member>(sql, new { Account = dto.Account });
			}
		}
	}
}