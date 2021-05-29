using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

using Blogger.DataManager;
using Blogger.Misc;
using Blogger.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blogger.Controllers
{
	//[Authorize]
	public class HomeController : Controller
	{
		[HttpGet]
		public ActionResult Index()
		{
			if (HttpContext.Request.Cookies["username"] == null)
				return RedirectToAction("Login", "Account");
			return View((Convertor.ReadAsMVCBlog(Access.GetBlogs()), HttpContext.Request.Cookies["username"]));
		}

		[HttpPost]
		public ViewResult Create(Blog blog)
		{
			if (ModelState.IsValid)
			{
				blog.CreationDate = DateTime.Now;
				blog.Username = HttpContext.Request.Cookies["username"];
				Access.CreateBlog(new DataManager.Models.Blog() { Title = blog.Title, Content = blog.Content, Username = blog.Username });
				return View("Index", (Convertor.ReadAsMVCBlog(Access.GetBlogs()), HttpContext.Request.Cookies["username"]));
			}
			return View();
		}

		[HttpGet]
		public ViewResult Create()
		{
			return View();
		}

		[HttpGet]
		public ViewResult Edit(int id)
		{
			return View("Edit", Convertor.ReadAsMVCBlog(Access.GetBlogs()).Find(match => match.Id == id));
		}

		[HttpPost]
		public ViewResult Edit(Blog blog)
		{
			if (ModelState.IsValid)
			{
				Access.EditBlog(new DataManager.Models.Blog() { Title = blog.Title, Content = blog.Content, Username = blog.Username });
				return View("Index", (Convertor.ReadAsMVCBlog(Access.GetBlogs()), HttpContext.Request.Cookies["username"]));
			}
			return View("Edit", blog);
		}

		[HttpGet]
		public ViewResult Customize(int id)
		{
			var item = Convertor.ReadAsMVCBlog(Access.GetBlogs()).Find(match => match.Id == id);
			return View("Customize", item);
		}

		[HttpPost]
		public ViewResult Customize(Blog blog)
		{
			if (ModelState.IsValid)
			{
				Access.EditBlog(new DataManager.Models.Blog() { Title = blog.Title, Content = blog.Content, Username = blog.Username });
				return View("Index", (Convertor.ReadAsMVCBlog(Access.GetBlogs()), HttpContext.Request.Cookies["username"]));
			}
			return View("Edit", blog);
		}

		public ViewResult Delete(int id)
		{
			Access.DeleteBlog(id);
			return View("Index", (Convertor.ReadAsMVCBlog(Access.GetBlogs()), HttpContext.Request.Cookies["username"]));
		}



		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
