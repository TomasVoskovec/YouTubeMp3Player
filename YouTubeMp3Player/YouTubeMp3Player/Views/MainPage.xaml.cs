using MediaManager.Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using YouTubeMp3Player.Models;

namespace YouTubeMp3Player.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage(Track track = null, Models.Playlist playlist = null)
        {
            InitializeComponent();
            init(track, playlist);
        }

        void init(Track track = null, Models.Playlist playlist = null)
        {
            NavigationPage.SetHasNavigationBar(this, false);

            // Initialize pages 
            this.Children.Add(new MusicPlayer(track, playlist) { Title = "Music Player", IconImageSource = "player_ico.png" });
            this.Children.Add(new PlaylistsPage() { Title = "Playlists", IconImageSource = "playlist_ico.png" });
        }
    }
}