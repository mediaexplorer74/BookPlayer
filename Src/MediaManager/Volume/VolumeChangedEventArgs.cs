// Decompiled with JetBrains decompiler
// Type: MediaManager.Volume.VolumeChangedEventArgs
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using System;

#nullable disable
namespace MediaManager.Volume
{
  public class VolumeChangedEventArgs : EventArgs
  {
    public VolumeChangedEventArgs(int newVolume, bool muted)
    {
      this.NewVolume = newVolume;
      this.Muted = muted;
    }

    public int NewVolume { get; }

    public bool Muted { get; }
  }
}
