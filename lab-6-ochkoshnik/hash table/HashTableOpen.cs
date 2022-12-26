using System;
using System.Collections.Generic;

namespace lab_6_ochkoshnik.hash_table
{
    public class HashTableOpen<TKey, TValue> : AbstractHashTable<TKey, TValue>
    {
        private readonly KeyValuePair<TKey, TValue>[] _cells;

        public ResearchType ResearchType { private get; set; } = ResearchType.Linear;

        /// <summary> Функция линейного хеширования </summary>
        private static readonly Func<Func<object, int, int>, object, int, int, int> LinearHashing =
            (f, key, sizeHashTable, index) => (f(key, sizeHashTable) + index) % sizeHashTable;

        /// <summary> Функция квадратичного хеширования </summary>
        private static readonly Func<Func<object, int, int>, object, int, int, int> QuadraticHashing =
            (f, key, sizeHashTable, index) => (f(key, sizeHashTable) + (int) Math.Pow(index, 2)) % sizeHashTable;

        /// <summary> Функция двойного хеширования</summary>
        private static readonly Func<Func<object, int, int>, Func<object, int, int>, object, int, int, int>
            DoubleHashing =
                (f1, f2, key, sizeHashTable, index) =>
                    (f1(key, sizeHashTable) + index * f2(key, sizeHashTable)) % sizeHashTable;

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
                var index = GetHashOwn(id.ToString(), i++);
                if (_cells[index].Equals(default(KeyValuePair<TKey, TValue>)) || !_cells[index].Key.Equals(id))
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
                var index = GetHashOwn(key.ToString(), i++);
                if (!_cells[index].Equals(default(KeyValuePair<TKey, TValue>)))
                {
                    continue;
                }

                _cells[index] = new KeyValuePair<TKey, TValue>(key, dataItem);
                // Console.WriteLine($"Элемент с ключем {key} добавлен c кодом {index}");
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
                var index = CalculateHash(id, i++);
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
        protected override int GetHashOwn(object key, int i) => key.ToString()[0] - 'a' + i;

        private int CalculateHash(TKey key, int i)
        {
            return ResearchType switch
            {
                ResearchType.Linear => LinearHashing(GetHashMethod(HashingTypes[0]), key, Size, i),
                ResearchType.Double => DoubleHashing(GetHashMethod(HashingTypes[0]), GetHashMethod(HashingTypes[1]), key, Size, i),
                ResearchType.Quadratic => QuadraticHashing(GetHashMethod(HashingTypes[0]), key, Size, i),
                _ => LinearHashing(GetHashMethod(HashingTypes[0]), key, Size, i)
            };
        }

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