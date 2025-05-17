// Decompiled with JetBrains decompiler
// Type: MediaManager.Library.Radio
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace MediaManager.Library
{
  public class Radio : ContentItem, IRadio, IContentItem, INotifyPropertyChanged
  {
    private string _uri;
    private string _title;
    private string _description;
    private string _tags;
    private string _genre;
    private object _image;
    private string _imageUri;
    private object _rating;
    private DateTime _createdAt;
    private DateTime _updatedAt;
    private SharingType _sharingType;
    private IList<IMediaItem> _mediaItems = (IList<IMediaItem>) new List<IMediaItem>();

    public Radio()
    {
      DateTime createdAt = this.CreatedAt;
    }

    public string Uri
    {
      get => this._uri;
      set => this.SetProperty<string>(ref this._uri, value, nameof (Uri));
    }

    public string Title
    {
      get => this._title;
      set => this.SetProperty<string>(ref this._title, value, nameof (Title));
    }

    public string Description
    {
      get => this._description;
      set => this.SetProperty<string>(ref this._description, value, nameof (Description));
    }

    public string Tags
    {
      get => this._tags;
      set => this.SetProperty<string>(ref this._tags, value, nameof (Tags));
    }

    public string Genre
    {
      get => this._genre;
      set => this.SetProperty<string>(ref this._genre, value, nameof (Genre));
    }

    public object Image
    {
      get => this._image;
      set => this.SetProperty<object>(ref this._image, value, nameof (Image));
    }

    public string ImageUri
    {
      get => this._imageUri;
      set => this.SetProperty<string>(ref this._imageUri, value, nameof (ImageUri));
    }

    public object Rating
    {
      get => this._rating;
      set => this.SetProperty<object>(ref this._rating, value, nameof (Rating));
    }

    public DateTime CreatedAt
    {
      get => this._createdAt;
      set => this.SetProperty<DateTime>(ref this._createdAt, value, nameof (CreatedAt));
    }

    public DateTime UpdatedAt
    {
      get => this._updatedAt;
      set => this.SetProperty<DateTime>(ref this._updatedAt, value, nameof (UpdatedAt));
    }

    public SharingType SharingType
    {
      get => this._sharingType;
      set => this.SetProperty<SharingType>(ref this._sharingType, value, nameof (SharingType));
    }

    public IList<IMediaItem> MediaItems
    {
      get => this._mediaItems;
      set => this.SetProperty<IList<IMediaItem>>(ref this._mediaItems, value, nameof (MediaItems));
    }
  }
}
