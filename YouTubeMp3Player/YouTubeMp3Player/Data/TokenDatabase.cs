using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using YouTubeMp3Player.Models;

namespace YouTubeMp3Player.Data
{
    public class TokenDatabase
    {
        static object locker = new object();

        SQLiteConnection database;

        public TokenDatabase()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Token>();
        }

        public Token GetToken()
        {
            lock (locker)
            {
                if (database.Table<Token>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    List<Token> tokens = database.Table<Token>().ToList();

                    return tokens[tokens.Count - 1];
                }
            }
        }

        public List<Token> GetAllTokens()
        {
            lock (locker)
            {
                if (database.Table<Token>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Token>().ToList();
                }
            }
        }

        public int SaveToken(Token token)
        {
            lock (locker)
            {
                if (token.Id != 0)
                {
                    database.Update(token);
                    return token.Id;
                }
                else
                {
                    return database.Insert(token);
                }
            }
        }

        public int DeleteToken(int id)
        {
            lock (locker)
            {
                return database.Delete<Token>(id);
            }
        }
    }
}
