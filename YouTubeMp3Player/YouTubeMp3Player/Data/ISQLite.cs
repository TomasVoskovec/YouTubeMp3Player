using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace YouTubeMp3Player.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
