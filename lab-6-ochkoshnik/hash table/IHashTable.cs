namespace lab_6_ochkoshnik.hash_table
{
    public interface IHashTable
    {
        DataItem Search(string id);
        int Add(DataItem dataItem);
        bool Remove(string id);
    }
}