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
            if (this.TracksSerialized != null)
            {
                return JsonConvert.DeserializeObject<List<Track>>(this.TracksSerialized);
            }
            else
            {
                return null;
            }
        }

        public void AddTrack(Track track)
        {
            if (this.TracksSerialized != null)
            {
                List<Track> tracks = JsonConvert.DeserializeObject<List<Track>>(this.TracksSerialized);
                tracks.Add(track);
                this.TracksSerialized = JsonConvert.SerializeObject(tracks);
            }
            else
            {
                this.TracksSerialized = JsonConvert.SerializeObject(new List<Track> { track });
            }
        }

        public bool ContainsTrack(Track track)
        {
            if (track != null)
            {

                if (this != null)
                {
                    List<Track> favTracks = this.GetTracks();

                    if (this.TracksSerialized != null)
                    {
                        foreach (Track favTrack in favTracks)
                        {
                            if (favTrack != null)
                            {
                                if (favTrack.Uri == track.Uri)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public void DeleteTrack(Track track)
        {
            List<Track> tracks = this.GetTracks();

            if (tracks != null && tracks.Count != 0)
            {
                foreach (Track trackX in tracks)
                {
                    if (trackX != null)
                    {
                        if (trackX.Uri == track.Uri)
                        {
                            tracks.Remove(trackX);
                            if (tracks == null || tracks.Count == 0)
                            {
                                this.TracksSerialized = null;
                            }
                            else
                            {
                                this.TracksSerialized = JsonConvert.SerializeObject(tracks);
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}
