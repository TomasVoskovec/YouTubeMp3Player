using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeMp3Player.Models
{
    public class RequestToken
    {
        public string RequestedToken { get; set; }
        public string ErrorDescription { get; set; }

        public RequestToken(string requestedToken, string errorDescription)
        {
            this.RequestedToken = requestedToken;
            this.ErrorDescription = errorDescription;
        }
    }
}