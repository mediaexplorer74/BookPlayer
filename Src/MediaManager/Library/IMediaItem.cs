// Decompiled with JetBrains decompiler
// Type: MediaManager.Library.IMediaItem
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using System;
using System.ComponentModel;

#nullable disable
namespace MediaManager.Library
{
  public interface IMediaItem : IContentItem, INotifyPropertyChanged
  {
    bool IsMetadataExtracted { get; set; }

    event MetadataUpdatedEventHandler MetadataUpdated;

    string Advertisement { get; set; }

    string Album { get; set; }

    string AlbumArtist { get; set; }

    object AlbumImage { get; set; }

    string AlbumImageUri { get; set; }

    string Artist { get; set; }

    object Image { get; set; }

    string ImageUri { get; set; }

    string Author { get; set; }

    string Compilation { get; set; }

    string Composer { get; set; }

    DateTime Date { get; set; }

    int DiscNumber { get; set; }

    object DisplayImage { get; set; }

    string DisplayImageUri { get; set; }

    string DisplayDescription { get; set; }

    string DisplaySubtitle { get; set; }

    string DisplayTitle { get; set; }

    DownloadStatus DownloadStatus { get; set; }

    TimeSpan Duration { get; set; }

    object Extras { get; set; }

    string Genre { get; set; }

    string MediaUri { get; set; }

    int NumTracks { get; set; }

    object Rating { get; set; }

    string Title { get; set; }

    int TrackNumber { get; set; }

    object UserRating { get; set; }

    string Writer { get; set; }

    int Year { get; set; }

    string FileExtension { get; set; }

    string FileName { get; set; }

    MediaType MediaType { get; set; }

    MediaLocation MediaLocation { get; set; }

    bool IsLive { get; set; }
  }
}
