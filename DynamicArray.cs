using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourseWork
{
    public class DynamicArray<T> : ISortObject<T>, IComparable where T : IComparable
    {
        protected T[] data;
        protected int count;
        private int tArray;
        public int Count
        {
            get { return count; }
            protected set
            {
                count = value;
                if (count < 0)
                    count = 0;
            }
        }

        public DynamicArray()
        {
            tArray = 3;
            data = new T[tArray];
            Count = 0;
        }

        public DynamicArray(int Count)
        {
            tArray = Count;
            data = new T[tArray];
            Count = 0;
        }

        public DynamicArray(T[] arr)
        {
            tArray = arr.Length + 2;
            data = new T[tArray];
            for (int i = 0; i < arr.Length; i++)
            {
                data[i] = arr[i];
            }
            Count = arr.Length;
        }

        public DynamicArray(DynamicArray<T> right)
        {
            tArray = right.Count;
            data = new T[tArray];
            for (int i = 0; i < right.Count; i++)
            {
                data[i] = right[i];
            }
            Count = right.Count;
        }

        public T? this[int index]
        {
            get
            {
                if (index >= 0 && index < Count)
                    return data[index];
                else if (index < 0 && index >= -Count)
                    return data[Count - index];
                return default;
            }
            protected set { data[index] = value; }
        }

        private void AddFirstest(T value)
        {
            data[0] = value;
            Count++;
        }

        public void AddAt(int index, T value)
        {
            if (Count == 0)
            {
                AddFirstest(value);
                return;
            }

            if (Count + 1 == tArray)
                RashirenieTerritorii(5);
            for (int i = Count - 1; i > index; i--)
            {
                (data[i + 1], data[i]) = (data[i], data[i + 1]);
            }
            T temp = data[index];
            data[index] = value;
            data[index + 1] = temp;

            Count++;
        }

        public void AddLast(T value)
        {
            AddAt(Count, value);
        }

        public void AddFirst(T value)
        {
            AddAt(0, value);
        }

        public bool RemoveAt(int index)
        {
            if (Count <= 0)
                return false;

            if (index >= Count)
                return false;

            for (int i = index; i < Count; i++)
            {
                data[i] = data[i + 1];
            }
            Count--;
            return true;
        }

        public bool RemoveFirst()
        {
            return RemoveAt(0);
        }

        public bool RemoveLast()
        {
            return RemoveAt(Count - 1);
        }

        private void RashirenieTerritorii(int valueOfExtention)
        {
            tArray += valueOfExtention;
            T[] dataNew = new T[tArray];
            for (int i = 0; i < Count; i++)
            {
                dataNew[i] = data[i];
            }
            data = dataNew;
        }

        public virtual void Clear()
        {
            tArray = 3;
            data = new T[tArray];
            Count = 0;
        }

        public override string? ToString()
        {
            string? rez = "";
            for (int i = 0; i < Count; i++)
            {
                rez += data[i].ToString() + " ";
            }
            return rez;
        }

        public bool SwitchData(int index, T data)
        {
            this.data[index] = data;
            return true;
        }

        public int CompareTo(object? obj)
        {
            if (obj is DynamicArray<T> other)
                return Count.CompareTo(other.Count);
            else throw new ArgumentException("Non-comparable object");
        }
    }
}
