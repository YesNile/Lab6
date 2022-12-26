using System;
using System.Security.Cryptography;
using System.Text;

namespace lab_6_ochkoshnik.hash_table
{
    public abstract class AbstractHashTable<TKey, TValue> : IHashTable<TKey, TValue>
    {
        private readonly double _goldenRatioConst = (Math.Sqrt(5) - 1) / 2;
        public abstract TValue Search(TKey id);
        public abstract int Add(TKey key, TValue dataItem);
        public abstract bool Remove(TKey id);
        public abstract void Clear();
        
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
        public string GetHashMD5(object key)
        {
            using var md = MD5.Create();
            var hash = md.ComputeHash(Encoding.UTF8.GetBytes(key?.ToString() ?? String.Empty));

            return Convert.ToBase64String(hash);
        }
    }
}