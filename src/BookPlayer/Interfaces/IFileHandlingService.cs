using BookPlayer.Models;
using System.Collections.Generic;

namespace BookPlayer.Interfaces
{
    /// <summary>
    /// Defines functionality related to handling files
    /// </summary>
    public interface IFileHandlingService
    {
        IList<Book> GetBooks(string rootFolderPath);
        BookMetadata GetBookMetadata(string filePath);
    }
}
