using Plugin.CrossPlatformTintedImage.Abstractions;
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
    public partial class MusicPlayer : ContentPage
    {
        public MusicPlayer()
        {
            InitializeComponent();
            init();
        }

        void init()
        {
            AudioSlider.MinimumTrackColor = Constants.ActiveOrangeColor;
            AudioSlider.MaximumTrackColor = Constants.PasiveColor;

            RepeatIco.TintColor = Constants.PasiveColor;
            AddToFavouritesIco.TintColor = Constants.PasiveColor;
            AddToPlaylistIco.TintColor = Constants.PasiveColor;
        }

        void activateIcon(TintedImage icon)
        {
            if (icon.TintColor == null || icon.TintColor == Constants.PasiveColor)
            {
                icon.TintColor = Constants.ActiveOrangeColor;
            }
            else
            {
                icon.TintColor = Constants.PasiveColor;
            }
        }

        private void Repeat_Clicked(object sender, EventArgs e)
        {
            activateIcon(RepeatIco);
        }

        private void AddToFavourites_Clicked(object sender, EventArgs e)
        {
            activateIcon(AddToFavouritesIco);
        }

        private void AddToPlaylist_Clicked(object sender, EventArgs e)
        {
            activateIcon(AddToPlaylistIco);
        }
    }
}