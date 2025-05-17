// Decompiled with JetBrains decompiler
// Type: MediaManager.Notifications.INotificationManager
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using System.ComponentModel;

#nullable disable
namespace MediaManager.Notifications
{
  public interface INotificationManager : INotifyPropertyChanged
  {
    bool Enabled { get; set; }

    bool ShowPlayPauseControls { get; set; }

    bool ShowNavigationControls { get; set; }

    void UpdateNotification();
  }
}
