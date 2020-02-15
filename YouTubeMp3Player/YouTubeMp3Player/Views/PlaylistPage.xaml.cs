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
        Dictionary<Frame, Track> tracksDictionary = new Dictionary<Frame, Track>();

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
                    currentPlaylist = playlist;

                    foreach (Track track in tracks)
                    {
                        if (track != null)
                        {
                            Image image = new Image() { Source = "track_ico.png", WidthRequest = 50, HeightRequest = 50 };
                            Label label = new Label() { Text = prepTrackTitle(track.Name), VerticalTextAlignment = TextAlignment.Center, HeightRequest = 50, Margin = new Thickness(10, 0)};

                            FlexLayout flexLayout = new FlexLayout();
                            flexLayout.Children.Add(image);
                            flexLayout.Children.Add(label);

                            TapGestureRecognizer gestureRecognizer = new TapGestureRecognizer();
                            gestureRecognizer.Tapped += trackButtonClicked;

                            Frame frame = new Frame() { HeightRequest = 50, Padding = 0, Content = flexLayout };
                            frame.GestureRecognizers.Add(gestureRecognizer);

                            tracksDictionary.Add(frame, track);
                            TracksContainer.Children.Add(frame);
                        }
                    }
                }
            }
        }

        string prepTrackTitle(string name)
        {
            if (name.Length > 40)
            {
                return name.Substring(0, 40) + "...";
            }
            return name;
        }

        async void trackButtonClicked(object sender, EventArgs args)
        {
            if (sender is Frame)
            {
                Frame key = (Frame)sender;
                Track track = tracksDictionary[key];
                
                await Navigation.PushModalAsync(new NavigationPage(new MainPage(track, currentPlaylist)));
            }
        }
    }
}