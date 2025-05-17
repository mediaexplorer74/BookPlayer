// Decompiled with JetBrains decompiler
// Type: MediaManager.Player.IMediaPlayer
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using MediaManager.Library;
using MediaManager.Video;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

#nullable disable
namespace MediaManager.Player
{
  public interface IMediaPlayer : IDisposable, INotifyPropertyChanged
  {
    IVideoView VideoView { get; set; }

    bool AutoAttachVideoView { get; set; }

    VideoAspectMode VideoAspect { get; set; }

    bool ShowPlaybackControls { get; set; }

    int VideoWidth { get; }

    int VideoHeight { get; }

    float VideoAspectRatio { get; }

    object VideoPlaceholder { get; set; }

    Task Play(IMediaItem mediaItem);

    Task Play(IMediaItem mediaItem, TimeSpan startAt, TimeSpan? stopAt = null);

    Task Play();

    Task Pause();

    Task Stop();

    Task SeekTo(TimeSpan position);

    event BeforePlayingEventHandler BeforePlaying;

    event AfterPlayingEventHandler AfterPlaying;
  }
}
