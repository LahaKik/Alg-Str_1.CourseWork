using System.Collections.Generic;
using System.Collections;

namespace CourseWork
{
    public class MList<T> : ISortObject<T> where T : IComparable
    {
        protected class Node<P>
        {
            public P Data { get; set; }
            public Node(P data)
            {
                Data = data;
            }
            public Node<P>? Prev { get; set; }
            public Node<P>? Next { get; set; }
        }

        private Node<T>? head;
        private Node<T>? tail;
        private int count;

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

        public MList()
        {
            Count = 0;
            head = null;
            tail = null;
        }
        public T? GetData(int index)
        {
            if (head == null)
                return default;

            bool OutOfRange = false;
            if (index < Count / 2)
            {
                Node<T>? curr = head;
                for (int i = 0; i < index; i++)
                {
                    if (curr == null)
                    {
                        OutOfRange = true;
                        break;
                    }
                    curr = curr.Next;
                }
                if (!OutOfRange)
                    return curr!.Data;
            }
            else
            {
                Node<T>? curr = tail;
                for (int i = 0; i < Count - 1 - index; i++)
                {
                    if (curr == null)
                    {
                        OutOfRange = true;
                        break;
                    }
                    curr = curr.Prev;
                }
                if (!OutOfRange)
                    return curr!.Data;
            }
            return default;
        }

        public T? GetDataFirst()
        {
            return GetData(0);
        }

        public void AddLast(T item)
        {
            Node<T> node = new Node<T>(item);
            if (head == null)
                head = node;
            else
            {
                tail!.Next = node;
                node.Prev = tail;
            }
            tail = node;
            Count++;
        }

        public void AddFirst(T item)
        {
            Node<T> node = new Node<T>(item);
            if (head == null)
            {
                head = node;
                tail = node;
            }
            else
            {
                head.Prev = node;
                node.Next = head;
                head = node;
            }
            Count++;
        }
        public bool RemoveLast()
        {
            if (Count == 0) return false;

            if (head != null && Count > 1)
            {
                tail = tail!.Prev;
                tail!.Next = null;
            }
            else
            {
                head = null;
                tail = null;
            }
            Count--;
            return true;
        }

        public bool RemoveAt(int index)
        {
            if (Count == 0) return false;
            if (Count == 1)
            {
                head = null;
                tail = null;
                Count = 0;
                return true;
            }

            Node<T>? curr = head;
            bool OutOfRange = false;
            for (int i = 0; i < index; i++)
            {
                if (curr == null)
                {
                    OutOfRange = true;
                    break;
                }
                curr = curr.Next;
            }
            if (!OutOfRange)
            {
                if (curr != head && curr != tail)
                {
                    curr!.Prev!.Next = curr.Next;
                    curr!.Next!.Prev = curr.Prev;
                }
                else if (curr == head)
                {
                    head = curr!.Next;
                    curr.Next!.Prev = null;
                }
                else
                {
                    tail = curr!.Prev;
                    curr.Prev!.Next = null;
                }
                Count--;
            }
            return true;
        }
        public bool RemoveFirst()
        {
            return RemoveAt(0);
        }

        public virtual void Clear()
        {
            Count = 0;
            head = null;
            tail = null;
        }

        public override string ToString()
        {
            string list = "";
            Node<T>? curr = head;
            int i = 0;
            while (curr != null)
            {
                list += "[" + i + "]" + " " + curr.Data!.ToString() + " ";
                i++;
                curr = curr.Next;
            }
            return list;
        }

        public void AddAt(int index, T data)
        {
            //...
        }

        public bool SwitchData(int index, T data)
        {
            //...
            return true;
        }

        public T? this[int index]
        {
            get
            {
                if (index < Count && index >= 0)
                    return GetData(index)!;
                else if (index >= -Count && index < 0)
                    return GetData(Count + index);
                return default;
            }
        }
    }
}