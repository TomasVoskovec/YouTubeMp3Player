using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

[assembly: Xamarin.Forms.Dependency(typeof(YouTubeMp3Player.Droid.MediaHelper))]
namespace YouTubeMp3Player.Droid.Models
{
    public class MediaHelper
    {
        public void ConvertToMp3(string fullFileName)
        {
            var infile = new MediaToolkit.Model.MediaFile { Filename = fullFileName;
            var outfile = new MediaToolkit.Model.MediaFile { Filename = $"{fullFileName}.mp3" };

            using (var engine = new MediaToolkit.Engine())
            {
                engine.GetMetadata(infile);

                engine.Convert(infile, outfile);
                if (File.Exists(fullFileName))
                {
                    File.Delete(fullFileName);
                }
            }
        }
    }
}