// Decompiled with JetBrains decompiler
// Type: MediaManager.Library.MediaItemExtensions
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace MediaManager.Library
{
  public static class MediaItemExtensions
  {
    public static IDictionary<string, object> ToDictionary(this IMediaItem mediaItem)
    {
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      foreach (PropertyInfo property in mediaItem.GetType().GetProperties())
      {
        object obj = property.GetValue((object) mediaItem, (object[]) null);
        if (obj != null)
          dictionary.Add(property.Name, obj);
      }
      return (IDictionary<string, object>) dictionary;
    }

    public static string GetTitle(this IMediaItem mediaItem)
    {
      if (!string.IsNullOrEmpty(mediaItem.DisplayTitle))
        return mediaItem.DisplayTitle;
      return !string.IsNullOrEmpty(mediaItem.Title) ? mediaItem.Title : string.Empty;
    }

    public static string GetContentTitle(this IMediaItem mediaItem)
    {
      if (!string.IsNullOrEmpty(mediaItem.DisplaySubtitle))
        return mediaItem.DisplaySubtitle;
      if (!string.IsNullOrEmpty(mediaItem.Artist))
        return mediaItem.Artist;
      if (!string.IsNullOrEmpty(mediaItem.AlbumArtist))
        return mediaItem.AlbumArtist;
      return !string.IsNullOrEmpty(mediaItem.Album) ? mediaItem.Album : string.Empty;
    }

    public static string GetSubText(this IMediaItem mediaItem)
    {
      if (!string.IsNullOrEmpty(mediaItem.Album))
        return mediaItem.Album;
      if (!string.IsNullOrEmpty(mediaItem.Artist))
        return mediaItem.Artist;
      return !string.IsNullOrEmpty(mediaItem.AlbumArtist) ? mediaItem.AlbumArtist : string.Empty;
    }
  }
}
