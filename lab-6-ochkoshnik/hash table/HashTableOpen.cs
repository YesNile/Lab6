using System;
using System.Collections.Generic;

namespace lab_6_ochkoshnik.hash_table
{
    public class HashTableOpen<TKey, TValue> : AbstractHashTable<TKey, TValue>
    {
        private readonly KeyValuePair<TKey, TValue>[] _cells;
        public readonly int Size;
        public int Count { get; private set; }

        public HashTableOpen(int size)
        {
            Size = size;
            _cells = new KeyValuePair<TKey, TValue>[Size];
        }

        public TValue this[TKey id] => Search(id);

        /// <summary>
        /// Поиск по таблице
        /// </summary>
        public override TValue Search(TKey id)
        {
            var i = 0;
            do
            {
                var index = CalculateHash(id.ToString(), i++);
                if (_cells[index].Equals(default) || _cells[index].Key.Equals(id))
                {
                    continue;
                }

                return _cells[index].Value;
            } while (i < Size);

            return default;
            // throw new ArgumentException("Элемента с таким id не существует");
        }

        /// <summary>
        /// Добавление в таблицу
        /// </summary>
        public override int Add(TKey key, TValue dataItem)
        {
            var i = 0;
            do
            {
                var index = CalculateHash(key.ToString(), i++);
                if (_cells[index].Equals(default))
                {
                    continue;
                }

                _cells[index] = new KeyValuePair<TKey, TValue>(key, dataItem);
                Console.WriteLine($"Элемент {dataItem} с ключем {key} добавлен c кодом {index}");
                return index;
            } while (i < Size);

            Count++;
            return -1;
        }

        /// <summary>
        /// Удаление из таблицы
        /// </summary>
        public override bool Remove(TKey id)
        {
            var i = 0;
            do
            {
                var index = CalculateHash(id.ToString(), i++);
                if (_cells[index].Equals(default) || _cells[i].Key.Equals(id))
                {
                    continue;
                }

                _cells[index] = default;
                return true;
            } while (i < Size);

            Console.WriteLine($"Элемент с ключем {id} был удален");
            Count--;

            return false;
        }

        public override void Clear()
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                _cells[i] = default;
            }

            Count = 0;

            Console.WriteLine("Список был полностью очищен");
        }

        /// <summary>
        /// Получение хэш кода
        /// </summary>
        private int CalculateHash(string key, int i) => key[0] - 'a' + i;

        /// <summary>
        /// Получение длины самого длинного кластера
        /// </summary>
        public int LargestClusterLength
        {
            get
            {
                var i = 0;
                var max = 0;
                foreach (var cell in _cells)
                {
                    if (cell.Equals(default))
                    {
                        i++;
                    }
                    else
                    {
                        if (i > max) max = i;
                        i = 0;
                    }
                }

                if (i > max) max = i;
                return max;
            }
        }
    }
}