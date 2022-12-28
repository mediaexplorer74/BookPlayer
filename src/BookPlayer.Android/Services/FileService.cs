using BookPlayer.Droid.Services;
using BookPlayer.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileService))]
namespace BookPlayer.Droid.Services
{
    public class FileService : IFileService
    {
#pragma warning disable CS0618
        public string StorageFolderPath => Android.OS.Environment.GetExternalStoragePublicDirectory("Audiobooks").AbsolutePath;
#pragma warning restore CS0618
        
        public string AppDataPath => FileSystem.AppDataDirectory;
    }
}