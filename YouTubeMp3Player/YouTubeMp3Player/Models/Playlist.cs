using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeMp3Player.Models
{
    public class Playlist
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        [SQLiteNetExtensions.Attributes.TextBlob("TracksBlobbed")]
        public List<Track> Tracks { get; set; }
        public string TracksBlobbed { get; set; }

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
