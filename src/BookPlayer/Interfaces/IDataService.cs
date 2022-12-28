using System.Collections.Generic;

namespace BookPlayer.Interfaces
{
    /// <summary>
    /// Defines functionality related to storing and loading data to/from database
    /// </summary>
    public interface IDataService
    {
        T GetItem<T>(int id);
        IList<T> GetItems<T>();
        void UpdateItem<T>(T item);
        void DeleteItem<T>(int id);
    }
}