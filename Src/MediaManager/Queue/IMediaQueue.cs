// Decompiled with JetBrains decompiler
// Type: MediaManager.Queue.IMediaQueue
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using MediaManager.Library;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

#nullable disable
namespace MediaManager.Queue
{
  public interface IMediaQueue : 
    IList<IMediaItem>,
    ICollection<IMediaItem>,
    IEnumerable<IMediaItem>,
    IEnumerable,
    INotifyPropertyChanged
  {
    event QueueEndedEventHandler QueueEnded;

    event QueueChangedEventHandler QueueChanged;

    ObservableCollection<IMediaItem> MediaItems { get; }

    string Title { get; set; }

    bool HasNext { get; }

    IMediaItem Next { get; }

    bool HasPrevious { get; }

    IMediaItem Previous { get; }

    bool HasCurrent { get; }

    int CurrentIndex { get; set; }

    IMediaItem Current { get; }

    void Move(int oldIndex, int newIndex);
  }
}
