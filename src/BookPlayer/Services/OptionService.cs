using BookPlayer.Interfaces;
using Xamarin.Essentials;

namespace BookPlayer.Services
{
    public class OptionService : IOptionService
    {
        private const string BookLibraryRootPathSettingName = "BookLibraryRootPath";

        public string BookLibraryRootFolderPath 
        { 
            get
            {
                return Preferences.Get(BookLibraryRootPathSettingName, @"/storage/emulated/0/Audiobooks");                
            }
            set
            {
                Preferences.Set(BookLibraryRootPathSettingName, value);
            }
        }
    }
}
