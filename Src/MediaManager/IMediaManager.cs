// Decompiled with JetBrains decompiler
// Type: MediaManager.IMediaManager
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using MediaManager.Library;
using MediaManager.Media;
using MediaManager.Notifications;
using MediaManager.Playback;
using MediaManager.Player;
using MediaManager.Queue;
using MediaManager.Volume;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

#nullable disable
namespace MediaManager
{
    //public interface IMediaManager<TPlayer> : IMediaManager where TPlayer : class
    //{
    //    TPlayer Player { get; }
    //}


    public interface IMediaManager : IPlaybackManager, IDisposable
  {
    IMediaPlayer MediaPlayer { get; set; }

    IMediaLibrary Library { get; set; }

    Dictionary<string, string> RequestHeaders { get; set; }

    INotificationManager Notification { get; set; }

    IVolumeManager Volume { get; set; }

    IMediaExtractor Extractor { get; set; }

    IMediaQueue Queue { get; set; }

    void Init();

    Task<IMediaItem> Play(IMediaItem mediaItem);

    Task<IMediaItem> Play(string uri);

    Task<IMediaItem> PlayFromAssembly(string resourceName, Assembly assembly = null);

    Task<IMediaItem> PlayFromResource(string resourceName);

    Task<IMediaItem> Play(IEnumerable<IMediaItem> mediaItems);

    Task<IMediaItem> Play(IEnumerable<string> items);

    Task<IMediaItem> Play(FileInfo file);

    Task<IMediaItem> Play(DirectoryInfo directoryInfo);

    Task<IMediaItem> Play(Stream stream, string cacheName);

    /// <summary>
    /// Plays media from a Stream.
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="mimeType"></param>
    /// <returns></returns>
    //Task<IMediaItem> Play(Stream stream, MimeType mimeType);
  }
}
