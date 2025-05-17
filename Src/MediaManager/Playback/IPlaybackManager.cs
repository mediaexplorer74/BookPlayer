// Decompiled with JetBrains decompiler
// Type: MediaManager.Playback.IPlaybackManager
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using MediaManager.Library;
using MediaManager.Player;
using MediaManager.Queue;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

#nullable disable
namespace MediaManager.Playback
{
  public interface IPlaybackManager : INotifyPropertyChanged
  {
    TimeSpan StepSizeForward { get; set; }

    TimeSpan StepSizeBackward { get; set; }

    MediaPlayerState State { get; }

    TimeSpan Position { get; }

    TimeSpan Duration { get; }

    TimeSpan Buffered { get; }

    float Speed { get; set; }

    RepeatMode RepeatMode { get; set; }

    ShuffleMode ShuffleMode { get; set; }

    bool ClearQueueOnPlay { get; set; }

    bool AutoPlay { get; set; }

    bool KeepScreenOn { get; set; }

    bool RetryPlayOnFailed { get; set; }

    bool PlayNextOnFailed { get; set; }

    int MaxRetryCount { get; set; }

    Task Play();

    Task Pause();

    Task Stop();

    Task<bool> PlayPrevious();

    Task<bool> PlayNext();

    Task<bool> PlayQueueItem(IMediaItem mediaItem);

    Task<bool> PlayQueueItem(int index);

    Task StepForward();

    Task StepBackward();

    Task SeekTo(TimeSpan position);

    event StateChangedEventHandler StateChanged;

    event BufferedChangedEventHandler BufferedChanged;

    event PositionChangedEventHandler PositionChanged;

    event MediaItemFinishedEventHandler MediaItemFinished;

    event MediaItemChangedEventHandler MediaItemChanged;

    event MediaItemFailedEventHandler MediaItemFailed;
  }
}
