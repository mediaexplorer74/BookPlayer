// Decompiled with JetBrains decompiler
// Type: MediaManager.Media.MediaExtractorBase
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using MediaManager.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

#nullable disable
namespace MediaManager.Media
{
  public abstract class MediaExtractorBase : IMediaExtractor
  {
    private IList<IMediaExtractorProvider> _providers;

    protected Dictionary<string, string> RequestHeaders => CrossMediaManager.Current.RequestHeaders;

    public IList<string> RemotePrefixes { get; } = (IList<string>) new List<string>()
    {
      "http",
      "udp",
      "rtp"
    };

    public IList<string> FilePrefixes { get; } = (IList<string>) new List<string>()
    {
      "file",
      "/",
      "ms-appx",
      "ms-appdata"
    };

    public IList<string> ResourcePrefixes { get; } = (IList<string>) new List<string>()
    {
      "android.resource",
      "raw",
      "assets"
    };

    public IList<string> VideoSuffixes { get; } = (IList<string>) new List<string>()
    {
      ".3gp",
      ".3g2",
      ".asf",
      ".wmv",
      ".avi",
      ".divx",
      ".evo",
      ".f4v",
      ".flv",
      ".mkv",
      ".mk3d",
      ".mp4",
      ".mpg",
      ".mpeg",
      ".m2p",
      ".ps",
      ".ts",
      ".m2ts",
      ".mxf",
      ".ogg",
      ".mov",
      ".qt",
      ".rmvb",
      ".vob",
      ".webm"
    };

    public IList<string> AudioSuffixes { get; } = (IList<string>) new List<string>()
    {
      ".3gp",
      ".aa",
      ".aac",
      ".aax",
      ".act",
      ".aiff",
      ".amr",
      ".ape",
      ".au",
      ".awb",
      ".dct",
      ".dss",
      ".dvf",
      ".flac",
      ".gsm",
      ".iklax",
      ".ivs",
      ".m4a",
      ".m4b",
      ".m4p",
      ".mmf",
      ".mp3",
      ".mpc",
      ".msv",
      ".nmf",
      ".nsf",
      ".ogg",
      ".oga,",
      ".mogg",
      ".opus",
      ".ra",
      ".rm",
      ".raw",
      ".sln",
      ".tta",
      ".voc",
      ".vox",
      ".wav",
      ".wma",
      ".wv",
      ".webm",
      ".8svx"
    };

    public IList<string> ImageSuffixes { get; } = (IList<string>) new List<string>()
    {
      ".jpg",
      ".png",
      ".gif",
      ".webp",
      ".tiff",
      ".psd",
      ".raw",
      ".bmp",
      ".heif",
      ".indd",
      ".jpeg",
      ".svg",
      ".ai",
      ".eps",
      ".pdf"
    };

    public IList<string> HlsSuffixes { get; } = (IList<string>) new List<string>()
    {
      ".m3u8",
      "manifest(format=m3u8-aapl)",
      "manifest(format=m3u8-aapl-v3)",
      "manifest(format=m3u8-aapl-v3,audio-only=false)"
    };

    public IList<string> SmoothStreamingSuffixes { get; } = (IList<string>) new List<string>()
    {
      ".ism",
      ".ism/manifest"
    };

    public IList<string> DashSuffixes { get; } = (IList<string>) new List<string>()
    {
      ".mpd",
      "manifest(format=mpd-time-csf)"
    };

    public IList<IMediaExtractorProvider> Providers
    {
      get => this._providers == null ? (this.Providers = this.CreateProviders()) : this._providers;
      internal set => this._providers = value;
    }

    public IEnumerable<IMediaItemMetadataProvider> MetadataProviders
    {
      get => this.Providers.OfType<IMediaItemMetadataProvider>();
    }

    public IEnumerable<IMediaItemImageProvider> ImageProviders
    {
      get => this.Providers.OfType<IMediaItemImageProvider>();
    }

    public IEnumerable<IMediaItemVideoFrameProvider> VideoFrameProviders
    {
      get => this.Providers.OfType<IMediaItemVideoFrameProvider>();
    }

    public virtual IList<IMediaExtractorProvider> CreateProviders()
    {
      return (IList<IMediaExtractorProvider>) new List<IMediaExtractorProvider>();
    }

    public virtual async Task<IMediaItem> CreateMediaItem(string url)
    {
      return await this.UpdateMediaItem((IMediaItem) new MediaItem(url)).ConfigureAwait(false);
    }

    public virtual async Task<IMediaItem> CreateMediaItem(FileInfo file)
    {
      return await this.CreateMediaItem(file.FullName).ConfigureAwait(false);
    }

    public virtual async Task<IMediaItem> CreateMediaItemFromAssembly(
      string resourceName,
      Assembly assembly = null)
    {
      if (assembly == (Assembly) null && !this.TryFindAssembly(resourceName, out assembly))
        return (IMediaItem) null;
      string uri = (string) null;
      string[] array = ((IEnumerable<string>) assembly.GetManifestResourceNames()).Where<string>((Func<string, bool>) (x => x.EndsWith(resourceName, StringComparison.OrdinalIgnoreCase))).ToArray<string>();
      if (array.Length < 1)
        return (IMediaItem) null;
      using (Stream stream = assembly.GetManifestResourceStream(((IEnumerable<string>) array).Single<string>()))
        uri = await this.CopyResourceStreamToFile(stream, "EmbeddedResources", resourceName).ConfigureAwait(false);
      return await this.UpdateMediaItem((IMediaItem) new MediaItem(uri)
      {
        MediaLocation = MediaLocation.Embedded
      }).ConfigureAwait(false);
    }

    public virtual async Task<IMediaItem> CreateMediaItemFromResource(string resourceName)
    {
      return await this.UpdateMediaItem((IMediaItem) new MediaItem(await this.GetResourcePath(resourceName).ConfigureAwait(false))
      {
        MediaLocation = MediaLocation.Resource
      }).ConfigureAwait(false);
    }

    public virtual async Task<IMediaItem> UpdateMediaItem(IMediaItem mediaItem)
    {
      if (mediaItem == null)
        throw new ArgumentNullException(nameof (mediaItem));
      if (string.IsNullOrEmpty(mediaItem.FileName))
        mediaItem.FileName = this.GetFileName(mediaItem.MediaUri);
      if (string.IsNullOrEmpty(mediaItem.FileExtension))
        mediaItem.FileExtension = this.GetFileExtension(mediaItem.FileName);
      if (mediaItem.MediaLocation == MediaLocation.Unknown)
        mediaItem.MediaLocation = this.GetMediaLocation(mediaItem.MediaUri);
      if (mediaItem.MediaType == MediaType.Default)
        mediaItem.MediaType = this.GetMediaType(mediaItem.FileExtension);
      if (mediaItem.DownloadStatus == DownloadStatus.Unknown)
        mediaItem.DownloadStatus = this.GetDownloadStatus(mediaItem);
      if (!mediaItem.IsMetadataExtracted)
      {
        mediaItem = await this.GetMetadata(mediaItem).ConfigureAwait(false);
        IMediaItem mediaItem1 = mediaItem;
        mediaItem1.Image = await this.GetMediaImage(mediaItem).ConfigureAwait(false);
        mediaItem1 = (IMediaItem) null;
        mediaItem.IsMetadataExtracted = true;
      }
      return mediaItem;
    }

    public async Task<IMediaItem> GetMetadata(IMediaItem mediaItem)
    {
      foreach (IMediaItemMetadataProvider metadataProvider in this.MetadataProviders.Where<IMediaItemMetadataProvider>((Func<IMediaItemMetadataProvider, bool>) (x => x.Enabled)))
      {
        IMediaItem mediaItem1 = await metadataProvider.ProvideMetadata(mediaItem).ConfigureAwait(false);
        if (mediaItem1 != null)
          mediaItem = mediaItem1;
      }
      return mediaItem;
    }

    public async Task<object> GetMediaImage(IMediaItem mediaItem)
    {
      if (mediaItem == null)
        throw new ArgumentNullException(nameof (mediaItem));
      object mediaImage = (object) null;
      if (mediaItem.IsMetadataExtracted)
        mediaImage = mediaItem.DisplayImage;
      if (mediaImage == null)
      {
        foreach (IMediaItemImageProvider itemImageProvider in this.ImageProviders.Where<IMediaItemImageProvider>((Func<IMediaItemImageProvider, bool>) (x => x.Enabled)))
        {
          mediaImage = await itemImageProvider.ProvideImage(mediaItem).ConfigureAwait(false);
          if (mediaImage != null)
            return mediaImage;
        }
      }
      return mediaImage;
    }

    public async Task<object> GetVideoFrame(IMediaItem mediaItem, TimeSpan timeFromStart)
    {
      object videoFrame = (object) null;
      foreach (IMediaItemVideoFrameProvider videoFrameProvider in this.VideoFrameProviders.Where<IMediaItemVideoFrameProvider>((Func<IMediaItemVideoFrameProvider, bool>) (x => x.Enabled)))
      {
        videoFrame = await videoFrameProvider.ProvideVideoFrame(mediaItem, timeFromStart).ConfigureAwait(false);
        if (videoFrame != null)
          return videoFrame;
      }
      return videoFrame;
    }

    protected abstract Task<string> GetResourcePath(string resourceName);

    public virtual string GetFileName(string url)
    {
      if (string.IsNullOrEmpty(url))
        return string.Empty;
      Uri result;
      if (!Uri.TryCreate(url, UriKind.Absolute, out result))
        result = new Uri(url);
      string fileName = Path.GetFileName(result.LocalPath);
      if (string.IsNullOrEmpty(fileName))
        fileName = ((IEnumerable<string>) result.Segments).LastOrDefault<string>();
      return fileName;
    }

    public virtual string GetFileExtension(string fileName)
    {
      if (string.IsNullOrEmpty(fileName))
        return string.Empty;
      fileName = fileName.ToLowerInvariant();
      IEnumerable<string> strings = this.VideoSuffixes.Union<string>((IEnumerable<string>) this.AudioSuffixes).Union<string>((IEnumerable<string>) this.HlsSuffixes).Union<string>((IEnumerable<string>) this.DashSuffixes).Union<string>((IEnumerable<string>) this.SmoothStreamingSuffixes);
      foreach (string fileExtension in strings)
      {
        if (fileName.EndsWith(fileExtension))
          return fileExtension;
      }
      foreach (string fileExtension in strings)
      {
        if (fileName.Contains(fileExtension))
          return fileExtension;
      }
      return fileName;
    }

    public virtual MediaType GetMediaType(string fileExtension)
    {
      if (string.IsNullOrEmpty(fileExtension))
        return MediaType.Default;
      fileExtension = fileExtension.ToLowerInvariant();
      if (this.VideoSuffixes.Contains(fileExtension))
        return MediaType.Video;
      if (this.AudioSuffixes.Contains(fileExtension))
        return MediaType.Audio;
      if (this.HlsSuffixes.Contains(fileExtension))
        return MediaType.Hls;
      if (this.DashSuffixes.Contains(fileExtension))
        return MediaType.Dash;
      if (this.SmoothStreamingSuffixes.Contains(fileExtension))
        return MediaType.SmoothStreaming;
      return this.ImageSuffixes.Contains(fileExtension) ? MediaType.Image : MediaType.Default;
    }

    public virtual MediaLocation GetMediaLocation(string url)
    {
      if (string.IsNullOrEmpty(url))
        return MediaLocation.Unknown;
      url = url.ToLowerInvariant();
      foreach (string remotePrefix in (IEnumerable<string>) this.RemotePrefixes)
      {
        if (url.StartsWith(remotePrefix))
          return MediaLocation.Remote;
      }
      foreach (string resourcePrefix in (IEnumerable<string>) this.ResourcePrefixes)
      {
        if (url.StartsWith(resourcePrefix))
          return MediaLocation.Resource;
      }
      foreach (string filePrefix in (IEnumerable<string>) this.FilePrefixes)
      {
        if (url.StartsWith(filePrefix))
          return MediaLocation.FileSystem;
      }
      return url.Length > 1 && url[1] == ':' ? MediaLocation.FileSystem : MediaLocation.Unknown;
    }

    public virtual DownloadStatus GetDownloadStatus(IMediaItem mediaItem)
    {
      if (mediaItem == null)
        throw new ArgumentNullException(nameof (mediaItem));
      switch (mediaItem.MediaLocation)
      {
        case MediaLocation.Unknown:
          return DownloadStatus.Unknown;
        case MediaLocation.Remote:
          return DownloadStatus.NotDownloaded;
        default:
          return DownloadStatus.Downloaded;
      }
    }

    protected virtual async Task<string> CopyResourceStreamToFile(
      Stream stream,
      string tempDirectoryName,
      string resourceName)
    {
      string path = (string) null;
      if (stream != null)
      {
        string str = Path.Combine(Path.GetTempPath(), tempDirectoryName);
        path = Path.Combine(str, resourceName);
        if (!Directory.Exists(str))
          Directory.CreateDirectory(str);
        using (FileStream tempFile = File.Create(path))
          await stream.CopyToAsync((Stream) tempFile).ConfigureAwait(false);
      }
      string file = path;
      path = (string) null;
      return file;
    }

    protected virtual bool TryFindAssembly(string resourceName, out Assembly assembly)
    {
      foreach (Assembly assembly1 in AppDomain.CurrentDomain.GetAssemblies())
      {
        if (((IEnumerable<string>) assembly1.GetManifestResourceNames()).Any<string>((Func<string, bool>) (x => x.EndsWith(resourceName, StringComparison.OrdinalIgnoreCase))))
        {
          assembly = assembly1;
          return true;
        }
      }
      assembly = (Assembly) null;
      return false;
    }
  }
}
