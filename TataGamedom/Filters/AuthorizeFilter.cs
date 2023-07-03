using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TataGamedom.Filters
{

	//設權限 抓 backendmembers內的BackendMembersRoleId
	[Flags]
	public enum UserRole
	{
		Visitor = 0, Tataboss = 1, NNtata = 2, Producttata = 3, Ordertata = 4, Newstata = 5, Faqtata = 6, Memberstata = 7
	}

	public class AuthorizeFilter : AuthorizeAttribute
	{
		private readonly UserRole[] allowedRoles;

		public AuthorizeFilter(params UserRole[] roles)
		{
			allowedRoles = roles;
		}


		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}

			// 取得用戶角色
			UserRole userRole = UserRole.Visitor; // 預設角色
			if (httpContext.Session["BackendMembersRoleId"] != null)
			{
				// 從 Session 取得角色
				int roleId = Convert.ToInt32(httpContext.Session["BackendMembersRoleId"]);
				if (Enum.IsDefined(typeof(UserRole), roleId))
				{
					userRole = (UserRole)roleId;
				}
			}

			// 檢查使用者角色是否在 allowedRoles 中
			bool hasAllowedRole = allowedRoles.Contains(userRole);
			return hasAllowedRole;
		}

		/// <summary>
		/// 權限檢查失敗時動作
		/// </summary>
		/// <param name="filterContext"></param>
		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			// 當權限檢查失敗時，跳頁至權限不足頁
			filterContext.Result = new RedirectResult("~/BackendMembers/NotAuthorize");
		}
	}
}