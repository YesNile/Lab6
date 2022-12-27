using System;
using System.Collections.Generic;
using lab_6_ochkoshnik.hash_table;
using lab_6_ochkoshnik.tester;

namespace lab_6_ochkoshnik
{
    class Program
    {
        static void Main(string[] args)
        {
            // TestHashTable.TestingHashFunctionByClass();
            TestHashTable.TestingOwnHashFunction();
            
            var size = 1000;
            var table = new HashTableOpen<string, DataItem>(size);
            // var table = new HashTableChains<string, DataItem>(size);
            
            table.Clear();
            var count = 1000;
            Console.WriteLine("Generating...");
            
            
            
            var users = new List<DataItem>();
            for (var i = 0; i < count; i++)
                users.Add(DataItem.RandomInstance());
            Console.WriteLine("Pushing...");
            
            var keys = new List<int>();
            for (var i = 0; i < count; i++)
                keys.Add(table.Add(users[i].Id, users[i]));
            Console.WriteLine($"Done, press enter to print out all the {count} objects");
            
            
            Console.ReadLine();
            
            var cnt = 0;
            for (var i = 0; i < count; i++)
            {
                var data = table[users[i].Id];
                cnt += data.Id == users[i].Id && users[i].RegDate == data.RegDate ? 1 : 0;
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = data.Id == users[i].Id && users[i].RegDate == data.RegDate
                    ? ConsoleColor.Green
                    : ConsoleColor.Red;
                Console.WriteLine(
                    $"{data.Id} - {data.RegDate} \t\t\t\t| {users[i].Id} - {users[i].RegDate} - \t\t\t\t| {users[i].GetHashCode()}");
            }
            
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine($"{cnt}/{count} ({cnt * 100.0 / count}%)");
            // Console.WriteLine($"Длина самой длинной цепочки {table.LengthLongestChain}\n" +
            //                   $"Длина самой короткой цепочки {table.LengthLongestChain}\n" +
            //                   $"Коэффициент заполнения {table.ElementsCount}");
            Console.WriteLine($"Длина самого длинного кластера {table.LargestClusterLength}");
            Console.ReadLine();
        }
    }
}