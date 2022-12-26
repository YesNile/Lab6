using System;
using System.Collections.Generic;
using System.Linq;

namespace lab_6_ochkoshnik.hash_table
{
    /// <summary>
    /// Таблица с разрешением коллизий с помощью цепочек
    /// </summary>
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
        protected override int GetHashOwn(object key, int index)
        {
            string keyStr = key.ToString();
            int hash = 1;
            for (int i = 0; i < keyStr.Length / 2; i++)
            {
                hash += keyStr[i] - 'a' + 1;
            }

            return hash % _cells.Length;
        }

        /// <summary>
        /// Поиск по таблице
        /// </summary>
        public override TValue Search(TKey id)
        {
            int index = GetHashMethod(HashingTypes[0]).Invoke(id, 0);
            var list = _cells[index];
            return list.First(x => x.Key.Equals(id)).Value;
        }

        /// <summary>
        /// Добавление в таблицу
        /// </summary>
        public override int Add(TKey key, TValue dataItem)
        {
            int index = GetHashMethod(HashingTypes[0]).Invoke(key, 0);
            if (_cells[index] is null)
            {
                _cells[index] = new LinkedList<KeyValuePair<TKey, TValue>>();
            }

            _cells[index].AddLast(new KeyValuePair<TKey, TValue>(key, dataItem));
            Count++;

            // Console.WriteLine($"Элемент с ключем {key} добавлен c кодом {index}");

            return index;
        }

        /// <summary>
        /// Удаление из таблицы
        /// </summary>
        public override bool Remove(TKey id)
        {
            int index = GetHashMethod(HashingTypes[0]).Invoke(id, 0);
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

        /// <summary>
        /// Полечение длины самой длинной цепочки
        /// </summary>
        public int LengthLongestChain => _cells.Max(x => x?.Count ?? 0);
        /// <summary>
        /// Полечение длины самой короткой цепочки
        /// </summary>
        public int LengthShortestChain => _cells.Min(x => x?.Count ?? 0);
        /// <summary>
        /// Полечение коэффициента заполнения
        /// </summary>
        public int ElementsCount => Count / (Size - 1);
    }
}