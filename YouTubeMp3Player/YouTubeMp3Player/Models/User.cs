using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeMp3Player.Models
{
    public class User
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }
        public string Username { get; set; }
        [Unique]
        public string Email { get; set; }
        public string Password { get; set; }

        public User() { }
        public User(string username, string password, string email)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
        }

        public User(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }

        public bool VertifyData()
        {
            if (this.Password == null || this.Password == null || this.Password == "" || this.Password == "" || this.Email == null || this.Email == null || this.Email == "" || this.Email == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}