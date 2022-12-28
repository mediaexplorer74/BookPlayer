using Android.OS;
using Android.Support.V4.Media;
using BookPlayer.Droid.Services;
using BookPlayer.Interfaces;
using MediaManager.Library;
using Xamarin.Forms;

[assembly: Dependency(typeof(MediaService))]
namespace BookPlayer.Droid.Services
{
    public class MediaService : IMediaService
    {
        public void AddMetaData(IMediaItem mediaItem, string mediaArtist)
        {
            // For samsung phones
            Bundle bundle = new Bundle();            
            bundle.PutString(MediaMetadataCompat.MetadataKeyArtist, mediaArtist);
            mediaItem.Extras = bundle;
        }
    }
}