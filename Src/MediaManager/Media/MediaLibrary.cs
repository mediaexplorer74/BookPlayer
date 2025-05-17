// Decompiled with JetBrains decompiler
// Type: MediaManager.Media.MediaLibrary
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using MediaManager.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace MediaManager.Media
{
  public class MediaLibrary : IMediaLibrary
  {
    private IList<ILibraryProvider> _providers;

    public bool ReturnOnFirstResult { get; set; }

    public IList<ILibraryProvider> Providers
    {
      get => this._providers == null ? (this.Providers = this.CreateProviders()) : this._providers;
      internal set => this._providers = value;
    }

    public virtual IList<ILibraryProvider> CreateProviders()
    {
      return (IList<ILibraryProvider>) new List<ILibraryProvider>();
    }

    public IEnumerable<IPlaylistProvider> PlaylistProviders
    {
      get => this.Providers.OfType<IPlaylistProvider>();
    }

    public IEnumerable<IArtistProvider> ArtistProviders => this.Providers.OfType<IArtistProvider>();

    public IEnumerable<IAlbumProvider> AlbumProviders => this.Providers.OfType<IAlbumProvider>();

    public IEnumerable<IRadioProvider> RadioProviders => this.Providers.OfType<IRadioProvider>();

    public IEnumerable<IMediaItemProvider> MediaItemProviders
    {
      get => this.Providers.OfType<IMediaItemProvider>();
    }

    public async Task<IEnumerable<TContentItem>> GetAll<TContentItem>() where TContentItem : IContentItem
    {
      List<TContentItem> items = new List<TContentItem>();
      IList<Task<IEnumerable<TContentItem>>> taskList = (IList<Task<IEnumerable<TContentItem>>>) new List<Task<IEnumerable<TContentItem>>>();
      foreach (ILibraryProvider<TContentItem> libraryProvider in this.Providers.Where<ILibraryProvider>((Func<ILibraryProvider, bool>) (x => x.Enabled)).OfType<ILibraryProvider<TContentItem>>())
        taskList.Add(libraryProvider.GetAll());
      foreach (IEnumerable<TContentItem> collection in await Task.WhenAll<IEnumerable<TContentItem>>((IEnumerable<Task<IEnumerable<TContentItem>>>) taskList).ConfigureAwait(false))
        items.AddRange(collection);
      IEnumerable<TContentItem> all = (IEnumerable<TContentItem>) items;
      items = (List<TContentItem>) null;
      return all;
    }

    public async Task<TContentItem> Get<TContentItem>(string id) where TContentItem : IContentItem
    {
      foreach (ILibraryProvider<TContentItem> libraryProvider in this.Providers.Where<ILibraryProvider>((Func<ILibraryProvider, bool>) (x => x.Enabled)).OfType<ILibraryProvider<TContentItem>>())
      {
        TContentItem contentItem = await libraryProvider.Get(id).ConfigureAwait(false);
        if ((object) contentItem != null)
          return contentItem;
      }
      return default (TContentItem);
    }

    public async Task<bool> AddOrUpdate<TContentItem>(TContentItem item) where TContentItem : IContentItem
    {
      foreach (ILibraryProvider<TContentItem> libraryProvider in this.Providers.Where<ILibraryProvider>((Func<ILibraryProvider, bool>) (x => x.Enabled)).OfType<ILibraryProvider<TContentItem>>())
      {
        bool flag = await libraryProvider.AddOrUpdate(item).ConfigureAwait(false);
        if (flag)
          return flag;
      }
      return false;
    }

    public async Task<bool> Remove<TContentItem>(TContentItem item) where TContentItem : IContentItem
    {
      foreach (ILibraryProvider<TContentItem> libraryProvider in this.Providers.Where<ILibraryProvider>((Func<ILibraryProvider, bool>) (x => x.Enabled)).OfType<ILibraryProvider<TContentItem>>())
      {
        bool flag = await libraryProvider.Remove(item).ConfigureAwait(false);
        if (flag)
          return flag;
      }
      return false;
    }

    public async Task<bool> RemoveAll<TContentItem>() where TContentItem : IContentItem
    {
      IList<Task<bool>> taskList = (IList<Task<bool>>) new List<Task<bool>>();
      foreach (ILibraryProvider<TContentItem> libraryProvider in this.Providers.Where<ILibraryProvider>((Func<ILibraryProvider, bool>) (x => x.Enabled)).OfType<ILibraryProvider<TContentItem>>())
        taskList.Add(libraryProvider.RemoveAll());
      return ((IEnumerable<bool>) await Task.WhenAll<bool>((IEnumerable<Task<bool>>) taskList).ConfigureAwait(false)).All<bool>((Func<bool, bool>) (x => x));
    }

    public async Task<bool> Exists<TContentItem>(string id) where TContentItem : IContentItem
    {
      foreach (ILibraryProvider<TContentItem> libraryProvider in this.Providers.Where<ILibraryProvider>((Func<ILibraryProvider, bool>) (x => x.Enabled)).OfType<ILibraryProvider<TContentItem>>())
      {
        bool flag = await libraryProvider.Exists(id).ConfigureAwait(false);
        if (flag)
          return flag;
      }
      return false;
    }
  }
}
