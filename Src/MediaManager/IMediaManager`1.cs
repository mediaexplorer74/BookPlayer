// Decompiled with JetBrains decompiler
// Type: MediaManager.IMediaManager`1
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using MediaManager.Playback;
using System;
using System.ComponentModel;

#nullable disable
namespace MediaManager
{
  public interface IMediaManager<TPlayer> : 
    IMediaManager,
    IPlaybackManager,
    INotifyPropertyChanged,
    IDisposable
    where TPlayer : class
  {
    TPlayer Player { get; }
  }
}
