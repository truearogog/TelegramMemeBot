using System;
using System.Collections.Generic;

public static class Extensions
{
    public static T[] SubArray<T>(this T[] array, int offset, int length)
    {
        return new List<T>(array).GetRange(offset, length).ToArray();
    }

    public static void PrintArray<T>(this T[] array)
    {
        Array.ForEach(array, x => Console.WriteLine(x.ToString()));
    }
}