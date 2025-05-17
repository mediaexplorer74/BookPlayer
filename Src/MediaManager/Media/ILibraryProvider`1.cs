// Decompiled with JetBrains decompiler
// Type: MediaManager.Media.ILibraryProvider`1
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using MediaManager.Library;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable disable
namespace MediaManager.Media
{
  public interface ILibraryProvider<TContentItem> : ILibraryProvider where TContentItem : IContentItem
  {
    Task<IEnumerable<TContentItem>> GetAll();

    Task<TContentItem> Get(string id);

    Task<bool> Exists(string id);

    Task<bool> AddOrUpdate(TContentItem item);

    Task<bool> Remove(TContentItem item);

    Task<bool> RemoveAll();
  }
}
