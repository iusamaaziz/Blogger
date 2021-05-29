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
	public class ProfileController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			string username = HttpContext.Request.Cookies["username"];
			var item = Access.GetUser(username);
			return View("~/Views/Profile/Profile.cshtml", new User() { Username = item.Username, Password = item.Password, Email = item.Email });
		}

		[HttpPost]
		public ViewResult Update(User user)
		{
			Access.UpdateProfile(new DataManager.Models.User() { Username = user.Username, Email = user.Email, Password = user.Password });
			return View("~/Views/Home/Index.cshtml", (Convertor.ReadAsMVCBlog(Access.GetBlogs()), HttpContext.Request.Cookies["username"]));
		}
	}
}
