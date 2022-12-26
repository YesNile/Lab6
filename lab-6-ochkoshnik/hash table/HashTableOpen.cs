namespace lab_6_ochkoshnik.hash_table
{
    public class HashTableOpen : AbstractHashTable<string, DataItem>
    {
        private readonly DataItem[] _cells;
        public readonly int Size;

        public HashTableOpen(int size)
        {
            Size = size;
            _cells = new DataItem[Size];
        }

        public DataItem this[string id] => Search(id);

        /// <summary>
        /// Поиск по таблице
        /// </summary>
        public override DataItem Search(string id)
        {
            var i = 0;
            do
            {
                var index = CalculateHash(id, i++);
                if (_cells[index] == null || _cells[index].Id != id)
                {
                    continue;
                }
                return _cells[index];
            } while (i < Size);

            return null;
        }

        /// <summary>
        /// Добавление в таблицу
        /// </summary>
        public override int Add(DataItem dataItem)
        {
            var i = 0;
            do
            {
                var index = CalculateHash(dataItem.Id, i++);
                if (_cells[index] is not null)
                {
                    continue;
                }
                _cells[index] = dataItem;
                return index;
            } while (i < Size);

            return -1;
        }

        /// <summary>
        /// Удаление из таблицы
        /// </summary>
        public override bool Remove(string id)
        {
            var i = 0;
            do
            {
                var index = CalculateHash(id, i++);
                if (_cells[index] == null || _cells[i].Id != id)
                {
                    continue;
                }
                _cells[index] = null;
                return true;
            } while (i < Size);

            return false;
        }

        public override void Clear()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Получение хэш кода
        /// </summary>
        private int CalculateHash(string key, int i) => key[0] - 'a' + i;

        public int LargestCluster
        {
            get
            {
                var i = 0;
                var max = 0;
                foreach (var cell in _cells)
                {
                    if (cell == null)
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