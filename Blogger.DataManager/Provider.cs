using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Blogger.DataManager
{
	public class Provider
	{
		public static string ConnectionString
		{
			get
			{
				// TODO - Update Connection String
				return "Server=.;Database=Blogger-DB;Trusted_Connection=True;";
			}
		}

		public static SqlConnection SqlConnection
		{
			get
			{
				SqlConnection cnn = new SqlConnection(Provider.ConnectionString);
				cnn.Open();
				return cnn;
			}
		}
	}
}
