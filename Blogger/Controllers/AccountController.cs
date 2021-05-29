using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Blogger.DataManager;
using Blogger.Misc;
using Blogger.Models;

using Microsoft.AspNetCore.Mvc;

namespace Blogger.Controllers
{
	public class AccountController : Controller
	{
		[HttpGet]
		public IActionResult Login()
		{
			return View("Login");
		}

		[HttpPost]
		public IActionResult Login(Login login)
		{
			User user = new User() { Username = login.Username, Password = login.Password };
			if (ModelState.IsValid && Access.IsValidLogin(login.Username, login.Password))
			{
				HttpContext.Response.Cookies.Append("username", user.Username);
				return View("~/Views/Home/Index.cshtml", (Convertor.ReadAsMVCBlog(Access.GetBlogs()), HttpContext.Request.Cookies["username"]));
			}
			return View(new User());
		}

		[HttpGet]
		public ViewResult SignUp()
		{
			return View();
		}

		[HttpPost]
		public IActionResult SignUp(User user)
		{
			if (ModelState.IsValid)
			{
				Access.SignUp(new DataManager.Models.User() { Username = user.Username, Password = user.Password, Email = user.Email });
				return View("Login");
			}
			return View();
		}

		public IActionResult Logout()
		{
			HttpContext.Response.Cookies.Delete("username");
			return View("Login");
		}
	}
}
