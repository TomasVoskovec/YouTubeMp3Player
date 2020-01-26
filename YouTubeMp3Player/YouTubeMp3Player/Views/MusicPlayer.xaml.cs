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
        List<Track> tracks = new List<Track>();

        public MusicPlayer()
        {
            InitializeComponent();
            init();
        }

        void init()
        {
            timer.Elapsed += new ElapsedEventHandler(TimerHandler);

            CrossMediaManager.Current.StateChanged += new MediaManager.Playback.StateChangedEventHandler(MediaChangedEventHandler);

            loadMusic();

            initPlaylist();

            CurrentTrackTime.MinimumTrackColor = Constants.ActiveOrangeColor;
            CurrentTrackTime.MaximumTrackColor = Constants.PasiveColor;

            AudioSlider.MinimumTrackColor = Constants.ActiveOrangeColor;
            AudioSlider.MaximumTrackColor = Constants.PasiveColor;

            RepeatIco.TintColor = Constants.PasiveColor;
            AddToFavouritesIco.TintColor = Constants.PasiveColor;
            AddToPlaylistIco.TintColor = Constants.PasiveColor;
        }

        void loadMusic()
        {
            IRoseMediaManager manager = DependencyService.Get<IRoseMediaManager>();

            tracks = manager.GetTracks();
        }

        void TimerHandler(object source, ElapsedEventArgs e)
        {
            CurrentTrackTime.Opacity = 1;
            AudioSlider.Opacity = 0;
            updateSlider(CrossMediaManager.Current.Position);
        }

        void MediaChangedEventHandler(object sender, MediaManager.Playback.StateChangedEventArgs e)
        {
            trackInit();

            if (CrossMediaManager.Current.IsPlaying())
            {
                PlayButtonImage.Source = "play_ico.png";
            }
            else
            {
                PlayButtonImage.Source = "pause_ico.png";
            }
        }

        async void initPlaylist()
        {
            List<string> tracksUris = new List<string>();
            foreach (Track track in tracks)
            {
                tracksUris.Add(track.Uri);
            }

            if(tracksUris.Count > 0)
            {
                var mediaManager = await CrossMediaManager.Current.Play(tracksUris[0]);

                trackInit();

                timer.Start();
            }
        }

        void trackInit()
        {
            // Slider functions
            initSlider(CrossMediaManager.Current.Queue.Current.Duration);
            // FrontEnd
            TrackTitle.Text = prepTrackTitle(CrossMediaManager.Current.Queue.Current.DisplayTitle);
        }

        string prepTrackTitle(string name)
        {
            if (name.Length > 50)
            {
                return name.Substring(0, 50) + "...";
            }
            return name;
        }

        void initSlider(TimeSpan duration)
        {
            int seconds = (int)duration.TotalSeconds;
            AudioSlider.Maximum = seconds;
            CurrentTrackTime.Maximum = seconds;
        }

        void updateSlider(TimeSpan possition)
        {
            int seconds = (int)possition.TotalSeconds;

            CurrentTrackTime.Value = seconds;
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

        private async void AudioSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            AudioSlider.Opacity = 1;
            CurrentTrackTime.Opacity = 0;

            int changedTimeNum = (int)AudioSlider.Value;
            TimeSpan changedTime = new TimeSpan(0, 0, changedTimeNum);

            await CrossMediaManager.Current.SeekTo(changedTime);
        }
    }
}