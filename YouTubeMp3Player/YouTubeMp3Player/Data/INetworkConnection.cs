using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeMp3Player.Data
{
    public interface INetworkConnection
    {
        bool IsConnected { get; }
        void CheckNetworkConnection();
    }
}
