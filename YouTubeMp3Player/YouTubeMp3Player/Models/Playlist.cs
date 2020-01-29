using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeMp3Player.Models
{
    public class Playlist
    {
        public string Name { get; set; }
        public List<Track> Tracks { get; set; }

        public Playlist(string name, List<Track> tracks)
        {
            this.Name = name;
            this.Tracks = tracks;
        }

        public Playlist(string name)
        {
            this.Name = name;
            this.Tracks = new List<Track>();
        }

        public Playlist()
        {

        }
    }
}
