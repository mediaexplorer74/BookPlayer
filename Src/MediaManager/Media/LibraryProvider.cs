// Decompiled with JetBrains decompiler
// Type: MediaManager.Media.LibraryProvider`1
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using MediaManager.Library;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable disable
namespace MediaManager.Media
{
  public abstract class LibraryProvider<TContentItem> : 
    ILibraryProvider<TContentItem>,
    ILibraryProvider
    where TContentItem : IContentItem
  {
    public bool Enabled { get; set; } = true;

    public abstract Task<bool> AddOrUpdate(TContentItem item);

    public abstract Task<bool> Exists(string id);

    public abstract Task<TContentItem> Get(string id);

    public abstract Task<IEnumerable<TContentItem>> GetAll();

    public abstract Task<bool> RemoveAll();

    public abstract Task<bool> Remove(TContentItem item);
  }
}
