// Decompiled with JetBrains decompiler
// Type: MediaManager.Forms.Xaml.PlayExtension
// Assembly: MediaManager.Forms, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 775A528A-0AB9-4EB3-B8E7-9E6E2449F6CA
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.Forms.dll

using MediaManager.Player;

#nullable disable
namespace MediaManager.Forms.Xaml
{
  public class PlayExtension : MediaExtensionBase
  {
    protected override bool CanExecute()
    {
      return this.MediaManager.State == MediaPlayerState.Paused || this.MediaManager.State == MediaPlayerState.Stopped;
    }

    protected override void Execute() => this.MediaManager.Play();
  }
}
