using AssignDAL.Models;
using AssignPL.Helpers;
using AssignPL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AssignPL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
        #region Register
        public IActionResult Register()
        {
            return View();
        }
		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if(ModelState.IsValid) {
				var user = new ApplicationUser()
				{
					FName = model.FName,
					LName = model.FName,
                    UserName = model.Email.Split('@')[0],
					Email = model.Email,
					IsAgree = model.IsAgree,

				};
				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
					return RedirectToAction(nameof(Login));
				foreach(var error in result.Errors)
					ModelState.AddModelError(string.Empty, error.Description);
			}
			return View(model);
		}
		#endregion

		#region Login
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user != null)
				{
					var check = await _userManager.CheckPasswordAsync(user, model.Password);
					if (check)
					{
						var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe,false);
						if (result.Succeeded)
							return RedirectToAction("Index", "Home");
					}
					ModelState.AddModelError(string.Empty, "Password is not Correct!");

				}
				ModelState.AddModelError(string.Empty, "Email is not Existed");
			}
			return View(model);
		}

		#endregion

		#region Sign Out
		public new async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}

		#endregion

		#region Forget Password
		public IActionResult ForgetPassword()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if(user != null)
				{
					var token = _userManager.GeneratePasswordResetTokenAsync(user);
					var passwordResetLink = Url.Action("ResetPassword","Account",new { email = user.Email, token = token },"https", "localhost:44359");
					var email = new Email()
					{
						Subject = "Reset Password",
						To = user.Email,
						Body = "passwordResetLink"
					};
					EmailSettings.SendEmail(email);
					return RedirectToAction(nameof(CheckYourInbox));
				}
				ModelState.AddModelError(string.Empty, "Email is not Existed");

			}
			return View(model);
		}
		public IActionResult CheckYourInbox()
		{
			return View();
		}
		#endregion
	}
}
