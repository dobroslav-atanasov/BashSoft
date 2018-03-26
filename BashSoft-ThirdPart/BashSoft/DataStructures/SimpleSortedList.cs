namespace BashSoft.DataStructures
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using Contracts;

    public class SimpleSortedList<T> : ISimpleOrderedBag<T>
        where T : IComparable<T>
    {
        private const int DefaultSize = 16;

        private T[] data;
        private int size;
        private IComparer<T> comparison;

        public SimpleSortedList(IComparer<T> comparer, int capacity)
        {
            this.comparison = comparer;
            this.InitializeDataCollection(capacity);
        }

        public SimpleSortedList(int capacity)
            : this(Comparer<T>.Create((x, y) => x.CompareTo(y)), capacity)
        {
        }

        public SimpleSortedList(IComparer<T> comparer)
            : this(comparer, DefaultSize)
        {
        }

        public SimpleSortedList()
            : this(Comparer<T>.Create((x, y) => x.CompareTo(y)), DefaultSize)
        {
        }

        public int Size
        {
            get { return this.size; }
        }

        public void Add(T item)
        {
            if (this.data.Length == this.Size)
            {
                this.Resize();
            }

            this.data[this.size] = item;
            this.size++;
            Array.Sort(this.data, 0, this.size, this.comparison);
        }

        public void AddAll(ICollection<T> collection)
        {
            if (this.Size + collection.Count >= this.data.Length)
            {
                this.MultiResize(collection);
            }

            foreach (T item in collection)
            {
                this.data[this.Size] = item;
                this.size++;
            }

            Array.Sort(this.data, 0, this.Size, this.comparison);
        }

        public string JoinWith(string joiner)
        {
            StringBuilder sb = new StringBuilder();
            foreach (T item in this.data)
            {
                sb.Append(item);
                sb.Append(joiner);
            }

            sb.Remove(sb.Length - 1, 1);
            return sb.ToString().Trim();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Size; i++)
            {
                yield return this.data[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void InitializeDataCollection(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentException("Capacity cannot be negative!");
            }

            this.data = new T[capacity];
        }

        private void Resize()
        {
            T[] newData = new T[this.Size * 2];
            Array.Copy(this.data, newData, this.Size);
            this.data = newData;
        }

        private void MultiResize(ICollection<T> collection)
        {
            int newSize = this.data.Length * 2;
            while (this.Size + collection.Count >= newSize)
            {
                newSize *= 2;
            }

            T[] newData = new T[newSize];
            Array.Copy(this.data, newData, this.Size);
            this.data = newData;
        }
    }
}