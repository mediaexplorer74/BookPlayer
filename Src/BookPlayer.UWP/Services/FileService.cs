using BookPlayer.Interfaces;
using BookPlayer.UWP.Services;
using System;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Essentials;
using Xamarin.Forms;

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
                //StorageFolder AppDataFolder = KnownFolders.MusicLibrary;
                var musicPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                return /*AppDataFolder.Path*/musicPath + "\\Audiobooks";
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