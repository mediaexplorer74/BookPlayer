// Decompiled with JetBrains decompiler
// Type: MediaManager.Forms.Xaml.MediaExtensionBase
// Assembly: MediaManager.Forms, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 775A528A-0AB9-4EB3-B8E7-9E6E2449F6CA
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.Forms.dll

using MediaManager.Playback;
using MediaManager.Player;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

#nullable disable
namespace MediaManager.Forms.Xaml
{
  public abstract class MediaExtensionBase : IMarkupExtension<ICommand>, IMarkupExtension
  {
    protected IMediaManager MediaManager { get; }

    private Command _command { get; }

    protected MediaExtensionBase()
    {
      this.MediaManager = CrossMediaManager.Current;
      this._command = new Command(new Action(this.Execute), new Func<bool>(this.CanExecute));
      this.MediaManager.StateChanged += (StateChangedEventHandler) ((s, e) => this.RaiseCanExecuteChanged());
    }

    protected virtual bool CanExecute() => !this.IsLoadingOrFaulted();

    protected abstract void Execute();

    public ICommand ProvideValue(IServiceProvider serviceProvider) => (ICommand) this._command;

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
      return (object) this.ProvideValue(serviceProvider);
    }

    protected bool IsLoadingOrFaulted()
    {
      switch (this.MediaManager.State)
      {
        case MediaPlayerState.Loading:
        case MediaPlayerState.Buffering:
        case MediaPlayerState.Failed:
          return true;
        default:
          return false;
      }
    }

    protected void RaiseCanExecuteChanged() => this._command.ChangeCanExecute();
  }
}
