// Decompiled with JetBrains decompiler
// Type: MediaManager.MediaManagerBase
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using MediaManager.Library;
using MediaManager.Media;
using MediaManager.Notifications;
using MediaManager.Playback;
using MediaManager.Player;
using MediaManager.Queue;
using MediaManager.Volume;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;

#nullable disable
namespace MediaManager
{
  public abstract class MediaManagerBase : 
    NotifyPropertyChangedBase,
    IMediaManager,
    IPlaybackManager,
    INotifyPropertyChanged,
    IDisposable
  {
    private bool _isInitialized = true;
    protected TimeSpan _stepSizeForward = TimeSpan.FromSeconds(10.0);
    protected TimeSpan _stepSizeBackward = TimeSpan.FromSeconds(10.0);
    protected Dictionary<string, string> _requestHeaders = new Dictionary<string, string>();
    protected IMediaLibrary _library;
    protected IMediaQueue _mediaQueue;
    protected MediaPlayerState _state;
    protected TimeSpan _buffered;
    private RepeatMode _repeatMode;
    private ShuffleMode _shuffleMode;
    private bool _clearQueueOnPlay = true;
    private bool _autoPlay = true;
    private bool _retryPlayOnFailed = true;
    private bool _playNextOnFailed = true;
    private int _maxRetryCount = 1;
    protected IMediaItem _currentSource;
    protected int RetryCount;
    protected TimeSpan _previousPosition;

    public MediaManagerBase() => this.InitTimer();

    public bool IsInitialized
    {
      get => this._isInitialized;
      protected set
      {
        this.SetProperty<bool>(ref this._isInitialized, value, nameof (IsInitialized));
      }
    }

    public Timer Timer { get; protected set; } = new Timer(MediaManagerBase.TimerInterval);

    public static double TimerInterval { get; set; } = 1000.0;

    public virtual TimeSpan StepSizeForward
    {
      get => this._stepSizeForward;
      set => this.SetProperty<TimeSpan>(ref this._stepSizeForward, value, nameof (StepSizeForward));
    }

    public virtual TimeSpan StepSizeBackward
    {
      get => this._stepSizeBackward;
      set
      {
        this.SetProperty<TimeSpan>(ref this._stepSizeBackward, value, nameof (StepSizeBackward));
      }
    }

    public virtual Dictionary<string, string> RequestHeaders
    {
      get => this._requestHeaders;
      set
      {
        this.SetProperty<Dictionary<string, string>>(ref this._requestHeaders, value, nameof (RequestHeaders));
      }
    }

    public virtual IMediaLibrary Library
    {
      get
      {
        if (this._library == null)
          this._library = (IMediaLibrary) new MediaLibrary();
        return this._library;
      }
      set => this.SetProperty<IMediaLibrary>(ref this._library, value, nameof (Library));
    }

    public virtual IMediaQueue Queue
    {
      get
      {
        if (this._mediaQueue == null)
          this._mediaQueue = (IMediaQueue) new MediaQueue();
        return this._mediaQueue;
      }
      set => this.SetProperty<IMediaQueue>(ref this._mediaQueue, value, nameof (Queue));
    }

    public virtual void Init()
    {
      this.IsInitialized = true;
      this.InitTimer();
    }

    public virtual void InitTimer()
    {
      Timer timer = this.Timer;
      if ((timer != null ? (timer.Enabled ? 1 : 0) : 0) != 0)
        return;
      this.Timer = new Timer(MediaManagerBase.TimerInterval)
      {
        AutoReset = true,
        Enabled = true
      };
      this.Timer.Elapsed += new ElapsedEventHandler(this.Timer_Elapsed);
      this.Timer.Start();
    }

    public abstract IMediaPlayer MediaPlayer { get; set; }

    public abstract IMediaExtractor Extractor { get; set; }

    public abstract IVolumeManager Volume { get; set; }

    public abstract INotificationManager Notification { get; set; }

    public MediaPlayerState State
    {
      get => this._state;
      internal set
      {
        if (!this.SetProperty<MediaPlayerState>(ref this._state, value, nameof (State)))
          return;
        this.OnStateChanged((object) this, new StateChangedEventArgs(this.State));
      }
    }

    public TimeSpan Buffered
    {
      get => this._buffered;
      internal set
      {
        if (!this.SetProperty<TimeSpan>(ref this._buffered, value, nameof (Buffered)))
          return;
        this.OnBufferedChanged((object) this, new BufferedChangedEventArgs(this.Buffered));
      }
    }

    public abstract TimeSpan Position { get; }

    public abstract TimeSpan Duration { get; }

    public abstract float Speed { get; set; }

    public abstract bool KeepScreenOn { get; set; }

    public virtual RepeatMode RepeatMode
    {
      get => this._repeatMode;
      set => this.SetProperty<RepeatMode>(ref this._repeatMode, value, nameof (RepeatMode));
    }

    public virtual ShuffleMode ShuffleMode
    {
      get => this._shuffleMode;
      set => this.SetProperty<ShuffleMode>(ref this._shuffleMode, value, nameof (ShuffleMode));
    }

    public bool ClearQueueOnPlay
    {
      get => this._clearQueueOnPlay;
      set => this.SetProperty<bool>(ref this._clearQueueOnPlay, value, nameof (ClearQueueOnPlay));
    }

    public bool AutoPlay
    {
      get => this._autoPlay;
      set => this.SetProperty<bool>(ref this._autoPlay, value, nameof (AutoPlay));
    }

    public bool RetryPlayOnFailed
    {
      get => this._retryPlayOnFailed;
      set => this.SetProperty<bool>(ref this._retryPlayOnFailed, value, nameof (RetryPlayOnFailed));
    }

    public bool PlayNextOnFailed
    {
      get => this._playNextOnFailed;
      set => this.SetProperty<bool>(ref this._playNextOnFailed, value, nameof (PlayNextOnFailed));
    }

    public int MaxRetryCount
    {
      get => this._maxRetryCount;
      set => this.SetProperty<int>(ref this._maxRetryCount, value, nameof (MaxRetryCount));
    }

    public virtual Task Play() => this.MediaPlayer.Play();

    public virtual Task Pause() => this.MediaPlayer.Pause();

    public virtual Task SeekTo(TimeSpan position) => this.MediaPlayer.SeekTo(position);

    public virtual Task Stop() => this.MediaPlayer.Stop();

    public virtual async Task<IMediaItem> Play(IMediaItem mediaItem)
    {
      IMediaItem mediaItemToPlay = await this.PrepareQueueForPlayback(mediaItem);
      await this.PlayAsCurrent(mediaItemToPlay);
      IMediaItem mediaItem1 = mediaItemToPlay;
      mediaItemToPlay = (IMediaItem) null;
      return mediaItem1;
    }

    public virtual async Task<IMediaItem> Play(string uri)
    {
      IMediaItem mediaItem = await this.Extractor.CreateMediaItem(uri).ConfigureAwait(false);
      await this.PlayAsCurrent(await this.PrepareQueueForPlayback(mediaItem));
      IMediaItem mediaItem1 = mediaItem;
      mediaItem = (IMediaItem) null;
      return mediaItem1;
    }

    public virtual async Task<IMediaItem> PlayFromAssembly(string resourceName, Assembly assembly = null)
    {
      IMediaItem mediaItem = await this.Extractor.CreateMediaItemFromAssembly(resourceName, assembly).ConfigureAwait(false);
      await this.PlayAsCurrent(await this.PrepareQueueForPlayback(mediaItem));
      IMediaItem mediaItem1 = mediaItem;
      mediaItem = (IMediaItem) null;
      return mediaItem1;
    }

    public virtual async Task<IMediaItem> PlayFromResource(string resourceName)
    {
      IMediaItem mediaItem = await this.Extractor.CreateMediaItemFromResource(resourceName).ConfigureAwait(false);
      await this.PlayAsCurrent(await this.PrepareQueueForPlayback(mediaItem));
      IMediaItem mediaItem1 = mediaItem;
      mediaItem = (IMediaItem) null;
      return mediaItem1;
    }

    public virtual async Task<IMediaItem> Play(Stream stream, string cacheName)
    {
      string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), cacheName);
      FileStream fileStream = File.Create(path);
      await stream.CopyToAsync((Stream) fileStream);
      fileStream.Close();
      IMediaItem mediaItem = await this.Extractor.CreateMediaItem(path).ConfigureAwait(false);
      await this.PlayAsCurrent(await this.PrepareQueueForPlayback(mediaItem));
      IMediaItem mediaItem1 = mediaItem;
      path = (string) null;
      fileStream = (FileStream) null;
      mediaItem = (IMediaItem) null;
      return mediaItem1;
    }

    public virtual async Task<IMediaItem> Play(IEnumerable<IMediaItem> mediaItems)
    {
      IMediaItem mediaItemToPlay = await this.PrepareQueueForPlayback(mediaItems);
      await this.PlayAsCurrent(mediaItemToPlay);
      IMediaItem mediaItem = mediaItemToPlay;
      mediaItemToPlay = (IMediaItem) null;
      return mediaItem;
    }

    public virtual async Task<IMediaItem> Play(IEnumerable<string> items)
    {
      IMediaItem mediaItemToPlay = await this.PrepareQueueForPlayback(await items.CreateMediaItems());
      await this.PlayAsCurrent(mediaItemToPlay);
      IMediaItem mediaItem = mediaItemToPlay;
      mediaItemToPlay = (IMediaItem) null;
      return mediaItem;
    }

    public virtual async Task<IMediaItem> Play(FileInfo file)
    {
      IMediaItem mediaItem = await this.Extractor.CreateMediaItem(file).ConfigureAwait(false);
      await this.PlayAsCurrent(await this.PrepareQueueForPlayback(mediaItem));
      IMediaItem mediaItem1 = mediaItem;
      mediaItem = (IMediaItem) null;
      return mediaItem1;
    }

    public virtual async Task<IMediaItem> Play(DirectoryInfo directoryInfo)
    {
      IMediaItem mediaItemToPlay = await this.PrepareQueueForPlayback(await ((IEnumerable<FileInfo>) directoryInfo.GetFiles()).CreateMediaItems());
      await this.PlayAsCurrent(mediaItemToPlay);
      IMediaItem mediaItem = mediaItemToPlay;
      mediaItemToPlay = (IMediaItem) null;
      return mediaItem;
    }

    public virtual async Task PlayAsCurrent(IMediaItem mediaItem)
    {
      if (!this.AutoPlay)
        return;
      await this.MediaPlayer.Play(mediaItem);
    }

    public virtual Task<IMediaItem> PrepareQueueForPlayback(IMediaItem mediaItem)
    {
      return this.PrepareQueueForPlayback((IEnumerable<IMediaItem>) new IMediaItem[1]
      {
        mediaItem
      });
    }

    public virtual Task<IMediaItem> PrepareQueueForPlayback(IEnumerable<IMediaItem> mediaItems)
    {
      if (this.ClearQueueOnPlay)
      {
        this.Queue.Clear();
        this.Queue.CurrentIndex = 0;
      }
      foreach (IMediaItem mediaItem in mediaItems)
        this.Queue.Add(mediaItem);
      return Task.FromResult<IMediaItem>(this.Queue.Current);
    }

    public virtual async Task<bool> PlayNext()
    {
      IMediaItem mediaItem = (IMediaItem) null;
      if (this.RepeatMode == RepeatMode.One)
        mediaItem = this.Queue.Current;
      else if (this.RepeatMode == RepeatMode.All && !this.Queue.HasNext)
        mediaItem = this.Queue.First<IMediaItem>();
      else if (this.Queue.HasNext)
        mediaItem = this.Queue.Next;
      return await this.PlayQueueItem(mediaItem);
    }

    public virtual async Task<bool> PlayPrevious()
    {
      return this.Queue.HasPrevious && await this.PlayQueueItem(this.Queue.Previous);
    }

    public virtual async Task<bool> PlayQueueItem(IMediaItem mediaItem)
    {
      if (mediaItem == null || !this.Queue.Contains(mediaItem))
        return false;
      this.Queue.CurrentIndex = this.Queue.IndexOf(mediaItem);
      await this.MediaPlayer.Play(mediaItem);
      return true;
    }

    public virtual async Task<bool> PlayQueueItem(int index)
    {
      IMediaItem mediaItem = this.Queue.ElementAtOrDefault<IMediaItem>(index);
      if (mediaItem == null)
        return false;
      this.Queue.CurrentIndex = index;
      await this.MediaPlayer.Play(mediaItem);
      return true;
    }

    public virtual Task StepBackward()
    {
      double num;
      if (!double.IsNaN(this.Position.TotalSeconds))
      {
        TimeSpan timeSpan = this.Position;
        double totalSeconds1 = timeSpan.TotalSeconds;
        timeSpan = this.StepSizeBackward;
        double totalSeconds2 = timeSpan.TotalSeconds;
        if (totalSeconds1 >= totalSeconds2)
        {
          timeSpan = this.Position;
          double totalSeconds3 = timeSpan.TotalSeconds;
          timeSpan = this.StepSizeBackward;
          double totalSeconds4 = timeSpan.TotalSeconds;
          num = totalSeconds3 - totalSeconds4;
        }
        else
          num = 0.0;
      }
      else
        num = 0.0;
      Task task = this.SeekTo(TimeSpan.FromSeconds(num));
      this.Timer_Elapsed((object) null, (ElapsedEventArgs) null);
      return task;
    }

    public virtual Task StepForward()
    {
      double num;
      if (!double.IsNaN(this.Position.TotalSeconds))
      {
        TimeSpan timeSpan = this.Position;
        double totalSeconds1 = timeSpan.TotalSeconds;
        timeSpan = this.StepSizeForward;
        double totalSeconds2 = timeSpan.TotalSeconds;
        num = totalSeconds1 + totalSeconds2;
      }
      else
        num = 0.0;
      Task task = this.SeekTo(TimeSpan.FromSeconds(num));
      this.Timer_Elapsed((object) null, (ElapsedEventArgs) null);
      return task;
    }

    public event StateChangedEventHandler StateChanged;

    public event BufferedChangedEventHandler BufferedChanged;

    public event PositionChangedEventHandler PositionChanged;

    public event MediaItemFinishedEventHandler MediaItemFinished;

    public event MediaItemChangedEventHandler MediaItemChanged;

    public event MediaItemFailedEventHandler MediaItemFailed;

    internal void OnBufferedChanged(object sender, BufferedChangedEventArgs e)
    {
      BufferedChangedEventHandler bufferedChanged = this.BufferedChanged;
      if (bufferedChanged == null)
        return;
      bufferedChanged(sender, e);
    }

    internal void OnMediaItemChanged(object sender, MediaItemEventArgs e)
    {
      if (!this.SetProperty<IMediaItem>(ref this._currentSource, e.MediaItem, nameof (OnMediaItemChanged)))
        return;
      MediaItemChangedEventHandler mediaItemChanged = this.MediaItemChanged;
      if (mediaItemChanged == null)
        return;
      mediaItemChanged(sender, e);
    }

    internal async void OnMediaItemFailed(object sender, MediaItemFailedEventArgs e)
    {
      MediaItemFailedEventHandler mediaItemFailed = this.MediaItemFailed;
      if (mediaItemFailed != null)
        mediaItemFailed(sender, e);
      if (this.RetryPlayOnFailed && this.RetryCount < this.MaxRetryCount)
      {
        ++this.RetryCount;
        await this.Play();
      }
      else
      {
        this.RetryCount = 0;
        if (!this.PlayNextOnFailed)
          return;
        int num = await this.PlayNext() ? 1 : 0;
      }
    }

    internal void OnMediaItemFinished(object sender, MediaItemEventArgs e)
    {
      MediaItemFinishedEventHandler mediaItemFinished = this.MediaItemFinished;
      if (mediaItemFinished == null)
        return;
      mediaItemFinished(sender, e);
    }

    internal void OnPositionChanged(object sender, PositionChangedEventArgs e)
    {
      PositionChangedEventHandler positionChanged = this.PositionChanged;
      if (positionChanged != null)
        positionChanged(sender, e);
      this.Notification?.UpdateNotification();
    }

    internal void OnStateChanged(object sender, StateChangedEventArgs e)
    {
      StateChangedEventHandler stateChanged = this.StateChanged;
      if (stateChanged != null)
        stateChanged(sender, e);
      this.OnPropertyChanged("Duration");
      this.Notification?.UpdateNotification();
    }

    protected TimeSpan PreviousPosition
    {
      get => this._previousPosition;
      set
      {
        if (!this.SetProperty<TimeSpan>(ref this._previousPosition, value, nameof (PreviousPosition)))
          return;
        this.OnPositionChanged((object) this, new PositionChangedEventArgs(this.Position));
      }
    }

    protected virtual void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      if (!this.IsInitialized)
        return;
      this.PreviousPosition = this.Position;
    }

    public virtual void Dispose()
    {
      this.Timer.Elapsed -= new ElapsedEventHandler(this.Timer_Elapsed);
      this.Timer.Dispose();
    }
  }
}
