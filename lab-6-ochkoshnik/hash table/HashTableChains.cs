using System;
using System.Collections.Generic;
using System.Linq;

namespace lab_6_ochkoshnik.hash_table
{
    public class HashTableChains : AbstractHashTable<string, DataItem>
    {
        private readonly LinkedList<DataItem>[] _cells;
        public readonly int Size;
        public int Count { get; private set; }

        public DataItem this[string id] => Search(id);

        public HashTableChains(int size)
        {
            _cells = new LinkedList<DataItem>[size];
            Size = size;
        }

        /// <summary>
        /// Получение хэш кода
        /// </summary>
        private int CalculateHash(string key)
        {
            int hash = 1;
            for (int i = 0; i < key.Length / 2; i++)
            {
                hash += key[i] - 'a' + 1;
            }

            return hash % _cells.Length;
        }

        /// <summary>
        /// Поиск по таблице
        /// </summary>
        public override DataItem Search(string id) => _cells[CalculateHash(id)].First(t => t.Id == id);

        /// <summary>
        /// Добавление в таблицу
        /// </summary>
        public override int Add(DataItem dataItem)
        {
            var index = CalculateHash(dataItem.Id);
            if (_cells[index] is null)
            {
                _cells[index] = new LinkedList<DataItem>();
            }

            _cells[index].AddFirst(dataItem);
            Count++;

            Console.WriteLine($"Элемент {dataItem.Id} добавлен c кодом {index}");

            return index;
        }

        /// <summary>
        /// Удаление из таблицы
        /// </summary>
        public override bool Remove(string id)
        {
            var index = CalculateHash(id);
            var removeData = _cells[index].First(t => t.Id == id);
            _cells[index].Remove(removeData);

            Count--;

            Console.WriteLine($"Элемент с ключем {id} был удален");

            return true;
        }

        public override void Clear()
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                _cells[i] = null;
            }

            Count = 0;

            Console.WriteLine("Список был полностью очищен");
        }

        public int LengthLongestChain => _cells.Max(x => x?.Count ?? 0);
        public int LengthShortestChain => _cells.Min(x => x?.Count ?? 0);

        public int ElementsCount => Count / Size;
    }
}