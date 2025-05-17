// Decompiled with JetBrains decompiler
// Type: MediaManager.Forms.VideoView
// Assembly: MediaManager.Forms, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 775A528A-0AB9-4EB3-B8E7-9E6E2449F6CA
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.Forms.dll

using MediaManager.Library;
using MediaManager.Media;
using MediaManager.Playback;
using MediaManager.Player;
using MediaManager.Queue;
using MediaManager.Video;
using System;
using System.ComponentModel;
using Xamarin.Forms;

#nullable disable
namespace MediaManager.Forms
{
  public class VideoView : View, IDisposable
  {
        // Fix for CS0103: The name '__methodptr' does not exist in the current context
        // Replace all instances of '__methodptr' with the appropriate delegate method reference.

        public static readonly BindableProperty VideoAspectProperty = BindableProperty.Create(
            nameof(VideoAspect),
            typeof(VideoAspectMode),
            typeof(VideoView),
            VideoAspectMode.AspectFit,
            BindingMode.TwoWay,
            null,
            OnVideoAspectPropertyChanged);
        public static readonly BindableProperty AutoPlayProperty = BindableProperty.Create(
            nameof(AutoPlay),
            typeof(bool),
            typeof(VideoView),
            true,
            BindingMode.TwoWay,
            null,
            OnAutoPlayPropertyChanged);
        // Fix for CS0103: The name 'cctor' does not exist in the current context
        // Fix for CS1056: Unexpected character '\u003E'
        // Fix for CS1003: Syntax error, ',' expected
        // Fix for CS0103: The name 'b__88_2' does not exist in the current context

        // Replace the problematic code with the following corrected implementation:

        public static readonly BindableProperty BufferedProperty = BindableProperty.Create(
            nameof(Buffered),
            typeof(TimeSpan),
            typeof(VideoView),
            TimeSpan.Zero,
            BindingMode.TwoWay,
            null,
            null,
            null,
            null,
            (bindable) => TimeSpan.Zero // Default value delegate
        );
    
        public static readonly BindableProperty StateProperty = BindableProperty.Create(
            nameof(State),
            typeof(MediaPlayerState),
            typeof(VideoView),
            MediaPlayerState.Stopped,
            BindingMode.TwoWay,
            null,
            null,
            null,
            null,
            (bindable) => MediaPlayerState.Stopped // Default value delegate
        );
    
        public static readonly BindableProperty DurationProperty = BindableProperty.Create(
            nameof(Duration),
            typeof(TimeSpan),
            typeof(VideoView),
            null,
            BindingMode.TwoWay,
            null,
            null,
            null,
            null,
            (bindable) => TimeSpan.Zero // Default value delegate
        );
    
        public static readonly BindableProperty PositionProperty = BindableProperty.Create(
            nameof(Position),
            typeof(TimeSpan),
            typeof(VideoView),
            TimeSpan.Zero,
            BindingMode.TwoWay,
            null,
            null,
            null,
            null,
            (bindable) => TimeSpan.Zero // Default value delegate
        );
       
        public static readonly BindableProperty ShowControlsProperty 
            = BindableProperty.Create(
            nameof(ShowControls),
            typeof(bool),
            typeof(VideoView),
            false,
            BindingMode.TwoWay,
            null,
            OnShowControlsPropertyChanged);
     
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            nameof(Source),
            typeof(object),
            typeof(VideoView),
            null,
            BindingMode.TwoWay,
            null,
            OnSourcePropertyChanged 
        );
    public static readonly BindableProperty CurrentProperty 
            = BindableProperty.Create(nameof (Current),
                typeof (IMediaItem), typeof (VideoView), (object) null, (BindingMode) 2,
                (BindableProperty.ValidateValueDelegate) null, 
                (BindableProperty.BindingPropertyChangedDelegate) null, 
                (BindableProperty.BindingPropertyChangingDelegate) null,
                (BindableProperty.CoerceValueDelegate) null, (BindableProperty.CreateDefaultValueDelegate) null);
        public static readonly BindableProperty RepeatProperty = BindableProperty.Create(
            nameof(Repeat),
            typeof(RepeatMode),
            typeof(VideoView),
            RepeatMode.Off,
            BindingMode.TwoWay,
            null,
            OnRepeatPropertyChanged);
        public static readonly BindableProperty ShuffleProperty = BindableProperty.Create(
            nameof(Shuffle),
            typeof(ShuffleMode),
            typeof(VideoView),
            ShuffleMode.Off,
            BindingMode.TwoWay,
            null,
            OnShufflePropertyChanged);
        public static readonly BindableProperty VideoHeightProperty = BindableProperty.Create(
            nameof(VideoHeight),
            typeof(int),
            typeof(VideoView),
            0,
            BindingMode.TwoWay,
            null,
            null,
            null,
            null,
            (bindable) => 0 // Default value delegate
        );
        public static readonly BindableProperty VideoWidthProperty = BindableProperty.Create(
            nameof(VideoWidth),
            typeof(int),
            typeof(VideoView),
            0,
            BindingMode.TwoWay,
            null,
            null,
            null,
            null,
            (bindable) => 0 // Default value delegate
        );
        public static readonly BindableProperty VolumeProperty = BindableProperty.Create(
            nameof(Volume),
            typeof(int),
            typeof(VideoView),
            1,
            BindingMode.TwoWay,
            null,
            OnVolumePropertyChanged);
        public static readonly BindableProperty SpeedProperty = BindableProperty.Create(
            nameof(Speed),
            typeof(float),
            typeof(VideoView),
            1f,
            BindingMode.TwoWay,
            null,
            OnSpeedPropertyChanged);
        public static readonly BindableProperty VideoPlaceholderProperty = BindableProperty.Create(
            nameof(VideoPlaceholder),
            typeof(ImageSource),
            typeof(VideoView),
            null,
            BindingMode.TwoWay,
            null,
            OnVideoPlaceholderPropertyChanged);

    protected static IMediaManager MediaManager => CrossMediaManager.Current;

    protected static IMediaPlayer MediaPlayer => VideoView.MediaManager.MediaPlayer;

    protected static IVideoView PlayerView => VideoView.MediaPlayer.VideoView;

    public VideoView()
    {
      VideoView.MediaManager.BufferedChanged += 
                new BufferedChangedEventHandler(this.MediaManager_BufferedChanged);

      VideoView.MediaManager.PositionChanged += 
                new PositionChangedEventHandler(this.MediaManager_PositionChanged);

      VideoView.MediaManager.StateChanged += 
                new StateChangedEventHandler(this.MediaManager_StateChanged);

      VideoView.MediaManager.MediaItemChanged += 
                new MediaItemChangedEventHandler(this.MediaManager_MediaItemChanged);

      VideoView.MediaManager.Queue.QueueChanged += 
                new QueueChangedEventHandler(this.MediaQueue_QueueChanged);

      VideoView.MediaManager.PropertyChanged += 
                new PropertyChangedEventHandler(this.MediaManager_PropertyChanged);

      VideoView.MediaManager.MediaPlayer.PropertyChanged +=
                new PropertyChangedEventHandler(this.MediaPlayer_PropertyChanged);
    }

    protected virtual void MediaPlayer_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      switch (e.PropertyName)
      {
        case "ShowPlaybackControls":
          this.ShowControls = VideoView.MediaPlayer.ShowPlaybackControls;
          break;
        case "VideoAspect":
          this.VideoAspect = VideoView.MediaPlayer.VideoAspect;
          break;
        case "VideoHeight":
          this.VideoHeight = VideoView.MediaPlayer.VideoHeight;
          break;
        case "VideoWidth":
          this.VideoWidth = VideoView.MediaPlayer.VideoWidth;
          break;
        case "VideoPlaceholder":
          if (!(VideoView.MediaPlayer.VideoPlaceholder is ImageSource videoPlaceholder))
            break;
          this.VideoPlaceholder = videoPlaceholder;
          break;
      }
    }

    protected virtual void MediaManager_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      switch (e.PropertyName)
      {
        case "Duration":
          this.Duration = VideoView.MediaManager.Duration;
          break;
        case "AutoPlay":
          this.AutoPlay = VideoView.MediaManager.AutoPlay;
          break;
        case "RepeatMode":
          this.Repeat = VideoView.MediaManager.RepeatMode;
          break;
        case "ShuffleMode":
          this.Shuffle = VideoView.MediaManager.ShuffleMode;
          break;
        case "Speed":
          this.Speed = VideoView.MediaManager.Speed;
          break;
      }
    }

    protected virtual void MediaQueue_QueueChanged(object sender, QueueChangedEventArgs e)
    {
    }

    protected virtual void MediaManager_MediaItemChanged(object sender, MediaItemEventArgs e)
    {
      this.Current = e.MediaItem;
    }

    protected virtual void MediaManager_StateChanged(object sender, StateChangedEventArgs e)
    {
      this.State = e.State;
    }

    protected virtual void MediaManager_PositionChanged(object sender,
        MediaManager.Playback.PositionChangedEventArgs e)
    {
      this.Position = e.Position;
    }

    protected virtual void MediaManager_BufferedChanged(object sender, BufferedChangedEventArgs e)
    {
      this.Buffered = e.Buffered;
    }

    public VideoAspectMode VideoAspect
    {
      get => (VideoAspectMode) ((BindableObject) this).GetValue(VideoView.VideoAspectProperty);
      set => ((BindableObject) this).SetValue(VideoView.VideoAspectProperty, (object) value);
    }

    public RepeatMode Repeat
    {
      get => (RepeatMode) ((BindableObject) this).GetValue(VideoView.RepeatProperty);
      set => ((BindableObject) this).SetValue(VideoView.RepeatProperty, (object) value);
    }

    public ShuffleMode Shuffle
    {
      get => (ShuffleMode) ((BindableObject) this).GetValue(VideoView.ShuffleProperty);
      set => ((BindableObject) this).SetValue(VideoView.ShuffleProperty, (object) value);
    }

    public bool AutoPlay
    {
      get => (bool) ((BindableObject) this).GetValue(VideoView.AutoPlayProperty);
      set => ((BindableObject) this).SetValue(VideoView.AutoPlayProperty, (object) value);
    }

    public TimeSpan Buffered
    {
      get => (TimeSpan) ((BindableObject) this).GetValue(VideoView.BufferedProperty);
      internal set => ((BindableObject) this).SetValue(VideoView.BufferedProperty, (object) value);
    }

    public MediaPlayerState State
    {
      get => (MediaPlayerState) ((BindableObject) this).GetValue(VideoView.StateProperty);
      internal set => ((BindableObject) this).SetValue(VideoView.StateProperty, (object) value);
    }

    public TimeSpan Duration
    {
      get => (TimeSpan) ((BindableObject) this).GetValue(VideoView.DurationProperty);
      internal set => ((BindableObject) this).SetValue(VideoView.DurationProperty, (object) value);
    }

    public bool ShowControls
    {
      get => (bool) ((BindableObject) this).GetValue(VideoView.ShowControlsProperty);
      set => ((BindableObject) this).SetValue(VideoView.ShowControlsProperty, (object) value);
    }

    public TimeSpan Position
    {
      get => (TimeSpan) ((BindableObject) this).GetValue(VideoView.PositionProperty);
      internal set => ((BindableObject) this).SetValue(VideoView.PositionProperty, (object) value);
    }

    public IMediaItem Current
    {
      get => (IMediaItem) ((BindableObject) this).GetValue(VideoView.CurrentProperty);
      internal set => ((BindableObject) this).SetValue(VideoView.CurrentProperty, (object) value);
    }

    public object Source
    {
      get => ((BindableObject) this).GetValue(VideoView.SourceProperty);
      set => ((BindableObject) this).SetValue(VideoView.SourceProperty, value);
    }

    public int VideoHeight
    {
      get => (int) ((BindableObject) this).GetValue(VideoView.VideoHeightProperty);
      internal set
      {
        ((BindableObject) this).SetValue(VideoView.VideoHeightProperty, (object) value);
      }
    }

    public int VideoWidth
    {
      get => (int) ((BindableObject) this).GetValue(VideoView.VideoWidthProperty);
      internal set
      {
        ((BindableObject) this).SetValue(VideoView.VideoWidthProperty, (object) value);
      }
    }

    public int Volume
    {
      get => (int) ((BindableObject) this).GetValue(VideoView.VolumeProperty);
      set => ((BindableObject) this).SetValue(VideoView.VolumeProperty, (object) value);
    }

    public float Speed
    {
      get => (float) ((BindableObject) this).GetValue(VideoView.SpeedProperty);
      set => ((BindableObject) this).SetValue(VideoView.SpeedProperty, (object) value);
    }

    public ImageSource VideoPlaceholder
    {
      get => (ImageSource) ((BindableObject) this).GetValue(VideoView.VideoPlaceholderProperty);
      set => ((BindableObject) this).SetValue(VideoView.VideoPlaceholderProperty, (object) value);
    }

    private static async void OnSourcePropertyChanged(
      BindableObject bindable,
      object oldValue,
      object newValue)
    {
      IMediaItem mediaItem = await CrossMediaManager.Current.Play(newValue);
    }

        private static void OnShowControlsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            VideoView.MediaManager.MediaPlayer.ShowPlaybackControls = (bool)newValue;
        }

        // Fix for CS1056: Unexpected character '\u003C' and '\u003E'
        // Ensure that all generic type parameters and method references are properly formatted.
        // Fix for CS1001: Identifier expected
        // Ensure that all identifiers are properly named and declared.
        // Fix for CS1003: Syntax error, ',' expected
        // Ensure that all method calls and property declarations have the correct syntax.

        private static void OnVideoAspectPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            VideoView.MediaManager.MediaPlayer.VideoAspect = (VideoAspectMode)newValue;
        }

        private static void OnRepeatPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            VideoView.MediaManager.RepeatMode = (RepeatMode)newValue;
        }

        private static void OnShufflePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            VideoView.MediaManager.ShuffleMode = (ShuffleMode)newValue;
        }

        private static void OnVolumePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            VideoView.MediaManager.Volume.CurrentVolume = (int)newValue;
        }

        private static void OnSpeedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            VideoView.MediaManager.Speed = (float)newValue;
        }

        private static void OnAutoPlayPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            VideoView.MediaManager.AutoPlay = (bool)newValue;
        }

        private static void OnVideoPlaceholderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // Placeholder logic can be added here if needed.
        }

    public virtual void Dispose()
    {
      VideoView.MediaManager.BufferedChanged -= new BufferedChangedEventHandler(this.MediaManager_BufferedChanged);
      VideoView.MediaManager.PositionChanged -= new PositionChangedEventHandler(this.MediaManager_PositionChanged);
      VideoView.MediaManager.StateChanged -= new StateChangedEventHandler(this.MediaManager_StateChanged);
      VideoView.MediaManager.MediaItemChanged -= new MediaItemChangedEventHandler(this.MediaManager_MediaItemChanged);
      VideoView.MediaManager.Queue.QueueChanged -= new QueueChangedEventHandler(this.MediaQueue_QueueChanged);
      VideoView.MediaManager.PropertyChanged -= new PropertyChangedEventHandler(this.MediaManager_PropertyChanged);
      VideoView.MediaManager.MediaPlayer.PropertyChanged -= new PropertyChangedEventHandler(this.MediaPlayer_PropertyChanged);
    }
  }
}
