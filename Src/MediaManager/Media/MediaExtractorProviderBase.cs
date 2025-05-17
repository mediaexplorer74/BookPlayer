// Decompiled with JetBrains decompiler
// Type: MediaManager.Media.MediaExtractorProviderBase
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

#nullable disable
namespace MediaManager.Media
{
  public class MediaExtractorProviderBase : NotifyPropertyChangedBase, IMediaExtractorProvider
  {
    private bool _enabled = true;

    public bool Enabled
    {
      get => this._enabled;
      set => this.SetProperty<bool>(ref this._enabled, value, nameof (Enabled));
    }
  }
}
