using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeMp3Player.Models
{
    public class Track
    {
        public string Name { get; set; }
        public string Uri { get; set; }

        public Track(string name, string uri)
        {
            this.Name = name;
            this.Uri = uri;
        }

        public Track()
        {

        }
    }
}
