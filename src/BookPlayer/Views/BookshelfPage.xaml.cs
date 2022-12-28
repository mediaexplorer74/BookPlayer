using BookPlayer.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookPlayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookshelfPage : ContentPage
    {
        private readonly BookshelfViewModel _viewModel;
        public BookshelfPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = ViewModelLocator.BookShelf;
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            _viewModel.LoadBooksCommand.Execute(null);
        }
    }
}