using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeMp3Player.Models
{
    public interface IRoseMediaManager
    {
        List<Track> GetTracks();
    }
}
