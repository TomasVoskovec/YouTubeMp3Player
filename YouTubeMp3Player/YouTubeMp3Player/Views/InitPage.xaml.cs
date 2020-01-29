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
    public partial class InitPage : ContentPage
    {
        public InitPage()
        {
            InitializeComponent();
            init();
        }

        async void init()
        {
            // App init actions
            if (GetJsonPlaylists() != null)
            {
                App.Playlists = GetJsonPlaylists();
            }

            // Main page
            await this.Navigation.PushModalAsync(new NavigationPage(new MainPage()));
        }

        public List<Playlist> GetJsonPlaylists()
        {
            BindingContext = this;
            var assembly = typeof(InitPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("YouTubeMp3Player.Playlists.json");

            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();

                return JsonConvert.DeserializeObject<List<Models.Playlist>>(json);
            }
        }
    }
}