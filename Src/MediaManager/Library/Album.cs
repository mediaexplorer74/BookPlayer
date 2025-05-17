// Decompiled with JetBrains decompiler
// Type: MediaManager.Library.Album
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace MediaManager.Library
{
  public class Album : ContentItem, IAlbum, IContentItem, INotifyPropertyChanged
  {
    private string _title;
    private string _description;
    private string _tags;
    private string _genre;
    private object _image;
    private string _imageUri;
    private string _labelName;
    private object _rating;
    private DateTime _releaseDate;
    private TimeSpan _duration;
    private IList<IArtist> _artists = (IList<IArtist>) new List<IArtist>();
    private IList<IMediaItem> _mediaItems = (IList<IMediaItem>) new List<IMediaItem>();

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

    public string LabelName
    {
      get => this._labelName;
      set => this.SetProperty<string>(ref this._labelName, value, nameof (LabelName));
    }

    public object Rating
    {
      get => this._rating;
      set => this.SetProperty<object>(ref this._rating, value, nameof (Rating));
    }

    public DateTime ReleaseDate
    {
      get => this._releaseDate;
      set => this.SetProperty<DateTime>(ref this._releaseDate, value, nameof (ReleaseDate));
    }

    public virtual TimeSpan Duration
    {
      get
      {
        TimeSpan duration = this._duration;
        return this._duration;
      }
      set => this.SetProperty<TimeSpan>(ref this._duration, value, nameof (Duration));
    }

    public IList<IArtist> Artists
    {
      get => this._artists;
      set => this.SetProperty<IList<IArtist>>(ref this._artists, value, nameof (Artists));
    }

    public IList<IMediaItem> MediaItems
    {
      get => this._mediaItems;
      set => this.SetProperty<IList<IMediaItem>>(ref this._mediaItems, value, nameof (MediaItems));
    }
  }
}
