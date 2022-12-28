using BookPlayer.Interfaces;
using System.Windows.Input;

namespace BookPlayer.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly IOptionService _optionService;
        public ICommand UpdateBookLibraryPathCommand { get; }        

        public string _bookLibraryPath;

        public string BookLibraryPath
        {
            get { return _bookLibraryPath; }
            set
            {
                SetProperty(ref _bookLibraryPath, value, nameof(BookLibraryPath));
            }
        }

        public SettingsViewModel(IOptionService optionService)
        {
            Title = "Settings";
            _optionService = optionService;

            BookLibraryPath = _optionService.BookLibraryRootFolderPath;

            UpdateBookLibraryPathCommand = new MvvmHelpers.Commands.Command(() =>
            {
                _optionService.BookLibraryRootFolderPath = BookLibraryPath;
            });
        }
    }
}
