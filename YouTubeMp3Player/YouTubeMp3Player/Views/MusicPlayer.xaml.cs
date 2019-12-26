using MediaManager;
using Plugin.CrossPlatformTintedImage.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YouTubeMp3Player.Models;

namespace YouTubeMp3Player.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MusicPlayer : ContentPage
    {
        Timer timer = new Timer(10);

        bool seeking = false;

        public MusicPlayer()
        {
            InitializeComponent();
            init();
        }

        void init()
        {
            timer.Elapsed += new ElapsedEventHandler(TimerHandler);

            playSomeShit();

            AudioSlider.MinimumTrackColor = Constants.ActiveOrangeColor;
            AudioSlider.MaximumTrackColor = Constants.PasiveColor;

            RepeatIco.TintColor = Constants.PasiveColor;
            AddToFavouritesIco.TintColor = Constants.PasiveColor;
            AddToPlaylistIco.TintColor = Constants.PasiveColor;
        }

        void TimerHandler(object source, ElapsedEventArgs e)
        {
            updateSlider(CrossMediaManager.Current.Position);
        }

        void playSomeShit()
        {
            trackInit("test.mp3");
        }

        async void trackInit(string fileName)
        {

            var mediaManager = await CrossMediaManager.Current.PlayFromResource("assets:///test.mp3");

            //timer.Start();

            //initSlider(mediaManager.Duration);
        }

        void initSlider(TimeSpan duration)
        {
            int seconds = (int)duration.TotalSeconds;
            AudioSlider.Maximum = seconds;
        }

        void updateSlider(TimeSpan possition)
        {
            int seconds = (int)possition.TotalSeconds;

            if (seconds > 0 && !seeking)
            {
                AudioSlider.Value = seconds;
            }
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

        private void PlayButton_Clicked(object sender, EventArgs e)
        {
            CrossMediaManager.Current.PlayPause();
        }

        private void AudioSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            /*seeking = true;

            TimeSpan time = new TimeSpan(0, 0, (int)AudioSlider.Value);
            CrossMediaManager.Current.SeekTo(time);

            seeking = false;*/
        }
    }
}