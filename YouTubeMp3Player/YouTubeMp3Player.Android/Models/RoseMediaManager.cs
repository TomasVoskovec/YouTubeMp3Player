using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using YouTubeMp3Player.Models;

[assembly: Xamarin.Forms.Dependency(typeof(YouTubeMp3Player.Droid.MediaHelper))]
namespace YouTubeMp3Player.Droid
{
    class MediaHelper : IRoseMediaManager
    {
        public List<Track> GetTracks()
        {
            List<Track> music = new List<Track>();
            var c = Application.Context.ContentResolver.Query(MediaStore.Audio.Media.GetContentUri("external"), new string[] { MediaStore.Audio.Media.InterfaceConsts.Data, MediaStore.Audio.Media.InterfaceConsts.DisplayName, MediaStore.Audio.Media.InterfaceConsts.Duration, MediaStore.Audio.Media.InterfaceConsts.Title }, null, null, null);


            while (c.MoveToNext())
            {
                
                string name = c.GetString(c.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.DisplayName));
                string uri = c.GetString(c.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Data));

                if (uri.Contains(".mp3"))
                {
                    Track track = new Track(name, uri);
                    music.Add(track);
                }
            }

            return music;

        }
    }
}