using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
            string videoId = null; 

            if (url != null && url != "" && paramKey != null && paramKey != "")
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

        async private void YouTubeDownloadButton_Clicked(object sender, EventArgs e)
        {
            string videoId = getUrlParameter("v", YouTubeVideoLink.Text);

            if (videoId != null && videoId != "")
            {
                YouTubeVideoLink.IsVisible = false;
                YouTubeDownloadButton.IsVisible = false;

                YouTubeApi.Source = "http://voskoto16.sps-prosek.cz/YouTubeMp3Player/api/download?VideoID=" + videoId;
                YouTubeApi.IsVisible = true;
            }

            else
            {
                await DisplayAlert("Stahování", "YouTube link je neplatný", "OK");
            }
        }
    }
}