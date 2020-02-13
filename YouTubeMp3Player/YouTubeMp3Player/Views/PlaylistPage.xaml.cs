using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YouTubeMp3Player.Models;

namespace YouTubeMp3Player.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaylistPage : ContentPage
    {
        Playlist currentPlaylist;
        Dictionary<Button, Track> tracksDictionary = new Dictionary<Button, Track>();

        public PlaylistPage(Playlist playlist)
        {
            InitializeComponent();
            init(playlist);
        }

        void init(Playlist playlist)
        {
            if (playlist != null && playlist.TracksSerialized != null)
            {
                List<Track> tracks = playlist.GetTracks();

                if (tracks != null && tracks.Count != 0)
                {
                    foreach (Track track in tracks)
                    {
                        if (track != null)
                        {
                            currentPlaylist = playlist;

                            Button trackButton = new Button();
                            trackButton.Text = track.Name;
                            trackButton.Clicked += trackButtonClicked;

                            tracksDictionary.Add(trackButton, track);
                            TracksContainer.Children.Add(trackButton);
                        }
                    }
                }
            }
        }

        void trackButtonClicked(object sender, EventArgs args)
        {
            if (sender is Button)
            {
                Button buttonKey = (Button)sender;
                Track track = tracksDictionary[buttonKey];

                Navigation.PushModalAsync(new MusicPlayer(track, currentPlaylist));
            }
        }
    }
}