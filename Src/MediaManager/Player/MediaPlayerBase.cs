// Decompiled with JetBrains decompiler
// Type: MediaManager.Player.MediaPlayerBase
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
  public abstract class MediaPlayerBase : 
    NotifyPropertyChangedBase,
    IMediaPlayer,
    IDisposable,
    INotifyPropertyChanged
  {
    protected bool _autoAttachVideoView = true;
    protected VideoAspectMode _videoAspect;
    protected bool _showPlaybackControls;
    protected int _videoWidth;
    protected int _videoHeight;
    private object _videoPlaceholder;

    public abstract IVideoView VideoView { get; set; }

    public virtual bool AutoAttachVideoView
    {
      get => this._autoAttachVideoView;
      set
      {
        this.SetProperty<bool>(ref this._autoAttachVideoView, value, nameof (AutoAttachVideoView));
      }
    }

    protected virtual void UpdateVideoView()
    {
      this.UpdateVideoAspect(this.VideoAspect);
      this.UpdateShowPlaybackControls(this.ShowPlaybackControls);
      this.UpdateVideoPlaceholder(this.VideoPlaceholder);
    }

    public virtual VideoAspectMode VideoAspect
    {
      get => this._videoAspect;
      set
      {
        if (!this.SetProperty<VideoAspectMode>(ref this._videoAspect, value, nameof (VideoAspect)))
          return;
        this.UpdateVideoAspect(value);
      }
    }

    public abstract void UpdateVideoAspect(VideoAspectMode videoAspectMode);

    public virtual bool ShowPlaybackControls
    {
      get => this._showPlaybackControls;
      set
      {
        if (!this.SetProperty<bool>(ref this._showPlaybackControls, value, nameof (ShowPlaybackControls)))
          return;
        this.UpdateShowPlaybackControls(value);
      }
    }

    public abstract void UpdateShowPlaybackControls(bool showPlaybackControls);

    public virtual int VideoWidth
    {
      get => this._videoWidth;
      internal set
      {
        this.SetProperty<int>(ref this._videoWidth, value, (Action) (() => this.OnPropertyChanged("VideoAspectRatio")), nameof (VideoWidth));
      }
    }

    public virtual int VideoHeight
    {
      get => this._videoHeight;
      internal set
      {
        this.SetProperty<int>(ref this._videoHeight, value, (Action) (() => this.OnPropertyChanged("VideoAspectRatio")), nameof (VideoHeight));
      }
    }

    public virtual float VideoAspectRatio
    {
      get => this.VideoHeight != 0 ? (float) this.VideoWidth / (float) this.VideoHeight : 0.0f;
    }

    public virtual object VideoPlaceholder
    {
      get => this._videoPlaceholder;
      set
      {
        if (!this.SetProperty<object>(ref this._videoPlaceholder, value, nameof (VideoPlaceholder)))
          return;
        this.UpdateVideoPlaceholder(value);
      }
    }

    public abstract void UpdateVideoPlaceholder(object value);

    public event BeforePlayingEventHandler BeforePlaying;

    public event AfterPlayingEventHandler AfterPlaying;

    public void InvokeBeforePlaying(object sender, MediaPlayerEventArgs e)
    {
      BeforePlayingEventHandler beforePlaying = this.BeforePlaying;
      if (beforePlaying == null)
        return;
      beforePlaying(sender, e);
    }

    public void InvokeAfterPlaying(object sender, MediaPlayerEventArgs e)
    {
      AfterPlayingEventHandler afterPlaying = this.AfterPlaying;
      if (afterPlaying == null)
        return;
      afterPlaying(sender, e);
    }

    public abstract Task Pause();

    public abstract Task Play(IMediaItem mediaItem);

    public abstract Task Play(IMediaItem mediaItem, TimeSpan startAt, TimeSpan? stopAt = null);

    public abstract Task Play();

    public abstract Task SeekTo(TimeSpan position);

    public abstract Task Stop();

    protected abstract void Dispose(bool disposing);

    public void Dispose() => this.Dispose(true);
  }
}
