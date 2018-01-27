using System;
using System.Collections.Generic;

public static class ListExtensions
{
   #region Generic array extensions

   public static IList<T> Swap<T> (this IList<T> list, int indexA, int indexB)
   {
      T tmp = list [indexA];
      list [indexA] = list [indexB];
      list [indexB] = tmp;
      return list;
   }

   public static IList<T> Shuffle<T> (this IList<T> list)
   {
      for (int i = 0, n = list.Count - 1; i < n; i++)
      {
         Swap (list, i, UnityEngine.Random.Range (i, list.Count));
      }
      return list;
   }


   public static IList<T> Reverse<T> (this IList<T> list)
   {
      int nLast = list.Count - 1;

      for (int i = 0, n = UnityEngine.Mathf.FloorToInt (list.Count / 2f); i < n; i++)
      {
         Swap (list, i, nLast - i);
      }
      return list;
   }

   #endregion

   public static int LowerNearestIndex<K, V> (this SortedList<K, V> list, K key) where K : IComparable<K>
   {
      // Check to see if we need to search the list.
      if (list == null || list.Count <= 0) { return -1; }
      if (list.Count == 1) { return key.CompareTo (list.Keys [0]) >= 0 ? 0 : -1; }

      // Setup the variables needed to find the closest index
      int lower = 0;
      int upper = list.Count - 1;
      int index = (lower + upper) / 2;

      // Find the closest index (rounded down)
      while ((lower <= upper))
      {
         int comparisonResult = key.CompareTo (list.Keys [index]);
         if (comparisonResult == 0)
         { return index; }

         if (comparisonResult < 0)
         {
            upper = index - 1;
         }
         else
         {
            lower = index + 1;
         } //endif
         index = (lower + upper) / 2;
      } //endwhile

      // Check to see if we are under or over the max values.
      if (index >= list.Count - 1) { return list.Count - 1; }
      if (index < 0) { return -1; }

      // Return the correct/closest index
      return index;
   }
}
