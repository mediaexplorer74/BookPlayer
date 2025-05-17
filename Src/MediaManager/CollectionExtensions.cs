// Decompiled with JetBrains decompiler
// Type: MediaManager.CollectionExtensions
// Assembly: MediaManager, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CB8478B-6376-4E7F-A176-D28B9466D5DE
// Assembly location: C:\Users\Admin\Desktop\RE\MediaManager\MediaManager.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace MediaManager
{
  public static class CollectionExtensions
  {
    public static int[] GetShuffleExchanges(int size, int key)
    {
      int[] shuffleExchanges = new int[size - 1];
      Random random = new Random(key);
      for (int index = size - 1; index > 0; --index)
      {
        int num = random.Next(index + 1);
        shuffleExchanges[size - 1 - index] = num;
      }
      return shuffleExchanges;
    }

    public static void Shuffle<T>(this IList<T> list, int key)
    {
      int count = list.Count;
      int[] shuffleExchanges = CollectionExtensions.GetShuffleExchanges(count, key);
      for (int index1 = count - 1; index1 > 0; --index1)
      {
        int index2 = shuffleExchanges[count - 1 - index1];
        T obj = list[index1];
        list[index1] = list[index2];
        list[index2] = obj;
      }
    }

    public static void DeShuffle<T>(this IList<T> list, int key)
    {
      int count = list.Count;
      int[] shuffleExchanges = CollectionExtensions.GetShuffleExchanges(count, key);
      for (int index1 = 1; index1 < count; ++index1)
      {
        int index2 = shuffleExchanges[count - index1 - 1];
        T obj = list[index1];
        list[index1] = list[index2];
        list[index2] = obj;
      }
    }
  }
}
