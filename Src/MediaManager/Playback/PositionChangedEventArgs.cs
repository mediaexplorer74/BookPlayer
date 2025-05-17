// Decompiled with JetBrains decompiler
// Type: MediaManager.Playback.PositionChangedEventArgs
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using System;

#nullable disable
namespace MediaManager.Playback
{
  public class PositionChangedEventArgs : EventArgs
  {
    public PositionChangedEventArgs(TimeSpan position) => this.Position = position;

    public TimeSpan Position { get; }
  }
}
