using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace YouTubeMp3Player.Models
{
    public class Playlist
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string TracksSerialized { get; set; }

        public Playlist(string name, List<Track> tracks)
        {
            this.Name = name;
            this.TracksSerialized = JsonConvert.SerializeObject(tracks);
        }

        public Playlist(string name)
        {
            this.Name = name;
            this.TracksSerialized = JsonConvert.SerializeObject(new List<Track>());
        }

        public Playlist()
        {
            
        }

        public List<Track> GetTracks()
        {
            return JsonConvert.DeserializeObject<List<Track>>(this.TracksSerialized);
        }

        public void AddTrack(Track track)
        {
            List<Track> tracks = JsonConvert.DeserializeObject<List<Track>>(this.TracksSerialized);
            tracks.Add(track);
            this.TracksSerialized = JsonConvert.SerializeObject(tracks);
        }

        public void DeleteTrack(Track track)
        {
            List<Track> tracks = this.GetTracks();

            if (tracks != null)
            {
                Track findTrack = tracks.Find(x => x.Uri == track.Uri);
                if (findTrack != null)
                {
                    tracks.Remove(findTrack);
                    this.TracksSerialized = JsonConvert.SerializeObject(tracks);
                }
            }
        }
    }
}
