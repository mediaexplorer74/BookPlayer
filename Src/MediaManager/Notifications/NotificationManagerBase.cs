// Decompiled with JetBrains decompiler
// Type: MediaManager.Notifications.NotificationManagerBase
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using System.ComponentModel;

#nullable disable
namespace MediaManager.Notifications
{
  public abstract class NotificationManagerBase : 
    NotifyPropertyChangedBase,
    INotificationManager,
    INotifyPropertyChanged
  {
    private bool _enabled = true;
    private bool _showPlayPauseControls = true;
    private bool _showNavigationControls = true;

    public virtual bool Enabled
    {
      get => this._enabled;
      set
      {
        if (!this.SetProperty<bool>(ref this._enabled, value, nameof (Enabled)))
          return;
        this.UpdateNotification();
      }
    }

    public virtual bool ShowPlayPauseControls
    {
      get => this._showPlayPauseControls;
      set
      {
        if (!this.SetProperty<bool>(ref this._showPlayPauseControls, value, nameof (ShowPlayPauseControls)))
          return;
        this.UpdateNotification();
      }
    }

    public virtual bool ShowNavigationControls
    {
      get => this._showNavigationControls;
      set
      {
        if (!this.SetProperty<bool>(ref this._showNavigationControls, value, nameof (ShowNavigationControls)))
          return;
        this.UpdateNotification();
      }
    }

    public abstract void UpdateNotification();
  }
}
