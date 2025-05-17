// Decompiled with JetBrains decompiler
// Type: MediaManager.Player.MediaPlayerEventArgs
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using MediaManager.Library;
using MediaManager.Media;

#nullable disable
namespace MediaManager.Player
{
  public class MediaPlayerEventArgs : MediaItemEventArgs
  {
    public MediaPlayerEventArgs(IMediaItem mediaItem, IMediaPlayer mediaPlayer)
      : base(mediaItem)
    {
      this.MediaPlayer = mediaPlayer;
    }

    public IMediaPlayer MediaPlayer { get; protected set; }
  }
}
