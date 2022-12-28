using System;
using lab_6.hash_table;
using lab_6.tester.Tester;

namespace lab_6_ochkoshnik.tester
{
    public class HashTableChainedTester : HashTableTester
    {
        public static void Testing(params HashingType[] hashFuncTypes)
        {
            const int SIZE_GENERATION = 100_000;

            Console.WriteLine("\nНачало тестирования хеш-таблицы (цепочки)");
            foreach (var hashFunc in hashFuncTypes)
            {
                Console.WriteLine(
                    $"\nТест хеш-таблицы с функцией хеширования: \"{hashFunc.ToString()}\"");
                var ht = new HashTableChains<string, string>(1000);

                Console.WriteLine("Генерация данных в таблицу");
                var (keys, values) = GeneratingValuesAndKeys(SIZE_GENERATION);

                Console.WriteLine("Добавление данных в таблицу");
                for (var i = 0; i < keys.Count; i++)
                {
                    ht.Add(keys[i], values[i]);
                }

                Console.WriteLine($"Коэффицент заполнения таблицы: {ht.ElementsCount}");
                Console.WriteLine($"Максимальная длинна цепочки в таблице: {ht.LengthLongestChain}");
                Console.WriteLine($"Минимальная длинна цепочки в таблице: {ht.LengthShortestChain}");
            }
        }

        public static void TestForTable(string name, int iterCount)
        {
            var testerTime = new TimeTester();
            var testerMemory = new MemoryTester();
            Action<int> func = StartSpecialTest;

            for (int i = 1; i <= 10000; i++)
            {
                Console.WriteLine($"Тест алгоритма: {name} | Итерация: {i}");
                testerTime.Test(() => func.Invoke(i), iterCount, name);
                testerMemory.Test(() => func.Invoke(i), iterCount, name);
            }

            testerTime.SaveAsExcel(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{name} - время");
            testerTime.AllResults.Clear();

            testerMemory.SaveAsExcel(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{name} - память");
            testerMemory.AllResults.Clear();
        }

        private static void StartSpecialTest(int size)
        {
            var ht = new HashTableChains<string, string>(1000);
            var (keys, values) = GeneratingValuesAndKeys(size);
            for (var i = 0; i < keys.Count; i++)
            {
                ht.Add(keys[i], values[i]);
            }
        }
    }
}