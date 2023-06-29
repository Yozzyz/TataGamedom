using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TataGamedom.Models.Dtos;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.Infra;
using TataGamedom.Models.Infra.DapperRepositories;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.Services;
using TataGamedom.Models.ViewModels;

namespace TataGamedom.Controllers
{
    public class BackendMembersController : Controller
    {
		// GET: BackendMembers
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public ActionResult Login(LoginVM vm)
		{
			if (ModelState.IsValid == false) return View();
			Result result = ValidLogin(vm);

			if (result.IsSuccess != true)
			{
				ModelState.AddModelError("", result.ErrorMessage);
				return View(vm);
			}
			const bool rememberMe = false;

			var processResult = ProcessLogin(vm.Account, rememberMe);
			Response.Cookies.Add(processResult.cookie);
			return Redirect(processResult.returnUrl);
		}

		private Result ValidLogin(LoginVM vm)
		{
			IBackendMemberRepositiry repo = new BackendMemberDapperRepository();
			BackendMemberService service = new BackendMemberService(repo);
			return service.ValidLogin(vm.ToDto());
		}
		private (string returnUrl, HttpCookie cookie) ProcessLogin(string account, bool rememberMe)
		{
			var roles = string.Empty; // 在本範例, 沒有用到角色權限,所以存入空白

			// 建立一張認證票
			var ticket =
				new FormsAuthenticationTicket(
					1,          // 版本別, 沒特別用處
					account,
					DateTime.Now,   // 發行日
					DateTime.Now.AddDays(31), // 到期日
					rememberMe,     // 是否續存
					roles,          // userdata
					"/" // cookie位置
				);

			// 將它加密
			var value = FormsAuthentication.Encrypt(ticket);

			// 存入cookie
			var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, value);

			//var url = "/BackendMembers/Index/";
			// 取得return url
			var url = FormsAuthentication.GetRedirectUrl(account, true); //第二個引數沒有用處  
			return (url, cookie);  //會跳回MEMBER

		}

		public ActionResult Logout()
		{
			Session.Abandon();
			FormsAuthentication.SignOut();
			return Redirect("/BackendMembers/Login");
		}


		public ActionResult EditProfile()
		{
			var currentUserAccount = User.Identity.Name;

			var model = GetBackendMemberProfile(currentUserAccount);

			return View(model);
		}

		private EditProfileVM GetBackendMemberProfile(string account)
		{
			IBackendMemberRepositiry repo = new BackendMemberDapperRepository();
			BackendMemberService service = new BackendMemberService(repo);
			return service.GetBackendMemberProfile(account);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditProfile(EditProfileVM vm)
		{
			var currentUserAccount = User.Identity.Name;

			if (ModelState.IsValid == false) return View();

			Result updateResult = UpdateProfile(vm);
			if (updateResult.IsSuccess) return RedirectToAction("Index");

			ModelState.AddModelError(string.Empty, updateResult.ErrorMessage);
			return View(vm);
		}
		private Result UpdateProfile(EditProfileVM vm)
		{
			// 取得在db裡的原始記錄
			var db = new AppDbContext();

			var currentUserAccount = User.Identity.Name;
			var memberInDb = db.BackendMembers.FirstOrDefault(m => m.Account == currentUserAccount);
			if (memberInDb == null) return Result.Fail("找不到要修改的會員記錄");

			// 更新記錄
			memberInDb.Name = vm.Name;
			memberInDb.Email = vm.Email;
			memberInDb.Phone = vm.Phone;

			db.SaveChanges();

			return Result.Success();
		}

		[Authorize]
		public ActionResult EditPassword()
		{
			return View();
		}

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditPassword(EditPasswordVM vm)
		{
			if (ModelState.IsValid == false) return View(vm);
			var currentUserAccount = User.Identity.Name;
			Result result = ChangePassword(currentUserAccount, vm);
			if (result.IsSuccess == false)
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return View(vm);
			}
			return RedirectToAction("Index");
		}

		private Result ChangePassword(string account, EditPasswordVM vm)
		{
			var salt = HashUtility.GetSalt();
			var hashOrigPwd = HashUtility.ToSHA256(vm.OringinalPassword, salt);

			var db = new AppDbContext();

			var memberInDb = db.BackendMembers.FirstOrDefault(m => m.Account == account && m.Password == hashOrigPwd);
			if (memberInDb == null) return Result.Fail("找不到要修改的會員紀錄");

			var hashPwd = HashUtility.ToSHA256(vm.CreatePassword, salt);

			memberInDb.Password = hashPwd;
			db.SaveChanges();

			return Result.Success();
		}
	}
}