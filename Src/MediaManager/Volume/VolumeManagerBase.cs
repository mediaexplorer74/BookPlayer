// Decompiled with JetBrains decompiler
// Type: MediaManager.Volume.VolumeManagerBase
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using System.ComponentModel;

#nullable disable
namespace MediaManager.Volume
{
  public abstract class VolumeManagerBase : 
    NotifyPropertyChangedBase,
    IVolumeManager,
    INotifyPropertyChanged
  {
    public abstract int CurrentVolume { get; set; }

    public abstract int MaxVolume { get; set; }

    public abstract float Balance { get; set; }

    public abstract bool Muted { get; set; }

    public abstract event VolumeChangedEventHandler VolumeChanged;
  }
}
