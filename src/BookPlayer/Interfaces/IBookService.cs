using BookPlayer.Models;
using System;
using System.Collections.Generic;

namespace BookPlayer.Interfaces
{
    /// <summary>
    /// Defines functionality related to handling of books
    /// </summary>
    public interface IBookService
    {
        event EventHandler<Book> SelectedBookChanged;
        IList<Book> GetBooks();
        Book GetSelectedBook();
        void SetSelectedBook(Book book);
        void UpdateProgress(Book book);
        void CleanUp();
    }
}
