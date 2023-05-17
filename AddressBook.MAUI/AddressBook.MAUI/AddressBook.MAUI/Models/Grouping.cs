using System;
using System.Collections.ObjectModel;

namespace AddressBook.MAUI.Models
{
    public class Grouping<K, T> : ObservableCollection<T>
    {
        public K Key { get; private set; }
        public int ColumnCount { get; private set; }

        public Grouping(K key)
        {
            Key = key;
        }

        public Grouping(K key, IEnumerable<T> items)
            : this(key)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        public Grouping(K key, IEnumerable<T> items, int columnCount)
            : this(key, items)
        {
            ColumnCount = columnCount;
        }
    }
}

