using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQLite;


namespace ABCEnjoy
{
    public interface ISQLite
    {
        string GetDatabasePath(string filename);
    }

    public class ItemsRepository
    {
        SQLiteConnection database;

        public ItemsRepository(string filename)
        {
            //string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
            string databasePath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), filename);

            database = new SQLiteConnection(databasePath);
            database.CreateTable<ItemOfCategory>();
        }

        public IEnumerable<ItemOfCategory> GetItems()
        {
            return (from i in database.Table<ItemOfCategory>() select i).ToList();
        }

        public ItemOfCategory GetItem(int id)
        {
            return database.Get<ItemOfCategory>(id);
        }

        public int DeleteItem(int id)
        {
            return database.Delete<ItemOfCategory>(id);
        }

        public int SaveItem(ItemOfCategory item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }

    public class SQLite_Android
    {
        public static string GetDatabasePath(string dbName)
        {
            string dbPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);
            // Check if your DB has already been extracted.
            if (!File.Exists(dbPath))
            {
                using (BinaryReader br = new BinaryReader(Android.App.Application.Context.Assets.Open(dbName)))
                {
                    using (BinaryWriter bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int len = 0;
                        while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, len);
                        }
                    }
                }
            }
            return dbPath;

        }

        public static List<ItemOfCategory> GetDBItems(string sqlCommand)
        {
            using (var conn = new SQLite.SQLiteConnection(Settings.dbPath))
            {
                var cmd = conn.CreateCommand(sqlCommand);
                var r = cmd.ExecuteQuery<ItemOfCategory>();

                Console.Write(r);
                return r;
            }


        }


    }

}
