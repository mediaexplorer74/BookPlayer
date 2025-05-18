using BookPlayer.Interfaces;
using BookPlayer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookPlayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        // 1 Interface connector
        public IFileService fileService;
        public SettingsPage()
        {
            InitializeComponent();

            // Interface Init  
            fileService = DependencyService.Get<IFileService>();
            App.booksPath = fileService.StorageFolderPath;

            BindingContext = ViewModelLocator.Settings;
        }
    }
}