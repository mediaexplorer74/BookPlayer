using BookPlayer.Models;
using BookPlayer.ViewModels;
using Xamarin.Forms;

namespace BookPlayer.Views
{
    public partial class PlayerPage : ContentPage
    {
        private readonly PlayerViewModel _viewModel;

        public PlayerPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = ViewModelLocator.Player;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.OnAppearing();
        }
    }
}