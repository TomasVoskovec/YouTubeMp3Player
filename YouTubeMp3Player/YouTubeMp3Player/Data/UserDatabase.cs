using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using YouTubeMp3Player.Models;

namespace YouTubeMp3Player.Data
{
    public class UserDatabase
    {
        static object locker = new object();

        SQLiteConnection database;

        public UserDatabase()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<User>();
        }

        public User GetUser()
        {
            lock (locker)
            {
                if (database.Table<User>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<User>().First();
                }
            }
        }

        public int SaveUser(User user)
        {
            lock (locker)
            {
                if (user.Id != 0)
                {
                    database.Update(user);
                    return user.Id;
                }
                else
                {
                    return database.Insert(user);
                }
            }
        }

        public int DeleteUser(int id)
        {
            lock (locker)
            {
                return database.Delete<User>(id);
            }
        }
    }
}