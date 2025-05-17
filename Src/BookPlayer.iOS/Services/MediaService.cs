using BookPlayer.iOS.Services;
using BookPlayer.Interfaces;
using MediaManager.Library;
using Xamarin.Forms;
using System;

[assembly: Dependency(typeof(MediaService))]
namespace BookPlayer.iOS.Services
{
    public class MediaService : IMediaService
    {
        public void AddMetaData(IMediaItem mediaItem, string mediaArtist)
        {
            Bundle bundle = new Bundle();            
            bundle.PutString(MediaMetadataCompat.MetadataKeyArtist, mediaArtist);
            mediaItem.Extras = bundle;
        }
    }

    internal class MediaMetadataCompat
    {
        internal static object MetadataKeyArtist;
    }

    internal class Bundle
    {
        internal void PutString(object metadataKeyArtist, string mediaArtist)
        {
            // Not Implemented
        }
    }
}