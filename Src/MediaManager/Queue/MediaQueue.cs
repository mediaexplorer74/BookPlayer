// Decompiled with JetBrains decompiler
// Type: MediaManager.Queue.MediaQueue
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using MediaManager.Library;
using MediaManager.Media;
using MediaManager.Playback;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace MediaManager.Queue
{
  public class MediaQueue : 
    NotifyPropertyChangedBase,
    IMediaQueue,
    IList<IMediaItem>,
    ICollection<IMediaItem>,
    IEnumerable<IMediaItem>,
    IEnumerable,
    INotifyPropertyChanged
  {
    private int shuffleKey = int.MinValue;
    private string _title;
    private int _currentIndex;

    protected MediaManagerBase MediaManager => CrossMediaManager.Current as MediaManagerBase;

    public MediaQueue()
    {
      this.MediaItems.CollectionChanged += new NotifyCollectionChangedEventHandler(this.MediaItems_CollectionChanged);
      this.MediaManager.PropertyChanged += new PropertyChangedEventHandler(this.MediaManager_PropertyChanged);
      this.MediaManager.MediaItemFinished += new MediaItemFinishedEventHandler(this.MediaManager_MediaItemFinished);
    }

    private void MediaManager_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "ShuffleMode"))
        return;
      if (this.MediaManager.ShuffleMode == ShuffleMode.All)
      {
        this.shuffleKey = new Random().Next(-2147483647, int.MaxValue);
        this.MediaItems.Shuffle<IMediaItem>(this.shuffleKey);
      }
      else
      {
        if (this.shuffleKey == int.MinValue)
          return;
        this.MediaItems.DeShuffle<IMediaItem>(this.shuffleKey);
        this.shuffleKey = int.MinValue;
      }
    }

    private void MediaItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.OnQueueChanged((object) this, new QueueChangedEventArgs(this.Current));
    }

    public event QueueEndedEventHandler QueueEnded;

    public event QueueChangedEventHandler QueueChanged;

    public ObservableCollection<IMediaItem> MediaItems { get; protected set; } = new ObservableCollection<IMediaItem>();

    public string Title
    {
      get => this._title;
      set => this.SetProperty<string>(ref this._title, value, nameof (Title));
    }

    public bool HasNext => this.MediaItems.Count > this.CurrentIndex + 1;

    public IMediaItem Next => this.HasNext ? this[this.CurrentIndex + 1] : (IMediaItem) null;

    public bool HasPrevious => this.CurrentIndex > 0;

    public IMediaItem Previous
    {
      get => this.HasPrevious ? this[this.CurrentIndex - 1] : (IMediaItem) null;
    }

    public bool HasCurrent => this.ElementAtOrDefault<IMediaItem>(this.CurrentIndex) != null;

    public IMediaItem Current
    {
      get => this.ElementAtOrDefault<IMediaItem>(this.CurrentIndex);
      internal set => this.CurrentIndex = this.MediaItems.IndexOf(value);
    }

    public int CurrentIndex
    {
      get => this._currentIndex;
      set
      {
        this.SetProperty<int>(ref this._currentIndex, value, nameof (CurrentIndex));
        if (this.Current == null)
          return;
        this.OnQueueChanged((object) this, new QueueChangedEventArgs(this.Current));
        this.MediaManager.OnMediaItemChanged((object) this, new MediaItemEventArgs(this.Current));
      }
    }

    internal void OnQueueEnded(object s, QueueEndedEventArgs e)
    {
      QueueEndedEventHandler queueEnded = this.QueueEnded;
      if (queueEnded == null)
        return;
      queueEnded(s, e);
    }

    internal void OnQueueChanged(object s, QueueChangedEventArgs e)
    {
      QueueChangedEventHandler queueChanged = this.QueueChanged;
      if (queueChanged != null)
        queueChanged(s, e);
      this.MediaManager?.Notification?.UpdateNotification();
    }

    private void MediaManager_MediaItemFinished(object sender, MediaItemEventArgs e)
    {
      if (this.MediaItems == null || this.MediaItems.Count == 0 || this.MediaItems.Last<IMediaItem>() != e.MediaItem)
        return;
      this.OnQueueEnded((object) this, new QueueEndedEventArgs(e.MediaItem));
    }

    public int Count => this.MediaItems.Count;

    public bool IsReadOnly => false;

    public IMediaItem this[int index]
    {
      get => this.MediaItems[index];
      set => this.MediaItems[index] = value;
    }

    public IEnumerator<IMediaItem> GetEnumerator() => this.MediaItems.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.MediaItems.GetEnumerator();

    public int IndexOf(IMediaItem item) => this.MediaItems.IndexOf(item);

    public void Insert(int index, IMediaItem item) => this.MediaItems.Insert(index, item);

    public void RemoveAt(int index) => this.MediaItems.RemoveAt(index);

    public void Add(IMediaItem item) => this.MediaItems.Add(item);

    public void Clear() => this.MediaItems.Clear();

    public bool Contains(IMediaItem item) => this.MediaItems.Contains(item);

    public void CopyTo(IMediaItem[] array, int arrayIndex)
    {
      this.MediaItems.CopyTo(array, arrayIndex);
    }

    public bool Remove(IMediaItem item) => this.MediaItems.Remove(item);

    public void Move(int oldIndex, int newIndex) => this.MediaItems.Move(oldIndex, newIndex);
  }
}
