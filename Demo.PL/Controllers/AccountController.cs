using AutoMapper;
using Demo.DAL.Entities;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IMapper _mapper;


		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_mapper = mapper;
		}

		#region SignUp
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)//Server Side Validation
			{
				var user = new ApplicationUser()
				{
					UserName = model.Email.Split('@')[0],
					Email = model.Email,
					IsAgree = model.IsAgree,

				};


				//var mappedEmp = _mapper.Map<RegisterViewModel, ApplicationUser> (model);
				//ApplicationUser.Name = model.Email.Split('@')[0];
				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
					return RedirectToAction(nameof(Login));

				foreach (var error in result.Errors)
					ModelState.AddModelError(string.Empty, error.Description);
			}
			return View(model);
		}

		#endregion

		#region LogIn

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
					bool flag = await _userManager.CheckPasswordAsync(user, model.Password);
					if (flag)
					{
						var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
						if (result.Succeeded)
							return RedirectToAction("Index", "Home");
					}
					else
						ModelState.AddModelError(string.Empty, "Password is not Correct");
				}

				ModelState.AddModelError(string.Empty, "Email is Not EXisted");

			}

			return View(model);
		}


		#endregion

		#region SignOut
		public async Task<IActionResult> SignOut()
		{

			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}
		#endregion

		#region ForgetPassword

		public IActionResult ForgetPassword()
		{
			return View();
		}
		public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user != null)
				{

					var email = new Email()
					{
						Subject = "Reset Your Password",
						To = model.Email,
						Body = "ResetPasswordLink"
					};

					EmailSettings.SendEmail(email);
					return RedirectToAction(nameof();
				}
				ModelState.AddModelError(string.Empty, "Email is Not Existed");

			}
		}

		#endregion

		public IActionResult CheckYourInbox()
		{
			return View();
		}
	} 
}





