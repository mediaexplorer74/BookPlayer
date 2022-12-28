using MediaManager.Library;

namespace BookPlayer.Interfaces
{
    /// <summary>
    /// Defines functionality to handle media information on different devices
    /// </summary>
    public interface IMediaService
    {
        void AddMetaData(IMediaItem mediaItem, string mediaArtist);
    }
}
