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
    public class MembersController : Controller
    {
		private AppDbContext db = new AppDbContext();

		[Authorize]
		public ActionResult MembersList()
		{
			var Members = db.Members;
			return View(Members.ToList());
		}
		// GET: Members
		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Register(RegisterVM vm)
		{
			if (ModelState.IsValid == false) return View(vm);



			// 建立新會員
			Result result = RegisterMember(vm);
			if (result.IsSuccess)
			{

				// 生成email連結
				var urlTemplate = Request.Url.Scheme + "://" +
					Request.Url.Authority + "/" +
					"Members/ActiveRegister?memberId={0}&confirmCode={1}";
				// 若成功，寄送郵件
				Result mailResult = ProcessRegister(vm.Account, vm.Email, urlTemplate);

				if (mailResult.IsSuccess)
				{
					vm.RegistrationDate = DateTime.Now;
					return View("ConfirmRegister");
				}
				else
				{
					ModelState.AddModelError(string.Empty, mailResult.ErrorMessage);
					return View(vm);
				}
			}
			else
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return View(vm);
			}
		}

		private Result RegisterMember(RegisterVM vm)
		{
			IMemberRepository repo = new MemberDapperRepository();
			MemberService service = new MemberService(repo);
			return service.Register(vm.ToDto());
		}

		public ActionResult ActiveRegister(int memberId, string confirmCode)
		{
			//根據傳入值去DB查詢是否有這一筆，有就啟用此會員資格
			Result result = ActiveMember(memberId, confirmCode);

			return View();
		}

		private Result ActiveMember(int memberId, string confirmCode)
		{
			IMemberRepository repo = new MemberDapperRepository();
			MemberService service = new MemberService(repo);
			return service.ActiveMember(memberId, confirmCode);

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

			// 取得return url
			var url = FormsAuthentication.GetRedirectUrl(account, true); //第二個引數沒有用處

			return (url, cookie);

		}

		private Result ValidLogin(LoginVM vm)
		{
			IMemberRepository repo = new MemberDapperRepository();
			MemberService service = new MemberService(repo);
			return service.ValidLogin(vm.ToDto());
		}

		[Authorize]
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Logout()
		{
			Session.Abandon();
			FormsAuthentication.SignOut();
			return Redirect("/Members/Login");
		}

		[Authorize]
		public ActionResult EditProfile()
		{
			var currentUserAccount = User.Identity.Name;

			var model = GetMemberProfile(currentUserAccount);

			return View(model);
		}

		private EditProfileVM GetMemberProfile(string account)
		{
			IMemberRepository repo = new MemberDapperRepository();
			MemberService service = new MemberService(repo);
			return service.GetMemberProfile(account);
		}

		[Authorize]
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
			var memberInDb = db.Members.FirstOrDefault(m => m.Account == currentUserAccount);
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

			var memberInDb = db.Members.FirstOrDefault(m => m.Account == account && m.Password == hashOrigPwd);
			if (memberInDb == null) return Result.Fail("找不到要修改的會員紀錄");

			var hashPwd = HashUtility.ToSHA256(vm.CreatePassword, salt);

			memberInDb.Password = hashPwd;
			db.SaveChanges();

			return Result.Success();
		}

		public ActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public ActionResult ForgetPassword(ForgetPasswordVM vm)
		{
			if (ModelState.IsValid == false) return View(vm);

			//生成email裡的連結
			var urlTemplate = Request.Url.Scheme + "://" +
				Request.Url.Authority + "/" +
				"Members/ResetPassword?memberId={0}&confirmCode={1}";

			Result result = ProcessResetPassword(vm.Account, vm.Email, urlTemplate);

			if (result.IsSuccess == false)
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return View(vm);
			}

			return View("ConfirmForgetPassword");
		}

		private Result ProcessResetPassword(string account, string email, string urlTemplate)
		{
			var db = new AppDbContext();

			//檢查account, email正確性
			var memberInDb = db.Members.FirstOrDefault(m => m.Account == account);

			if (memberInDb == null) return Result.Fail("帳號或email錯誤"); //故意不告知確切錯誤原因

			if (string.Compare(email, memberInDb.Email, StringComparison.CurrentCultureIgnoreCase) != 0) return Result.Fail("帳號或email錯誤");

			//檢查IsConfirm必須是true，只有以啟用的帳號才能重設密碼
			if (memberInDb.IsConfirmed == false) return Result.Fail("您還沒有啟用本帳號，請先完成才能重設密碼");

			//更新紀錄，重給一個confirmCode
			var confirmCode = Guid.NewGuid().ToString("N");
			memberInDb.ConfirmCode = confirmCode;
			db.SaveChanges();

			//發email
			var url = string.Format(urlTemplate, memberInDb.Id, confirmCode);
			new EmailHelper().SendForgetPasswordEmail(url, memberInDb.Name, email);
			return Result.Success();
		}

		private Result ProcessRegister(string account, string email, string urlTemplate)
		{
			var db = new AppDbContext();

			//檢查account, email正確性
			var memberInDb = db.Members.FirstOrDefault(m => m.Account == account);

			if (memberInDb == null) return Result.Fail("帳號或email錯誤"); //故意不告知確切錯誤原因

			if (string.Compare(email, memberInDb.Email, StringComparison.CurrentCultureIgnoreCase) != 0) return Result.Fail("帳號或email錯誤");

			//更新紀錄，重給一個confirmCode
			var confirmCode = Guid.NewGuid().ToString("N");
			memberInDb.ConfirmCode = confirmCode;
			db.SaveChanges();

			//發email
			var url = string.Format(urlTemplate, memberInDb.Id, confirmCode);
			new EmailHelper().SendConfirmRegisterEmail(url, memberInDb.Name, email);
			return Result.Success();
		}

		public ActionResult ResetPassword()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public ActionResult ResetPassword(ResetPasswordVM vm, string confirmCode, int memberId)
		{
			if (ModelState.IsValid == false) return View(vm);
			Result result = ProcessChangePassword(memberId, confirmCode, vm.CreatePassword);

			if (result.IsSuccess == false)
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return View(vm);
			}
			return View("ConfirmResetPassword");
		}

		private Result ProcessChangePassword(int memberId, string confirmCode, string newPassword)
		{

			var db = new AppDbContext();

			//檢查memberId,confirmCode正確性
			var memberInDb = db.Members.FirstOrDefault(m => m.Id == memberId && m.ConfirmCode == confirmCode);

			if (memberInDb == null) return Result.Fail("找不到對應的會員紀錄");

			//更新密碼，並將confirmCode清空
			var salt = HashUtility.GetSalt();
			var encryptedPassword = HashUtility.ToSHA256(newPassword, salt);

			memberInDb.Password = encryptedPassword;
			memberInDb.ConfirmCode = null;

			db.SaveChanges();
			return Result.Success();
		}
	}
}