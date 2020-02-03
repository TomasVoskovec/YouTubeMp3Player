using Newtonsoft.Json;
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
        List<Playlist> playlists = App.PlaylistDatabase.GetAllPlaylists();
        Dictionary<Button, Playlist> playlistButtons = new Dictionary<Button, Playlist>();

        Track addToPlaylistTrack = App.AddToPlaylistTrack;

        public AddToPlaylistPage()
        {
            InitializeComponent();
            init();
        }

        void init()
        {
            //deleteAllPlaylists();
            //playlistTest();

            playlists = App.PlaylistDatabase.GetAllPlaylists();

            updatePlaylists();
        }

        void playlistTest()
        {
            App.PlaylistDatabase.SavePlaylist(new Playlist("playlistTest", new List<Track> { addToPlaylistTrack }));
        }

        void deleteAllPlaylists()
        {
            App.PlaylistDatabase.DeleteAllPlaylists();
            playlists = null;
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
                        if (playlist.TracksSerialized != null)
                        {
                            List<Track> tracks = JsonConvert.DeserializeObject<List<Track>>(playlist.TracksSerialized);
                            if (tracks != null && tracks.Count != 0)
                            {
                                if (tracks.Find(x => x.Uri == addToPlaylistTrack.Uri) != null)
                                {
                                    playlistButton.BackgroundColor = Constants.ActiveOrangeColor;
                                    playlistButton.TextColor = Constants.ButtonTextColor;
                                }
                            }
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
                List<Track> tracks = JsonConvert.DeserializeObject<List<Track>>(playlist.TracksSerialized);

                Track findTrack = tracks.Find(x => x.Uri == addToPlaylistTrack.Uri);

                if (findTrack != null)
                {
                    tracks.Remove(findTrack);
                }
                else
                {
                    tracks.Add(addToPlaylistTrack);
                }

                playlist.TracksSerialized = JsonConvert.SerializeObject(tracks);

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

                App.PlaylistDatabase.SavePlaylist(newPlaylist);
            }
        }
    }
}