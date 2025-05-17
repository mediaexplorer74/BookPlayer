// Decompiled with JetBrains decompiler
// Type: MediaManager.CrossMediaManager
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using System;
using System.Diagnostics;
using System.Threading;

#nullable disable
namespace MediaManager
{
  [DebuggerStepThrough]
  public static class CrossMediaManager
  {
    private static Lazy<IMediaManager> implementation = new Lazy<IMediaManager>((Func<IMediaManager>) (() => CrossMediaManager.CreateMediaManager()), LazyThreadSafetyMode.PublicationOnly);

    public static bool IsSupported => CrossMediaManager.implementation.Value != null;

    public static IMediaManager Current
    {
      get
      {
        return CrossMediaManager.implementation.Value ?? throw CrossMediaManager.NotImplementedInReferenceAssembly();
      }
    }

    private static IMediaManager CreateMediaManager() => (IMediaManager) null;

    internal static Exception NotImplementedInReferenceAssembly()
    {
      return (Exception) new NotImplementedException("This functionality is not implemented in the portable version of this assembly. You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
    }
  }
}
