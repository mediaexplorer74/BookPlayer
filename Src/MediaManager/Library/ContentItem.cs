// Decompiled with JetBrains decompiler
// Type: MediaManager.Library.ContentItem
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using System;
using System.ComponentModel;

#nullable disable
namespace MediaManager.Library
{
  public class ContentItem : NotifyPropertyChangedBase, IContentItem, INotifyPropertyChanged
  {
    private string _id = Guid.NewGuid().ToString();

    public string Id
    {
      get => this._id;
      set => this.SetProperty<string>(ref this._id, value, nameof (Id));
    }
  }
}
