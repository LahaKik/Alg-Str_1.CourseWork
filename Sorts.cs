using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace CourseWork
{
    static class Sorts<T, U> where T : ISortObject<U> where U : IComparable
    {
        private static void Swap(ref T mass, int firstIndex, int secondIndex)
        {
            if (firstIndex > mass.Count - 1 || secondIndex > mass.Count - 1 || firstIndex == secondIndex)
                return;
            var temp = mass[firstIndex];
            mass.SwitchData(firstIndex, mass[secondIndex]!);
            mass.SwitchData(secondIndex, temp!);
        }

        public static void InsertionSort(ref T mass, int start = 0, int finish = int.MaxValue)
        {
            if (finish == int.MaxValue)
                finish = mass.Count;
            if (mass.Count < 2 || start > finish || start > mass.Count || finish > mass.Count)
                return;

            for (int i = start; i <= finish; i++)
            {
                for (int j = i; j > start && mass[j - 1]!.CompareTo(mass[j]) > 0; j--)
                {
                    Swap(ref mass, j - 1, j);
                }
            }
        }

        public static void ShellSort(ref T mass)
        {
            for (int d = mass.Count; d != 0; d /= 2)
            {
                for (int i = d; i < mass.Count; i++)
                {
                    for (int j = i - d; j >= 0 && mass[j]!.CompareTo(mass[j + d]) > 0; j -= d)
                    {
                        Swap(ref mass, j, j + d);
                    }
                }
            }
        }
    }
    static class Sorts
    { 
        public static (int, int, int)[] Sort((int, int, int)[] mass)
        {
            for (int i = 0; i < mass.Length; i++)
            {
                for (int j = i; j > 0 && mass[j - 1]!.Item3.CompareTo(mass[j].Item3) > 0; j--)
                {
                    (mass[j], mass[j - 1]) = (mass[j - 1], mass[j]);
                }
            }
            return mass;
        }
    }

    public interface ISortObject<T> where T : IComparable
    {
        public int Count { get; }
        public void AddAt(int index, T data);
        public void AddFirst(T data);
        public void AddLast(T data);
        public void Clear();
        public bool RemoveAt(int index);
        public bool RemoveFirst();
        public bool RemoveLast();
        public T? this[int index] { get; }
        public bool SwitchData(int index, T data);
    }
}
