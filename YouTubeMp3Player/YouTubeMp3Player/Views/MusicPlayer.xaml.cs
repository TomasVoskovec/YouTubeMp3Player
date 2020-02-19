using MediaManager;
using Plugin.CrossPlatformTintedImage.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using VideoLibrary;
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

        string playlistName;
        bool isLooping = false;
        bool isInitialized = false;

        public MusicPlayer()
        {
            InitializeComponent();
            init();
        }

        public MusicPlayer(Track currentTrack, Playlist playlist)
        {
            InitializeComponent();
            init(currentTrack, playlist);
        }

        void init(Track track = null, Playlist playlist = null)
        {
            timer.Elapsed += new ElapsedEventHandler(TimerHandler);

            CrossMediaManager.Current.StateChanged += new MediaManager.Playback.StateChangedEventHandler(MediaChangedEventHandler);
            CrossMediaManager.Current.MediaItemFinished += new MediaManager.Playback.MediaItemFinishedEventHandler(MediaItemFinished);

            if (track == null || playlist == null)
            {
                loadMusic();
                initPlaylist();
            }
            else
            {
                initPlaylist(track, playlist);
                playlistName = playlist.Name;
            }

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
            updateSlider(CrossMediaManager.Current.Position);
        }

        void MediaChangedEventHandler(object sender, MediaManager.Playback.StateChangedEventArgs e)
        {
            trackInit();

            if (CrossMediaManager.Current.IsPlaying())
            {
                PlayButtonImage.Source = "pause_ico.png";
            }
            else
            {
                PlayButtonImage.Source = "play_ico.png";
            }
        }

        void MediaItemFinished(object sender, MediaManager.Media.MediaItemEventArgs e)
        {
            trackInit();
        }

        async void initPlaylist(Track currentTrack = null, Playlist playlist = null)
        {
            List<string> tracksUris = new List<string>();

            if (currentTrack != null && playlist != null)
            {
                tracks = playlist.GetTracks();
            }

            foreach (Track track in tracks)
            {
                tracksUris.Add(track.Uri);
            }
            
            await CrossMediaManager.Current.Play("https://ia800806.us.archive.org/15/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3");
            await CrossMediaManager.Current.Play(tracksUris);

            if (currentTrack != null && playlist != null)
            {
                int trackIndex = tracksUris.FindIndex(x => x == currentTrack.Uri);
                CrossMediaManager.Current.Queue.Move(CrossMediaManager.Current.Queue.CurrentIndex, trackIndex);
            }

            trackInit();
            timer.Start();
        }

        void trackInit()
        {
            // Slider functions
            initSlider(CrossMediaManager.Current.Queue.Current.Duration);

            // FrontEnd
            PlaylistName.Text = prepPlaylistInfo(playlistName);
            TrackTitle.Text = prepTrackTitle(CrossMediaManager.Current.Queue.Current.DisplayTitle);
            isFavoriteAction();
            isInitialized = true;
        }

        string prepPlaylistInfo(string name = null)
        {
            if (name == null)
            {
                return "";
            }
            return "Hudba z playlistu \"" + name + "\"";
        }

        void isFavoriteAction()
        {
            if (App.PlaylistDatabase.IsFavourite(getCurrentTrack()))
            {
                activateIcon(AddToFavouritesIco);
            }
            else
            {
                deactivateIcon(AddToFavouritesIco);
            }
        }

        Track getCurrentTrack()
        {
            return tracks.Find(x => x.Uri == CrossMediaManager.Current.Queue.Current.MediaUri);
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
            icon.TintColor = Constants.ActiveOrangeColor;
        }

        void deactivateIcon(TintedImage icon)
        {
            icon.TintColor = Constants.PasiveColor;
        }

        private void Repeat_Clicked(object sender, EventArgs e)
        {
            if (isInitialized)
            {
                isLooping = !isLooping;

                if (isLooping)
                {
                    CrossMediaManager.Current.RepeatMode = MediaManager.Playback.RepeatMode.One;
                    activateIcon(RepeatIco);
                }
                else
                {
                    CrossMediaManager.Current.RepeatMode = MediaManager.Playback.RepeatMode.Off;
                    deactivateIcon(RepeatIco);
                }
            }
        }

        private void AddToFavourites_Clicked(object sender, EventArgs e)
        {
            if (isInitialized)
            {
                if (tracks != null || tracks.Count != 0)
                {
                    Track track = tracks.Find(x => x.Uri == CrossMediaManager.Current.Queue.Current.MediaUri);

                    if (!App.PlaylistDatabase.IsFavourite(track))
                    {
                        App.PlaylistDatabase.AddToFavourites(track);
                        activateIcon(AddToFavouritesIco);
                    }
                    else
                    {
                        App.PlaylistDatabase.DeleteFromFavourites(track);
                        deactivateIcon(AddToFavouritesIco);
                    }
                }
            }
        }

        private void AddToPlaylist_Clicked(object sender, EventArgs e)
        {
            if (isInitialized)
            {
                string trackUri = CrossMediaManager.Current.Queue.Current.MediaUri;

                if (File.Exists(trackUri))
                {
                    Track addToPlaylistTrack = tracks.Find(x => x.Uri == trackUri);

                    Navigation.PushModalAsync(new AddToPlaylistPage(addToPlaylistTrack));
                }
                else
                {
                    DisplayAlert("Error", "Nejdříve vyberte skladbu", "OK");
                }
            }
        }

        private async void PlayButton_Clicked(object sender, EventArgs e)
        {
            if (isInitialized)
            {
                await CrossMediaManager.Current.PlayPause();
            }
        }

        private async void AudioSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            int changedTimeNum = (int)AudioSlider.Value;
            TimeSpan changedTime = new TimeSpan(0, 0, changedTimeNum);

            await CrossMediaManager.Current.SeekTo(changedTime);
        }

        private void AudioSlider_DragStarted(object sender, EventArgs e)
        {
            AudioSlider.Opacity = 1;
            CurrentTrackTime.Opacity = 0;
        }

        private void AudioSlider_DragCompleted(object sender, EventArgs e)
        {
            AudioSlider.Opacity = 0;
            CurrentTrackTime.Opacity = 1;
        }

        private async void PlayNext_Clicked(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.PlayNext();
        }

        private async void PlayPrev_Clicked(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.PlayPrevious();
        }
    }
}