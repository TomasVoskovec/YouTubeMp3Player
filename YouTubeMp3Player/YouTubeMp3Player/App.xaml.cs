using System;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YouTubeMp3Player.Data;
using YouTubeMp3Player.Models;
using YouTubeMp3Player.Views;

namespace YouTubeMp3Player
{
    public partial class App : Application
    {
        static TokenDatabase tokenDatabase;
        static UserDatabase userDatabase;
        static RestService restService;
        static Label labelScreen;
        static Page currentPage;
        static Timer timer;

        static bool hasInternet;
        static bool noInternetShow;

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static UserDatabase UserDatabase
        {
            get
            {
                if (userDatabase == null)
                {
                    userDatabase = new UserDatabase();
                }
                return userDatabase;
            }
        }

        public static TokenDatabase TokenDatabase
        {
            get
            {
                if (tokenDatabase == null)
                {
                    tokenDatabase = new TokenDatabase();
                }
                return tokenDatabase;
            }
        }

        public static RestService RestService
        {
            get
            {
                if (restService == null)
                {
                    restService = new RestService();
                }
                return restService;
            }
        }

        // Internet connection
        public static void StartCheckInternet(Label label, Page page)
        {
            labelScreen = label;
            label.Text = Constants.NoInternetText;
            label.IsVisible = false;
            hasInternet = false;
            currentPage = page;

            if (!CheckInternet())
            {
                label.IsVisible = true;
            }

            if (timer == null)
            {
                timer = new Timer((e) =>
                {
                    CheckIfInternetOverTime();
                }, null, 10, (int)TimeSpan.FromSeconds(3).TotalMilliseconds);
            }
        }

        static void CheckIfInternetOverTime()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (hasInternet)
                    {
                        if (!noInternetShow)
                        {
                            hasInternet = false;
                            labelScreen.IsVisible = true;
                            noInternetShow = false;
                        }
                    }
                });
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    hasInternet = true;
                    labelScreen.IsVisible = false;
                });
            }
        }

        // Faster version (buttons usage etc.)
        public static bool CheckInternet()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                return true;
            }

            return false;
        }

        public static bool CheckIfInternetAlert()
        {
            var networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckNetworkConnection();

            if (!networkConnection.IsConnected)
            {
                if (!noInternetShow)
                {
                    noInternetShow = false;
                }
            }
            return true;
        }
    }
}