using System.Collections.Generic;
using System.Linq;

namespace lab_6_ochkoshnik.hash_table
{
    public class HashTableChains : IHashTable
    {
        private readonly LinkedList<DataItem>[] _cells;
        public readonly int Size;
        public int Count { get; private set; }

        public HashTableChains(int size)
        {
            _cells = new LinkedList<DataItem>[size];
            Size = size;
        }

        private int CalculateHash(string key)
        {
            int hash = 1;
            for (int i = 0; i < key.Length / 2; i++)
            {
                hash += key[i] - 'a' + 1;
            }

            return hash % _cells.Length;
        }

        public DataItem Search(string id) => _cells[CalculateHash(id)].First(t => t.Id == id);

        public int Add(DataItem dataItem)
        {
            var index = CalculateHash(dataItem.Id);
            if (_cells[index] is null) _cells[index] = new LinkedList<DataItem>();
            _cells[index].AddFirst(dataItem);
            Count++;
            return index;
        }

        /// <summary>
        /// Represents deletion of an object to a table.
        /// </summary>
        public bool Remove(string id)
        {
            var removeData = _cells[CalculateHash(id)].First(t => t.Id == id);
            _cells[CalculateHash(id)].Remove(removeData);
            Count--;
            return true;
        }

        // public (int, int, int) Calculations()
        // {
        //     int maxValue = 0;
        //     int minValue = int.MaxValue;
        //     int elements = 0;
        //     foreach (var data in _cells)
        //     {
        //         if (data is null)
        //         {
        //             continue;
        //         }
        //         elements++;
        //         maxValue = data.Count > maxValue ? data.Count : maxValue;
        //         minValue = data.Count < minValue ? data.Count : minValue;
        //     }
        //
        //     return (maxValue, minValue, elements);
        // }

        public int LengthLongestChain => _cells.Max(x => x?.Count ?? 0);
        public int LengthShortestChain => _cells.Min(x => x?.Count ?? 0);

        public int ElementsCount => Count / Size;
    }
}