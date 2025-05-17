using BookPlayer.UWP.Services;
using BookPlayer.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;
using Windows.Storage;

[assembly: Dependency(typeof(FileService))]
namespace BookPlayer.UWP.Services
{
    public class FileService : IFileService
    {
#pragma warning disable CS0618
        public string StorageFolderPath
        {
            get
            {
                // construct platform-specific path
                StorageFolder AppDataFolder = KnownFolders.MusicLibrary;
                return AppDataFolder.Path + "\\Audiobooks";
            }
        }
#pragma warning restore CS0618

        public string AppDataPath
        {
            get
            {
                return FileSystem.AppDataDirectory;
            }
        }
    }
}