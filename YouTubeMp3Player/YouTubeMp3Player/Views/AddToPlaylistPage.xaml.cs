using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YouTubeMp3Player.Data;
using YouTubeMp3Player.Models;

namespace YouTubeMp3Player.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddToPlaylistPage : ContentPage
    {
        List<Playlist> playlists = App.Playlists;
        Dictionary<Button, Playlist> playlistButtons = new Dictionary<Button, Playlist>();

        Track addToPlaylistTrack = App.AddToPlaylistTrack;

        public AddToPlaylistPage()
        {
            InitializeComponent();
            init();
        }

        void init()
        {
            updatePlaylists();
        }

        void deleteAllPlaylists()
        {
            foreach (Playlist playlist in playlists)
            {
                App.PlaylistDatabase.DeletePlaylist(playlist.Id);
            }

            App.Playlists = new List<Playlist>();
            playlists = App.Playlists;
        }

        void updatePlaylists()
        {
            PlaylistsScroll.Children.Clear();
            if(playlists != null)
            {
                if (playlists.Count != 0)
                {
                    foreach (Playlist playlist in playlists)
                    {
                        Button playlistButton = new Button();
                        playlistButton.Text = playlist.Name;
                        playlistButton.Clicked += Playlist_Clicked;
                        if (playlist.Tracks.Contains(addToPlaylistTrack))
                        {
                            playlistButton.BackgroundColor = Constants.ActiveOrangeColor;
                            playlistButton.TextColor = Constants.ButtonTextColor;
                        }

                        PlaylistsScroll.Children.Add(playlistButton);
                        playlistButtons.Add(playlistButton, playlist);
                    }
                }
            }
        }

        private void Playlist_Clicked(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Playlist playlist = playlistButtons[(Button)sender];
                if(playlist.Tracks.Contains(addToPlaylistTrack))
                {
                    playlist.Tracks.Remove(addToPlaylistTrack);
                }
                else
                {
                    playlist.Tracks.Add(addToPlaylistTrack);
                }

                App.Playlists = playlists;
                App.PlaylistDatabase.SavePlaylist(playlist);
            }

            updatePlaylists();
        }

        private void AddNewPlaylist_Clicked(object sender, EventArgs e)
        {
            if (NewPlaylistName.Text != "")
            {
                Playlist newPlaylist = new Playlist(NewPlaylistName.Text, new List<Track>{addToPlaylistTrack});
                playlists.Add(newPlaylist);
                updatePlaylists();

                App.Playlists = playlists;
                App.PlaylistDatabase.SavePlaylist(newPlaylist);
            }
        }
    }
}