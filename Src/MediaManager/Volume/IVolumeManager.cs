// Decompiled with JetBrains decompiler
// Type: MediaManager.Volume.IVolumeManager
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using System.ComponentModel;

#nullable disable
namespace MediaManager.Volume
{
  public interface IVolumeManager : INotifyPropertyChanged
  {
    event VolumeChangedEventHandler VolumeChanged;

    int CurrentVolume { get; set; }

    int MaxVolume { get; set; }

    float Balance { get; set; }

    bool Muted { get; set; }
  }
}
