using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace Blogger.DataManager.Models
{
	public class User
	{
		public string Username { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

	}
}
