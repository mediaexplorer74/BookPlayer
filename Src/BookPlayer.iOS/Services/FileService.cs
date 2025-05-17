using BookPlayer.iOS.Services;
using BookPlayer.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.IO;
using System;
//using Windows.Storage;

[assembly: Dependency(typeof(FileService))]
namespace BookPlayer.iOS.Services
{
    public class FileService : IFileService
    {
        public string StorageFolderPath
        {
            get
            {
                // construct platform-specific path
                string musicLibraryPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), 
                    "Audiobooks");
                return musicLibraryPath;
            }
        }

        public string AppDataPath
        {
            get
            {
                return FileSystem.AppDataDirectory;
            }
        }
    }
}