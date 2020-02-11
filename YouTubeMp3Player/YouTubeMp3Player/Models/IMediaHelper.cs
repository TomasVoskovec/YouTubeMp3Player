using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeMp3Player.Models
{
    public interface IMediaHelper
    {
        void ConvertToMp3(string fullFileName);
    }
}
