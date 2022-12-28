using BookPlayer.Interfaces;
using BookPlayer.Models;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Command = MvvmHelpers.Commands.Command;

namespace BookPlayer.ViewModels
{
    public class PlayerViewModel : BaseViewModel
    {
        private readonly IBookService _bookService;
        private readonly IPlayerService _playerService;
        private Book _currentBook;

        public ObservableCollection<Item> Items { get; }
        public Command PlayCommand { get; }
        public Command NextCommand { get; }
        public Command PreviousCommand { get; }
        public Command JumpBackwardCommand { get; }
        public Command JumpForwardCommand { get; }
        public Command AddItemCommand { get; }
        public MvvmHelpers.Commands.Command<Item> ItemTapped { get; }
        public IAsyncCommand ChangePositionCommand { get; }

        public IPlayerService Player => _playerService;
        public PlayerViewModel(IBookService bookService, IPlayerService playerService)
        {
            _bookService = bookService;            
            _playerService = playerService;

            PlayCommand = new Command(async () =>
            {
                await _playerService.PlayOrPause();                
            });
            NextCommand = new Command(async () =>
            {
                await _playerService.PlayNextFile();
            });
            PreviousCommand = new Command(async () =>
            {
                await _playerService.PlayPreviousFile();
            });

            JumpBackwardCommand = new Command(async () =>
            {
                await _playerService.JumpBack();
            });

            JumpForwardCommand = new Command(async () =>
            {
                await _playerService.JumpForward();
            });

            ChangePositionCommand = new AsyncCommand(async () =>
            {
                await _playerService.SeekTo(_playerService.CurrentProgress);
            });
        }       

        public async Task OnAppearing()
        {
            IsBusy = true;
            var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if (status != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.StorageRead>();
            }

            if (!_playerService.IsBookOpen)
            {
                _currentBook = _bookService.GetSelectedBook();
                _playerService.OpenBook(_currentBook);
            }            
        }
    }
}