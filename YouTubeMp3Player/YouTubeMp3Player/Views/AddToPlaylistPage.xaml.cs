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

        public AddToPlaylistPage()
        {
            InitializeComponent();
            init();
        }

        void init()
        {
            updatePlaylists();
        }

        void updatePlaylists()
        {
            if (playlists.Count != 0)
            {
                foreach (Playlist playlist in playlists)
                {
                    Button playlistButton = new Button();
                    playlistButton.Text = playlist.Name;
                    playlistButton.Clicked += Playlist_Clicked;

                    PlaylistsScroll.Children.Add(playlistButton);
                    playlistButtons.Add(playlistButton, playlist);
                }
            }
        }

        private void Playlist_Clicked(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Playlist playlist = playlistButtons[(Button)sender];
                playlist.Tracks.Add(App.AddToPlaylistTrack);

                App.Playlists = playlists;
            }
        }

        private void AddNewPlaylist_Clicked(object sender, EventArgs e)
        {
            if (NewPlaylistName.Text != "")
            {
                Playlist newPlaylist = new Playlist(NewPlaylistName.Text);
                playlists.Add(newPlaylist);
                updatePlaylists();

                App.Playlists = playlists;
            }
        }
    }
}