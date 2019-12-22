using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace YouTubeMp3Player.Models
{
    public class Constants
    {
        public static bool IsDev = true;
        public string AppName = "YouTube MP3 Player";
        public string AppWebName = "YouTubeMp3Player";

        // Colors
        public static Color BackgroundColor = Color.White;
        public static Color MainTextColor = Color.Black;
        public static Color PlaceHolderColor = Color.FromRgb(179, 179, 179);
        public static Color OrangeTextColor = Color.FromRgb(255, 86, 0);
        public static Color ButtonTextColor = Color.White;
        public static Color ActiveOrangeColor = Color.FromRgb(255, 117, 0);
        public static Color ErrorColor = Color.FromRgb(228, 108, 103);
        public static Color PasiveColor = Color.FromRgb(204, 204, 204);

        // Font sizes
        public static double TextL = 20;
        public static double TextM = 18;
        public static double TextS = 12;

        //Server
        public static string ServerUrl = "https://voskoto16.sps-prosek.cz";

        // Login
        public static string LoginUrl = Constants.ServerUrl + "/YouTubeMp3Player/api/login.php";
        public static string ValidateTokenUrl = Constants.ServerUrl + "/YouTubeMp3Player/api/validate_token.php";

        // Messages
        public static string NoInternetText = "No internet connection, please reconnect your device.";
    }
}