using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using YouTubeMp3Player.Models;

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
    }
}
