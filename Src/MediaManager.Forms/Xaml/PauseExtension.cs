// Decompiled with JetBrains decompiler
// Type: MediaManager.Forms.Xaml.PauseExtension
// Assembly: MediaManager.Forms, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 775A528A-0AB9-4EB3-B8E7-9E6E2449F6CA
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.Forms.dll

using MediaManager.Player;

#nullable disable
namespace MediaManager.Forms.Xaml
{
  public class PauseExtension : MediaExtensionBase
  {
    protected override bool CanExecute() => this.MediaManager.State == MediaPlayerState.Playing;

    protected override void Execute() => this.MediaManager.Pause();
  }
}
