﻿namespace lab_6_ochkoshnik.hash_table
{
    public interface IHashTable<TKey, TValue>
    {
        TValue? Search(TKey id);
        int Add(TKey key, TValue dataItem);
        bool Remove(TKey id);
    }
}