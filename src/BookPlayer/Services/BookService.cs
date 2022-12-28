using BookPlayer.Interfaces;
using BookPlayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookPlayer.Services
{
    public class BookService : IBookService
    {
        private readonly IDataService _dataService;
        private readonly IOptionService _optionService;
        private readonly IFileHandlingService _fileHandlingService;
        private readonly Player _player;
        private Book _selectedBook;

        public BookService(IDataService dataService, IOptionService optionService,
            IFileHandlingService fileHandlingService)
        {
            _dataService = dataService;
            _optionService = optionService;
            _fileHandlingService = fileHandlingService;

            _player ??= _dataService.GetItem<Player>(1) ?? new Player();
        }

        public event EventHandler<Book> SelectedBookChanged;

        public IList<Book> GetBooks()
        {
            var scannedBooks = _fileHandlingService.GetBooks(_optionService.BookLibraryRootFolderPath);
            var existingBooks = _dataService.GetItems<Book>().ToList();

            foreach (var newBook in scannedBooks
                .Where(book => !existingBooks.Any(existingBook => existingBook.Name.Equals(book.Name))))
            {
                _dataService.UpdateItem(newBook);
            }

            foreach (var oldBook in existingBooks
                .Where(book => !scannedBooks.Any(newBook => newBook.Name.Equals(book.Name))))
            {
                _dataService.DeleteItem<Book>(oldBook.Id);
            }

            existingBooks = _dataService.GetItems<Book>().ToList();

            return existingBooks.ToList();
        }

        public Book GetSelectedBook()
        {            
            _selectedBook ??= _dataService.GetItem<Book>(_player.CurrentBook);
            
            return _selectedBook;
        }

        public void SetSelectedBook(Book book)
        {
            _selectedBook = book;
            _player.CurrentBook = _selectedBook.Id;
            _dataService.UpdateItem(_player);

            SelectedBookChanged?.Invoke(this, book);
        }

        public void UpdateProgress(Book book)
        {
            _dataService.UpdateItem(book);
        }

        public void CleanUp()
        {
            
        }
    }
}
