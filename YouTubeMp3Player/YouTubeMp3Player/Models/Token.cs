using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeMp3Player.Models
{
    public class Token
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string AccessToken { get; set; }
        public string ErrorDescription { get; set; }
        public DateTime ExpireDate { get; set; }

        public Token() { }
    }
}