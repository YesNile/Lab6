using System;
using System.Collections.Generic;
using System.Linq;

namespace lab_6_ochkoshnik.hash_table
{
    public class HashTableChains<TKey, TValue> : AbstractHashTable<TKey, TValue>
    {
        private readonly LinkedList<KeyValuePair<TKey, TValue>>[] _cells;

        public TValue this[TKey id] => Search(id);

        public HashTableChains(int size)
        {
            _cells = new LinkedList<KeyValuePair<TKey, TValue>>[size];
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
        public override TValue Search(TKey id)
        {
            var list = _cells[CalculateHash(id.ToString())];
            return list.First(x => x.Key.Equals(id)).Value;
        }

        /// <summary>
        /// Добавление в таблицу
        /// </summary>
        public override int Add(TKey key, TValue dataItem)
        {
            var index = CalculateHash(key.ToString());
            if (_cells[index] is null)
            {
                _cells[index] = new LinkedList<KeyValuePair<TKey, TValue>>();
            }

            _cells[index].AddLast(new KeyValuePair<TKey, TValue>(key, dataItem));
            Count++;

            Console.WriteLine($"Элемент с ключем {key} добавлен c кодом {index}");

            return index;
        }

        /// <summary>
        /// Удаление из таблицы
        /// </summary>
        public override bool Remove(TKey id)
        {
            var index = CalculateHash(id.ToString());
            var removeData = _cells[index]?.First;
            if (removeData != null)
            {
                _cells[index].Remove(removeData);
            }

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

        public int ElementsCount => Count / (Size - 1);
    }
}