using System.Collections.Generic;
using UnityEngine;

public static class IListExtensions
{
    /// <summary>
    /// Shuffles the elements of a list
    /// </summary>
    public static void Shuffle<T>(this IList<T> list)
    {
        int count = list.Count;
        int last = count - 1;
      
        for (int i = 0; i < last; ++i)
        {
            int index = Random.Range(i, count);
            T tmp = list[i];
            list[i] = list[index];
            list[index] = tmp;
        }
    }
}