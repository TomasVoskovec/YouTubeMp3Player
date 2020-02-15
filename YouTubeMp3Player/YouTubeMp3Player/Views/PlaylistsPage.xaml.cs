using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YouTubeMp3Player.Models;

namespace YouTubeMp3Player.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaylistsPage : ContentPage
    {
        List<Playlist> playlists = App.PlaylistDatabase.GetAllPlaylists();
        Dictionary<StackLayout, Playlist> playlistsDictionary = new Dictionary<StackLayout, Playlist>();

        public PlaylistsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            playlists = App.PlaylistDatabase.GetAllPlaylists();
            init();
        }

        void init()
        {
            DisplayInfo mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            double width = mainDisplayInfo.Width / mainDisplayInfo.Density / 2;

            PlaylistsContainer.Children.Clear();

            if (playlists != null && playlists.Count != 0)
            {
                foreach (Playlist playlist in playlists)
                {
                    Image image = new Image() { Source = "track_ico.png", WidthRequest = width * 0.9 };
                    Label label = new Label() { Text = playlist.Name, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center, WidthRequest = width * 0.9 };

                    TapGestureRecognizer gestureRecognizer = new TapGestureRecognizer();
                    gestureRecognizer.Tapped += onPlaylistClicked;

                    StackLayout stack = new StackLayout();
                    stack.GestureRecognizers.Add(gestureRecognizer);
                    stack.Margin = width * 0.05;
                    stack.Children.Add(image);
                    stack.Children.Add(label);

                    playlistsDictionary.Add(stack, playlist);
                    PlaylistsContainer.Children.Add(stack);
                }
            }
        }

        void onPlaylistClicked (object sender, EventArgs args)
        {
            if (sender is StackLayout)
            {
                StackLayout stackKey = (StackLayout)sender;
                Navigation.PushModalAsync(new PlaylistPage(playlistsDictionary[stackKey]));
            }
        }
    }
}