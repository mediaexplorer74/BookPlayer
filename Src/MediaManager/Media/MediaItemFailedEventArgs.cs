// Decompiled with JetBrains decompiler
// Type: MediaManager.Media.MediaItemFailedEventArgs
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using MediaManager.Library;
using System;

#nullable disable
namespace MediaManager.Media
{
  public class MediaItemFailedEventArgs : MediaItemEventArgs
  {
    public MediaItemFailedEventArgs(IMediaItem mediaItem, Exception exception, string message)
      : base(mediaItem)
    {
      this.Exeption = exception;
      this.Message = message;
    }

    public Exception Exeption { get; protected set; }

    public string Message { get; protected set; }
  }
}
