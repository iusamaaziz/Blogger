using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Blogger.Models;

namespace Blogger.Misc
{
	/// <summary>
	/// Converts a Database Blog to MVC Blog Model
	/// </summary>
	public class Convertor
	{
		public static List<Blog> ReadAsMVCBlog(List<Blogger.DataManager.Models.Blog> data)
		{
			List<Blog> blogs = new List<Blog>();
			foreach (var item in data)
			{
				blogs.Add(new Blog() { Id = item.Id, Content = item.Content, CreationDate = item.CreationDate, Title = item.Title, Username = item.Username });
			}
			return blogs;
		}
	}
}
