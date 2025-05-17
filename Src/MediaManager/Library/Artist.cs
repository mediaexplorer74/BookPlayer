// Decompiled with JetBrains decompiler
// Type: MediaManager.Library.Artist
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace MediaManager.Library
{
  public class Artist : ContentItem, IArtist, IContentItem, INotifyPropertyChanged
  {
    private string _name;
    private string _biography;
    private string _tags;
    private string _genre;
    private object _image;
    private string _imageUri;
    private object _rating;
    private IList<IAlbum> _albums = (IList<IAlbum>) new List<IAlbum>();
    private IList<IMediaItem> _topTracks = (IList<IMediaItem>) new List<IMediaItem>();

    public string Name
    {
      get => this._name;
      set => this.SetProperty<string>(ref this._name, value, nameof (Name));
    }

    public string Biography
    {
      get => this._biography;
      set => this.SetProperty<string>(ref this._biography, value, nameof (Biography));
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

    public IList<IAlbum> Albums
    {
      get => this._albums;
      set => this.SetProperty<IList<IAlbum>>(ref this._albums, value, nameof (Albums));
    }

    public IList<IMediaItem> TopTracks
    {
      get => this._topTracks;
      set => this.SetProperty<IList<IMediaItem>>(ref this._topTracks, value, nameof (TopTracks));
    }

    public virtual IList<IMediaItem> AllTracks
    {
      get
      {
        IList<IAlbum> albums = this.Albums;
        if (albums == null)
          return (IList<IMediaItem>) null;
        IEnumerable<IMediaItem> source = albums.SelectMany<IAlbum, IMediaItem>((Func<IAlbum, IEnumerable<IMediaItem>>) (x => (IEnumerable<IMediaItem>) x.MediaItems));
        return source == null ? (IList<IMediaItem>) null : (IList<IMediaItem>) source.ToList<IMediaItem>();
      }
    }
  }
}
