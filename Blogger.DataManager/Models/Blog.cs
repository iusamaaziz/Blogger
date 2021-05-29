using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blogger.DataManager.Models
{
	public class Blog
	{
		public int Id { get; set; }

		public DateTime CreationDate { get; set; }

		public string Title { get; set; }

		public string Content { get; set; }

		public string Username { get; set; }

	}
}
