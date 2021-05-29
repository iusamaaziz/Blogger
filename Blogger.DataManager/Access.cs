using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using Blogger.DataManager.Models;

namespace Blogger.DataManager
{
	public class Access
	{
		public static void SignUp(User user)
		{
			using (SqlConnection cnn = Provider.SqlConnection)
			{
				SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[User](Username, Email, Password)VALUES(@username, @email, @pass)", cnn);
				cmd.Parameters.AddWithValue("@username", user.Username);
				cmd.Parameters.AddWithValue("@email", user.Email);
				cmd.Parameters.AddWithValue("@pass", user.Password);
				cmd.ExecuteNonQuery();
			}
		}

		public static void UpdateProfile(User user)
		{
			using (SqlConnection cnn = Provider.SqlConnection)
			{
				SqlCommand cmd = new SqlCommand("UPDATE [dbo].[User] SET [Email] = @email, [Password] = @pass WHERE [Username] = @username", cnn);
				cmd.Parameters.AddWithValue("@username", user.Username);
				cmd.Parameters.AddWithValue("@email", user.Email);
				cmd.Parameters.AddWithValue("@pass", user.Password);
				cmd.ExecuteNonQuery();
			}
		}

		public static bool IsValidLogin(string username, string password)
		{
			using (SqlConnection cnn = Provider.SqlConnection)
			{
				SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[User] WHERE [Username] = @username AND [Password] = @pass", cnn);
				cmd.Parameters.AddWithValue("@username", username);
				cmd.Parameters.AddWithValue("@pass", password);
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
					return true;
				return false;
			}
		}

		public static void CreateBlog(Blog blog)
		{
			using (SqlConnection cnn = Provider.SqlConnection)
			{
				SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Blog](Title, Content, Username)VALUES(@title, @content, @username)", cnn);
				cmd.Parameters.AddWithValue("@title", blog.Title);
				cmd.Parameters.AddWithValue("@content", blog.Content);
				cmd.Parameters.AddWithValue("@username", blog.Username);
				cmd.ExecuteNonQuery();
			}
		}

		public static void EditBlog(Blog blog)
		{
			using (SqlConnection cnn = Provider.SqlConnection)
			{
				SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Blog] SET Title = @title, Content = @content WHERE [Id] = @id", cnn);
				cmd.Parameters.AddWithValue("@id", blog.Id);
				cmd.Parameters.AddWithValue("@title", blog.Title);
				cmd.Parameters.AddWithValue("@content", blog.Content);
				cmd.ExecuteNonQuery();
			}
		}

		public static void DeleteBlog(int id)
		{
			using (SqlConnection cnn = Provider.SqlConnection)
			{
				SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[Blog] WHERE [Id] = @id", cnn);
				cmd.Parameters.AddWithValue("@id", id);
				cmd.ExecuteNonQuery();
			}
		}

		public static List<Blog> GetBlogs()
		{
			List<Blog> blogs = new List<Blog>();
			using (SqlConnection cnn = Provider.SqlConnection)
			{
				SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Blog]", cnn);
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						Blog blog = new Blog()
						{
							Id = (int)reader["Id"],
							Title = (string)reader["Title"],
							Content = (string)reader["Content"],
							CreationDate = (DateTime)reader["CreationDate"],
							Username = (string)reader["Username"]
						};
						blogs.Add(blog);
					}
				}
			}
			return blogs;
		}

		public static User GetUser(string username)
		{
			using (SqlConnection cnn = Provider.SqlConnection)
			{
				SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[User] WHERE [Username] = @username", cnn);
				cmd.Parameters.AddWithValue("@username", username);
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						User user = new User()
						{
							Username = (string)reader["Username"],
							Password = (string)reader["Password"],
							Email = (string)reader["Email"]
						};
						return user;
					}
				}
				throw new RowNotInTableException();
			}
		}
	}
}
