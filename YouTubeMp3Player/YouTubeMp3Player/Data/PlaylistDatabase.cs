using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using YouTubeMp3Player.Models;
using Newtonsoft.Json;
using System.Linq;

namespace YouTubeMp3Player.Data
{
    public class PlaylistDatabase
    {
        SQLiteConnection database;

        public PlaylistDatabase()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Playlist>();
        }

        public List<Playlist> GetAllPlaylists()
        {
            if (database.Table<Playlist>().Count() == 0)
            {
                return null;
            }
            else
            {
                return database.Table<Playlist>().ToList();
            }
        }

        public int SavePlaylist(Playlist playlist)
        {
            if (playlist.Id != 0)
            {
                database.Update(playlist);
                return playlist.Id;
            }
            else
            {
                return database.Insert(playlist);
            }
        }

        public int DeletePlaylist(int id)
        {
            return database.Delete<Playlist>(id);
        }

        public void DeleteAllPlaylists()
        {
            database.DeleteAll<Playlist>();
        }

        public void AddToFavourites(Track track)
        {
            Playlist favourites = GetFavourites();

            favourites.AddTrack(track);
            database.Update(favourites);
        }

        public Playlist GetFavourites()
        {
            Playlist favourites = database.Table<Playlist>().ToList().Find(x => x.Name == "Favourites");

            if (favourites == null)
            {
                favourites = new Playlist("Favourites");
                database.Insert(favourites);
            }

            return favourites;
        }

        public void DeleteFromFavourites(Track track)
        {
            if (IsFavourite(track))
            {
                Playlist fav = GetFavourites();
                fav.DeleteTrack(track);
                
                database.Update(fav);
            }
        }

        public bool IsFavourite (Track track)
        {
            if (track != null)
            {
                Playlist fav = GetFavourites();

                if (fav != null)
                {
                    List<Track> favTracks = fav.GetTracks();

                    if (fav.TracksSerialized != null)
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
    }
}
