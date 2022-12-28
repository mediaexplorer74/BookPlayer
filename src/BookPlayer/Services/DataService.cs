using BookPlayer.Interfaces;
using LiteDB;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Essentials;

namespace BookPlayer.Services
{
    public class DataService : IDataService
    {
        private const string DatabaseFileName = "Data.db";
        
        public T GetItem<T>(int id)
        {
            using (var db = new LiteDatabase(DatabaseFilePath))
            {                
                var col = db.GetCollection<T>(typeof(T).Name);
                return col.FindById(id);
            }
        }

        public IList<T> GetItems<T>()
        {            
            using (var db = new LiteDatabase(DatabaseFilePath))
            {
                var col = db.GetCollection<T>(typeof(T).Name);
                return col.FindAll().ToList();
            }
        }

        public void UpdateItem<T>(T item)
        {
            using (var db = new LiteDatabase(DatabaseFilePath))
            {                
                var col = db.GetCollection<T>(typeof(T).Name);
                col.Upsert(item);
            }
        }

        public void DeleteItem<T>(int id)
        {
            using (var db = new LiteDatabase(DatabaseFilePath))
            {
                var col = db.GetCollection<T>(typeof(T).Name);
                col.Delete(id);
            }
        }

        string DatabaseFilePath 
        { 
            get => Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName); 
        }
    }
}
