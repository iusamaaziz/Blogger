using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blogger.Models
{
	public class Blog
	{
		public int Id { get; set; }

		public DateTime CreationDate { get; set; }

		[Required(ErrorMessage = "Title can't be empty.")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Content can't be empty.")]
		[MinLength(10, ErrorMessage = "Content should have at-least 10 characters.")]
		public string Content { get; set; }

		public string Username { get; set; }

	}
}
