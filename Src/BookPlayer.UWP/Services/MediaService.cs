using BookPlayer.UWP.Services;
using BookPlayer.Interfaces;
using MediaManager.Library;
using Xamarin.Forms;
using System;

[assembly: Dependency(typeof(MediaService))]
namespace BookPlayer.UWP.Services
{
    public class MediaService : IMediaService
    {
        public void AddMetaData(MediaManager.Media.IMediaItem mediaItem, string mediaArtist)
        {
            Bundle bundle = new Bundle();            
            bundle.PutString(MediaMetadataCompat.MetadataKeyArtist, mediaArtist);
            mediaItem.Extras = bundle;
        }
    }

    public class MediaMetadataCompat
    {
        public static object MetadataKeyArtist;
    }

    public class Bundle
    {
        internal void PutString(object metadataKeyArtist, string mediaArtist)
        {
            // Not Implemented
        }
    }
}