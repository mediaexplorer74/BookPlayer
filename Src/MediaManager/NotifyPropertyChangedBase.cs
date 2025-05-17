// Decompiled with JetBrains decompiler
// Type: MediaManager.NotifyPropertyChangedBase
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

#nullable disable
namespace MediaManager
{
  public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
      if (EqualityComparer<T>.Default.Equals(storage, value))
        return false;
      storage = value;
      this.OnPropertyChanged(propertyName);
      return true;
    }

    protected virtual void SetProperty<T>(
      ref T storage,
      T value,
      Action<bool> action,
      [CallerMemberName] string propertyName = null)
    {
      if (action == null)
        throw new ArgumentException("action should not be null", nameof (action));
      action(this.SetProperty<T>(ref storage, value, propertyName));
    }

    protected virtual bool SetProperty<T>(
      ref T storage,
      T value,
      Action afterAction,
      [CallerMemberName] string propertyName = null)
    {
      if (!this.SetProperty<T>(ref storage, value, propertyName))
        return false;
      if (afterAction != null)
        afterAction();
      return true;
    }
  }
}
