// Decompiled with JetBrains decompiler
// Type: MediaManager.Player.IMediaPlayer`2
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using MediaManager.Video;
using System;
using System.ComponentModel;

#nullable disable
namespace MediaManager.Player
{
  public interface IMediaPlayer<TPlayer, TPlayerView> : 
    IMediaPlayer<TPlayer>,
    IMediaPlayer,
    IDisposable,
    INotifyPropertyChanged
    where TPlayer : class
    where TPlayerView : class, IVideoView
  {
    TPlayerView PlayerView { get; }
  }
}
