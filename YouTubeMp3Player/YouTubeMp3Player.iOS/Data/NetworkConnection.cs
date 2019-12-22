using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreFoundation;
using Foundation;
using SystemConfiguration;
using UIKit;
using Xamarin.Forms;
using YouTubeMp3Player.Data;
using YouTubeMp3Player.iOS.Data;

[assembly: Dependency(typeof(NetworkConnection))]

namespace YouTubeMp3Player.iOS.Data
{
    public class NetworkConnection : INetworkConnection
    {
        public bool IsConnected { get; set; }

        public void CheckNetworkConnection()
        {
            InternetStatus();
        }

        public bool InternetStatus()
        {
            NetworkReachabilityFlags flags;
            bool defaultNetworkAvaible = IsNetworkAvailable(out flags);

            if (defaultNetworkAvaible && ((flags & NetworkReachabilityFlags.IsDirect) != 0))
            {
                return false;
            }
            else if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
            {
                return true;
            }
            else if (flags == 0)
            {
                return false;
            }
            return true;
        }

        event EventHandler ReachabilityChanged;
        void OnChange(NetworkReachabilityFlags flags)
        {
            var h = ReachabilityChanged;
            if (h != null)
            {
                h(null, EventArgs.Empty);
            }
        }

        private NetworkReachability defaultReachability;
        public bool IsNetworkAvailable(out NetworkReachabilityFlags flags)
        {
            if (defaultReachability == null)
            {
                defaultReachability = new NetworkReachability(new System.Net.IPAddress(0));
                defaultReachability.SetNotification(OnChange);
                defaultReachability.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
            }
            if (!defaultReachability.TryGetFlags(out flags))
            {
                return false;
            }
            return IsReachableWithoutRequiring(flags);
        }

        bool IsReachableWithoutRequiring(NetworkReachabilityFlags flags)
        {
            bool isReachable = (flags & NetworkReachabilityFlags.Reachable) != 0;
            bool noConnectionRequired = (flags & NetworkReachabilityFlags.ConnectionRequired) == 0;

            if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
            {
                noConnectionRequired = true;
            }
            return isReachable && noConnectionRequired;
        }
    }
}