using BookPlayer.Interfaces;
using BookPlayer.Models;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookPlayer.ViewModels
{
    public class BookshelfViewModel : BaseViewModel
    {
        private readonly IBookService _bookService;
        public ICommand LoadBooksCommand { get; }
        public IAsyncCommand<Book> SelectBookCommand { get; }

        public ObservableCollection<Book> _books = new ObservableCollection<Book>();

        public ObservableCollection<Book> Books
        {
            get { return _books; }
            set
            {
                SetProperty(ref _books, value, nameof(Books));
            }
        }

        public BookshelfViewModel(IBookService bookService)
        {
            Title = "Bookshelf";
            _bookService = bookService;

            LoadBooksCommand = new MvvmHelpers.Commands.Command(() =>
            {
                ReloadBooks();
            });

            SelectBookCommand = new AsyncCommand<Book>(async (p) =>
            {
                _bookService.SetSelectedBook(p);
                await Shell.Current.GoToAsync("//PlayerPage");
            });
        }

        private void ReloadBooks()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;
                Books.Clear();
                var books = _bookService.GetBooks();

                if (books != null)
                {
                    foreach (var book in books)
                    {
                        Books.Add(book);
                    }
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
