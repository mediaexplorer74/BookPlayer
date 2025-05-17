// Decompiled with JetBrains decompiler
// Type: MediaManager.MediaManagerExtensions
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using MediaManager.Library;
using MediaManager.Playback;
using MediaManager.Player;
using MediaManager.Queue;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

#nullable disable
namespace MediaManager
{
  public static class MediaManagerExtensions
  {
    public static async Task<IMediaItem> Play(this IMediaManager mediaManager, object mediaSource)
    {
      IMediaItem mediaItem1 = (IMediaItem) null;
      switch (mediaSource)
      {
        case string uri:
          mediaItem1 = await mediaManager.Play(uri);
          break;
        case IEnumerable<string> items:
          mediaItem1 = await mediaManager.Play(items);
          break;
        case IMediaItem mediaItem2:
          mediaItem1 = await mediaManager.Play(mediaItem2);
          break;
        case IEnumerable<IMediaItem> mediaItems:
          mediaItem1 = await mediaManager.Play(mediaItems);
          break;
        case FileInfo file:
          mediaItem1 = await mediaManager.Play(file);
          break;
        case DirectoryInfo directoryInfo:
          mediaItem1 = await mediaManager.Play(directoryInfo);
          break;
        case IAlbum album:
          mediaItem1 = await mediaManager.Play((IEnumerable<IMediaItem>) album.MediaItems);
          break;
        case IRadio radio:
          mediaItem1 = await mediaManager.Play((IEnumerable<IMediaItem>) radio.MediaItems);
          break;
        case IPlaylist playlist:
          mediaItem1 = await mediaManager.Play((IEnumerable<IMediaItem>) playlist.MediaItems);
          break;
        case IArtist artist:
          mediaItem1 = await mediaManager.Play((IEnumerable<IMediaItem>) artist.AllTracks);
          break;
      }
      return mediaItem1;
    }

    public static async Task<IMediaItem> Play(
      this IMediaManager mediaManager,
      IMediaItem mediaItem,
      TimeSpan startAt,
      TimeSpan? stopAt = null)
    {
      if (mediaManager is MediaManagerBase mediaManagerBase)
        mediaItem = await mediaManagerBase.PrepareQueueForPlayback(mediaItem);
      await mediaManager.MediaPlayer.Play(mediaItem, startAt, stopAt);
      return mediaItem;
    }

    public static Task PlayPreviousOrSeekToStart(
      this IMediaManager mediaManager,
      TimeSpan? timeSpan = null)
    {
      ref TimeSpan? local = ref timeSpan;
      TimeSpan? nullable = timeSpan;
      TimeSpan timeSpan1 = nullable ?? TimeSpan.FromSeconds(3.0);
      local = new TimeSpan?(timeSpan1);
      TimeSpan position = mediaManager.Position;
      nullable = timeSpan;
      return (nullable.HasValue ? (position < nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? (Task) mediaManager.PlayPrevious() : mediaManager.SeekToStart();
    }

    public static bool IsPlaying(this IMediaManager mediaManager)
    {
      return mediaManager.State == MediaPlayerState.Playing || mediaManager.State == MediaPlayerState.Buffering;
    }

    public static bool IsPrepared(this IMediaManager mediaManager)
    {
      return mediaManager.State == MediaPlayerState.Playing || mediaManager.State == MediaPlayerState.Paused || mediaManager.State == MediaPlayerState.Buffering;
    }

    public static bool IsBuffering(this IMediaManager mediaManager)
    {
      return mediaManager.State == MediaPlayerState.Buffering;
    }

    public static bool IsStopped(this IMediaManager mediaManager)
    {
      return mediaManager.State == MediaPlayerState.Stopped || mediaManager.State == MediaPlayerState.Failed;
    }

    public static Task PlayPause(this IMediaManager mediaManager)
    {
      switch (mediaManager.State)
      {
        case MediaPlayerState.Stopped:
        case MediaPlayerState.Paused:
          return mediaManager.Play();
        default:
          return mediaManager.Pause();
      }
    }

    public static Task SeekToStart(this IMediaManager mediaManager)
    {
      return mediaManager.SeekTo(TimeSpan.Zero);
    }

    public static void ToggleRepeat(this IMediaManager mediaManager)
    {
      if (mediaManager.RepeatMode == RepeatMode.Off)
        mediaManager.RepeatMode = RepeatMode.All;
      else if (mediaManager.RepeatMode == RepeatMode.All)
        mediaManager.RepeatMode = RepeatMode.One;
      else
        mediaManager.RepeatMode = RepeatMode.Off;
    }

    public static void ToggleShuffle(this IMediaManager mediaManager)
    {
      if (mediaManager.ShuffleMode == ShuffleMode.Off)
        mediaManager.ShuffleMode = ShuffleMode.All;
      else
        mediaManager.ShuffleMode = ShuffleMode.Off;
    }
  }
}
