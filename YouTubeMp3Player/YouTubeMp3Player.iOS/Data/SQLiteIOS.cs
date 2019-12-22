using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using YouTubeMp3Player.Data;
using YouTubeMp3Player.iOS.Data;

[assembly: Dependency(typeof(SQLiteIOS))]

namespace YouTubeMp3Player.iOS.Data
{
    public class SQLiteIOS : ISQLite
    {
        public SQLiteIOS() { }
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFileName = "TestDB.db3";
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, sqliteFileName);

            var connection = new SQLite.SQLiteConnection(path);

            return connection;
        }
    }
}