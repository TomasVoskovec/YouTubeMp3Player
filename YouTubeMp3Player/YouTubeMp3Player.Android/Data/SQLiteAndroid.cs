using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using YouTubeMp3Player.Data;
using YouTubeMp3Player.Droid.Data;

[assembly: Dependency(typeof(SQLiteAndroid))]

namespace YouTubeMp3Player.Droid.Data
{
    public class SQLiteAndroid : ISQLite
    {
        public SQLiteAndroid() { }
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFileName = "TestDB.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFileName);

            var connection = new SQLite.SQLiteConnection(path);

            return connection;
        }
    }
}