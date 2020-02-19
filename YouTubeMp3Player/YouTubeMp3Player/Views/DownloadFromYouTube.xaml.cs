using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YouTubeMp3Player.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DownloadFromYouTube : ContentPage
    {
        public DownloadFromYouTube()
        {
            InitializeComponent();
        }

        string getUrlParameter (string paramKey, string url)
        {
            string videoId = ""; 

            if (url != null /*&& url != "" && paramKey != null && paramKey != ""*/)
            {
                Uri myUri = new Uri(url);
                videoId = HttpUtility.ParseQueryString(myUri.Query).Get(paramKey);
            }
            if (videoId == null || videoId == "")
            {
                int pos = url.LastIndexOf("/") + 1;
                videoId = url.Substring(pos, url.Length - pos);
            }
            return videoId;
        }

        private async void YouTubeDownloadButton_Clicked(object sender, EventArgs e)
        {
            string videoId;
            if (YouTubeVideoLink.Text != null)
            {
                videoId = getUrlParameter("v", YouTubeVideoLink.Text);
            }
            else
            {
                videoId = null;
            }

            if (videoId != null && videoId != "")
            {
                //await Browser.OpenAsync("https://break.tv/widget/button/?link=https://www.youtube.com/watch?v=" + videoId, BrowserLaunchMode.External);
            }

            else
            {
                await DisplayAlert("Stahování", "YouTube link je neplatný", "OK");
            }
        }
    }
}