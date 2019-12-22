using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using YouTubeMp3Player.Data;

namespace YouTubeMp3Player.Droid.Data
{
    public class NetworkConnection : INetworkConnection
    {
        public bool IsConnected { get; set; }

        public void CheckNetworkConnection()
        {
            ConnectivityManager connectivityManager = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
            NetworkInfo activeNetworkInfo = connectivityManager.ActiveNetworkInfo;

            if (activeNetworkInfo != null)
            {
                IsConnected = true;
            }
            else
            {
                IsConnected = false;
            }
        }
    }
}