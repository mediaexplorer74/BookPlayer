// Decompiled with JetBrains decompiler
// Type: MediaManager.Media.IMediaLibrary
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using MediaManager.Library;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable disable
namespace MediaManager.Media
{
  public interface IMediaLibrary
  {
    bool ReturnOnFirstResult { get; set; }

    IList<ILibraryProvider> Providers { get; }

    IEnumerable<IPlaylistProvider> PlaylistProviders { get; }

    IEnumerable<IArtistProvider> ArtistProviders { get; }

    IEnumerable<IAlbumProvider> AlbumProviders { get; }

    IEnumerable<IRadioProvider> RadioProviders { get; }

    IEnumerable<IMediaItemProvider> MediaItemProviders { get; }

    Task<IEnumerable<TContentItem>> GetAll<TContentItem>() where TContentItem : IContentItem;

    Task<TContentItem> Get<TContentItem>(string id) where TContentItem : IContentItem;

    Task<bool> Exists<TContentItem>(string id) where TContentItem : IContentItem;

    Task<bool> AddOrUpdate<TContentItem>(TContentItem item) where TContentItem : IContentItem;

    Task<bool> Remove<TContentItem>(TContentItem item) where TContentItem : IContentItem;

    Task<bool> RemoveAll<TContentItem>() where TContentItem : IContentItem;
  }
}
