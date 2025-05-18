using BookPlayer.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BookPlayer.Services
{
    public class OptionService : IOptionService
    {

        private const string BookLibraryRootPathSettingName = "BookLibraryRootPath";
        private const string DefaultStorageFolderPath =
                          @"/storage/emulated/0/Audiobooks"; // Added default path

        public string BookLibraryRootFolderPath
        {
            get
            {
                return Preferences.Get(BookLibraryRootPathSettingName,
                    /*DefaultStorageFolderPath*/
                    App.booksPath); // 
            }
            set
            {
                Preferences.Set(BookLibraryRootPathSettingName, value);
            }
        }
    }
}
