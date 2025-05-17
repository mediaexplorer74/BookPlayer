// Decompiled with JetBrains decompiler
// Type: MediaManager.Media.MediaExtractorExtensions
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using MediaManager.Library;
using MediaManager.Queue;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace MediaManager.Media
{
  public static class MediaExtractorExtensions
  {
    private static IMediaExtractor MediaExtractor => CrossMediaManager.Current.Extractor;

    public static bool IsRemote(this MediaLocation mediaLocation)
    {
      return mediaLocation == MediaLocation.Remote;
    }

    public static bool IsLocal(this MediaLocation mediaLocation)
    {
      return mediaLocation == MediaLocation.FileSystem || mediaLocation == MediaLocation.Embedded || mediaLocation == MediaLocation.Resource;
    }

    public static async Task<IEnumerable<IMediaItem>> CreateMediaItems(
      this IEnumerable<string> items)
    {
      return (IEnumerable<IMediaItem>) await Task.WhenAll<IMediaItem>(items.Select<string, Task<IMediaItem>>((Func<string, Task<IMediaItem>>) (i => MediaExtractorExtensions.MediaExtractor.CreateMediaItem(i)))).ConfigureAwait(false);
    }

    public static async Task<IEnumerable<IMediaItem>> CreateMediaItems(
      this IEnumerable<FileInfo> items)
    {
      return (IEnumerable<IMediaItem>) await Task.WhenAll<IMediaItem>(items.Select<FileInfo, Task<IMediaItem>>((Func<FileInfo, Task<IMediaItem>>) (i => MediaExtractorExtensions.MediaExtractor.CreateMediaItem(i)))).ConfigureAwait(false);
    }

    public static async Task<IMediaItem> UpdateMediaItem(this IMediaItem mediaItem)
    {
      return mediaItem.IsMetadataExtracted ? mediaItem : await MediaExtractorExtensions.MediaExtractor.UpdateMediaItem(mediaItem).ConfigureAwait(false);
    }

    public static async Task<IEnumerable<IMediaItem>> UpdateMediaItems(
      this IEnumerable<IMediaItem> items)
    {
      return (IEnumerable<IMediaItem>) await Task.WhenAll<IMediaItem>(items.Select<IMediaItem, Task<IMediaItem>>((Func<IMediaItem, Task<IMediaItem>>) (i => i.UpdateMediaItem()))).ConfigureAwait(false);
    }

    public static async Task<IEnumerable<IMediaItem>> UpdateMediaItems(this IMediaQueue mediaQueue)
    {
      return (IEnumerable<IMediaItem>) await Task.WhenAll<IMediaItem>(mediaQueue.Select<IMediaItem, Task<IMediaItem>>((Func<IMediaItem, Task<IMediaItem>>) (i => i.UpdateMediaItem()))).ConfigureAwait(false);
    }
  }
}
