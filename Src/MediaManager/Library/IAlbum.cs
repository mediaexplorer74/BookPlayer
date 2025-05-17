// Decompiled with JetBrains decompiler
// Type: MediaManager.Library.IAlbum
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace MediaManager.Library
{
  public interface IAlbum : IContentItem, INotifyPropertyChanged
  {
    string Title { get; set; }

    string Description { get; set; }

    string Tags { get; set; }

    string Genre { get; set; }

    object Image { get; set; }

    string ImageUri { get; set; }

    object Rating { get; set; }

    DateTime ReleaseDate { get; set; }

    TimeSpan Duration { get; set; }

    string LabelName { get; set; }

    IList<IArtist> Artists { get; set; }

    IList<IMediaItem> MediaItems { get; set; }
  }
}
