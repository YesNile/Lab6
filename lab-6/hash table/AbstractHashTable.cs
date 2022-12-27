using System;
using System.Security.Cryptography;
using System.Text;

namespace lab_6.hash_table
{
    /// <summary>
    /// Абстрактный класс для базовых алгоритмов
    /// </summary>
    public abstract class AbstractHashTable<TKey, TValue> : IHashTable<TKey, TValue>
    {
        private readonly double _goldenRatioConst = (Math.Sqrt(5) - 1) / 2;
        public abstract TValue Search(TKey id);
        public abstract int Add(TKey key, TValue dataItem);
        public abstract bool Remove(TKey id);
        public abstract void Clear();
        protected abstract int GetHashOwn(object key, int index);

        public HashingType[] HashingTypes { protected get; set; } = {HashingType.Own};

        public int Size { get; protected set; }
        public int Count { get; protected set; }


        /// <summary>
        /// Получение хэша методом умножения
        /// </summary>
        public int GetHashMulti(object key, int sizeHashTable) =>
            (int) Math.Abs(sizeHashTable * (key.GetHashCode() * _goldenRatioConst % 1));


        /// <summary>
        /// Получение хэша методом деления
        /// </summary>
        public int GetHashDiv(object key, int sizeHashTable) => Math.Abs(key.GetHashCode() % sizeHashTable);

        /// <summary>
        /// Получить хеш-код SHA256
        /// </summary>
        public int GetHashSha256(object key, int sizeHashTable)
        {
            var sha256 = SHA256.Create();
            var bytesSha256In = Encoding.UTF8.GetBytes(key?.ToString() ?? string.Empty);
            var bytesSha256Out = sha256.ComputeHash(bytesSha256In);
            var resultSha256 = BitConverter.ToInt32(bytesSha256Out, 0);

            return Math.Abs(resultSha256 % sizeHashTable);
        }

        /// <summary>
        /// Получение хэша с помощью MD5
        /// </summary>
        public int GetHashMD5(object key, int sizeHashTable)
        {
            using var md = MD5.Create();
            var hash = md.ComputeHash(Encoding.UTF8.GetBytes(key?.ToString() ?? String.Empty));
            string strHash = BitConverter.ToString(hash);
            strHash = strHash.Replace("-", "");

            int sum = 0;
            for (int i = 0; i < strHash.Length; i++)
            {
                sum += Convert.ToInt32(strHash[i]);
            }

            return sum % sizeHashTable;
        }
        
        /// <summary>
        /// Получение метода хеширования
        /// </summary>
        protected Func<object, int, int> GetHashMethod(HashingType type)
        {
            return type switch
            {
                HashingType.Multi => GetHashMulti,
                HashingType.Div => GetHashDiv,
                HashingType.Sha256 => GetHashSha256,
                HashingType.MD5 => GetHashMD5,
                _ => GetHashOwn
            };
        }
    }
}