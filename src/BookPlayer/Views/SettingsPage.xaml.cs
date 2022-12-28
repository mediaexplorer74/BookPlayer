using BookPlayer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookPlayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Settings;
        }
    }
}