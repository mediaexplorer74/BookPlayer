// Decompiled with JetBrains decompiler
// Type: MediaManager.Library.MediaItem
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using MediaManager.Media;
using System;
using System.ComponentModel;

#nullable disable
namespace MediaManager.Library
{
  public class MediaItem : ContentItem, IMediaItem, IContentItem, INotifyPropertyChanged
  {
    private string _advertisement;
    private string _album;
    private object _albumArt;
    private string _albumArtist;
    private string _albumArtUri;
    private object _art;
    private string _artist;
    private string _artUri;
    private string _author;
    private string _compilation;
    private string _composer;
    private DateTime _date;
    private int _discNumber;
    private object _displayImage;
    private string _displayImageUri;
    private string _displayTitle;
    private string _displaySubtitle;
    private string _displayDescription;
    private DownloadStatus _downloadStatus;
    private TimeSpan _duration;
    private object _extras;
    private string _genre;
    private string _mediaUri;
    private int _numTracks;
    private object _rating;
    private string _title;
    private int _trackNumber;
    private object _userRating;
    private string _writer;
    private int _year;
    private string _fileExtension;
    private string _fileName;
    private MediaType _mediaType;
    private MediaLocation _mediaLocation;
    private bool _isMetadataExtracted;
    private bool _isLive;

    public MediaItem()
    {
    }

    public MediaItem(string uri)
    {
      this.MediaUri = !string.IsNullOrEmpty(uri) ? uri : throw new ArgumentNullException(uri);
    }

    public event MetadataUpdatedEventHandler MetadataUpdated;

    public string Advertisement
    {
      get => this._advertisement;
      set => this.SetProperty<string>(ref this._advertisement, value, nameof (Advertisement));
    }

    public string Album
    {
      get => this._album;
      set => this.SetProperty<string>(ref this._album, value, nameof (Album));
    }

    public object AlbumImage
    {
      get => this._albumArt;
      set => this.SetProperty<object>(ref this._albumArt, value, nameof (AlbumImage));
    }

    public string AlbumArtist
    {
      get => this._albumArtist;
      set => this.SetProperty<string>(ref this._albumArtist, value, nameof (AlbumArtist));
    }

    public string AlbumImageUri
    {
      get => this._albumArtUri;
      set => this.SetProperty<string>(ref this._albumArtUri, value, nameof (AlbumImageUri));
    }

    public object Image
    {
      get => this._art;
      set => this.SetProperty<object>(ref this._art, value, nameof (Image));
    }

    public string Artist
    {
      get => this._artist;
      set => this.SetProperty<string>(ref this._artist, value, nameof (Artist));
    }

    public string ImageUri
    {
      get => this._artUri;
      set => this.SetProperty<string>(ref this._artUri, value, nameof (ImageUri));
    }

    public string Author
    {
      get => this._author;
      set => this.SetProperty<string>(ref this._author, value, nameof (Author));
    }

    public string Compilation
    {
      get => this._compilation;
      set => this.SetProperty<string>(ref this._compilation, value, nameof (Compilation));
    }

    public string Composer
    {
      get => this._composer;
      set => this.SetProperty<string>(ref this._composer, value, nameof (Composer));
    }

    public DateTime Date
    {
      get => this._date;
      set => this.SetProperty<DateTime>(ref this._date, value, nameof (Date));
    }

    public int DiscNumber
    {
      get => this._discNumber;
      set => this.SetProperty<int>(ref this._discNumber, value, nameof (DiscNumber));
    }

    public object DisplayImage
    {
      get
      {
        if (this._displayImage != null)
          return this._displayImage;
        if (this.Image != null)
          return this.Image;
        return this.AlbumImage != null ? this.AlbumImage : (object) null;
      }
      set => this.SetProperty<object>(ref this._displayImage, value, nameof (DisplayImage));
    }

    public string DisplayImageUri
    {
      get
      {
        if (!string.IsNullOrEmpty(this._displayImageUri))
          return this._displayImageUri;
        if (!string.IsNullOrEmpty(this.ImageUri))
          return this.ImageUri;
        return !string.IsNullOrEmpty(this.AlbumImageUri) ? this.AlbumImageUri : string.Empty;
      }
      set => this.SetProperty<string>(ref this._displayImageUri, value, nameof (DisplayImageUri));
    }

    public string DisplayTitle
    {
      get
      {
        if (!string.IsNullOrEmpty(this._displayTitle))
          return this._displayTitle;
        if (!string.IsNullOrEmpty(this.Title))
          return this.Title;
        return !string.IsNullOrEmpty(this.FileName) ? this.FileName : string.Empty;
      }
      set => this.SetProperty<string>(ref this._displayTitle, value, nameof (DisplayTitle));
    }

    public string DisplaySubtitle
    {
      get
      {
        if (!string.IsNullOrEmpty(this._displaySubtitle))
          return this._displaySubtitle;
        if (!string.IsNullOrEmpty(this.Artist))
          return this.Artist;
        if (!string.IsNullOrEmpty(this.AlbumArtist))
          return this.AlbumArtist;
        return !string.IsNullOrEmpty(this.Album) ? this.Album : string.Empty;
      }
      set => this.SetProperty<string>(ref this._displaySubtitle, value, nameof (DisplaySubtitle));
    }

    public string DisplayDescription
    {
      get
      {
        if (!string.IsNullOrEmpty(this._displayDescription))
          return this._displayDescription;
        if (!string.IsNullOrEmpty(this.Album))
          return this.Album;
        if (!string.IsNullOrEmpty(this.Artist))
          return this.Artist;
        return !string.IsNullOrEmpty(this.AlbumArtist) ? this.AlbumArtist : string.Empty;
      }
      set
      {
        this.SetProperty<string>(ref this._displayDescription, value, nameof (DisplayDescription));
      }
    }

    public DownloadStatus DownloadStatus
    {
      get => this._downloadStatus;
      set
      {
        this.SetProperty<DownloadStatus>(ref this._downloadStatus, value, nameof (DownloadStatus));
      }
    }

    public TimeSpan Duration
    {
      get => this._duration;
      set => this.SetProperty<TimeSpan>(ref this._duration, value, nameof (Duration));
    }

    public object Extras
    {
      get => this._extras;
      set => this.SetProperty<object>(ref this._extras, value, nameof (Extras));
    }

    public string Genre
    {
      get => this._genre;
      set => this.SetProperty<string>(ref this._genre, value, nameof (Genre));
    }

    public string MediaUri
    {
      get => this._mediaUri;
      set => this.SetProperty<string>(ref this._mediaUri, value, nameof (MediaUri));
    }

    public int NumTracks
    {
      get => this._numTracks;
      set => this.SetProperty<int>(ref this._numTracks, value, nameof (NumTracks));
    }

    public object Rating
    {
      get => this._rating;
      set => this.SetProperty<object>(ref this._rating, value, nameof (Rating));
    }

    public string Title
    {
      get => this._title;
      set => this.SetProperty<string>(ref this._title, value, nameof (Title));
    }

    public int TrackNumber
    {
      get => this._trackNumber;
      set => this.SetProperty<int>(ref this._trackNumber, value, nameof (TrackNumber));
    }

    public object UserRating
    {
      get => this._userRating;
      set => this.SetProperty<object>(ref this._userRating, value, nameof (UserRating));
    }

    public string Writer
    {
      get => this._writer;
      set => this.SetProperty<string>(ref this._writer, value, nameof (Writer));
    }

    public int Year
    {
      get => this._year;
      set => this.SetProperty<int>(ref this._year, value, nameof (Year));
    }

    public string FileExtension
    {
      get => this._fileExtension;
      set => this.SetProperty<string>(ref this._fileExtension, value, nameof (FileExtension));
    }

    public string FileName
    {
      get => this._fileName;
      set => this.SetProperty<string>(ref this._fileName, value, nameof (FileName));
    }

    public MediaType MediaType
    {
      get => this._mediaType;
      set => this.SetProperty<MediaType>(ref this._mediaType, value, nameof (MediaType));
    }

    public MediaLocation MediaLocation
    {
      get => this._mediaLocation;
      set
      {
        this.SetProperty<MediaLocation>(ref this._mediaLocation, value, nameof (MediaLocation));
      }
    }

    public bool IsMetadataExtracted
    {
      get => this._isMetadataExtracted;
      set
      {
        if (!this.SetProperty<bool>(ref this._isMetadataExtracted, value, nameof (IsMetadataExtracted)))
          return;
        MetadataUpdatedEventHandler metadataUpdated = this.MetadataUpdated;
        if (metadataUpdated == null)
          return;
        metadataUpdated((object) this, new MetadataChangedEventArgs((IMediaItem) this));
      }
    }

    public bool IsLive
    {
      get => this._isLive;
      set => this.SetProperty<bool>(ref this._isLive, value, nameof (IsLive));
    }
  }
}
