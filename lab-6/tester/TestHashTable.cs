using System;
using lab_6.hash_table;
using lab_6.tester.Tester;

namespace lab_6.tester
{
    public class TestHashTable
    {
        /// <summary>
        /// Тестирование своих алгоритмов
        /// </summary>
        public static void TestingOwnHashFunction()
        {
            for (int i = 0; i < 5; i++)
            {
                HashTableChains<string, DataItem> table = new HashTableChains<string, DataItem>(10_000);
                var type = (HashingType) i;
                table.HashingTypes = new[] {type};

                TestTable(table, (count) => TestHashTableAdd(table, count), $"Таблица с цепочками ({type.ToString()})",
                    1);
            }

            for (int i = 0; i < 5; i++)
            {
                HashTableOpen<string, DataItem> table = new HashTableOpen<string, DataItem>(10_000);
                var type = (HashingType) i;
                table.HashingTypes = new[] {type};

                TestTable(table, (count) => TestHashTableAdd(table, count),
                    $"Таблица с адрессацией ({type.ToString()})",
                    1);
            }
        }

        /// <summary>
        /// Тестирование хеширований по классам (последнее задание)
        /// </summary>
        public static void TestingHashFunctionByClass()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    HashTableOpen<string, DataItem> table = new HashTableOpen<string, DataItem>(10_000);
                    var researchType = (ResearchType) j;
                    table.ResearchType = researchType;

                    var hashingType = (HashingType) i;
                    if (researchType.Equals(ResearchType.Double))
                    {
                        var secondHashingType = (HashingType) ((i + 1) % 6);
                        table.HashingTypes = new[] {hashingType, secondHashingType};
                    }

                    TestTable(table, (count) => TestHashTableAdd(table, count),
                        $"Цепочки ({hashingType.ToString()}) ({researchType.ToString()})",
                        1);
                }
            }
        }

        /// <summary>
        /// Генерация данных для тестирования
        /// </summary>
        private static void TestHashTableAdd(AbstractHashTable<string, DataItem> table, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var user = DataItem.RandomInstance();
                table.Add(user.Id, user);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Сам тестер
        /// </summary>
        private static void TestTable(AbstractHashTable<string, DataItem> table, Action<int> func, string name,
            int iterCount)
        {
            var tester = new TimeTester();
            var tester2 = new MemoryTester();
            for (int i = 1; i < 1_000; i += 1)
            {
                Console.WriteLine($"Тест алгоритма: {name} | Итерация: {i}");
                var count = i;
                tester.Test(() => func.Invoke(count), iterCount, name);
                table.Clear();
                tester2.Test(() => func.Invoke(count), iterCount, name);
                table.Clear();
            }

            tester.SaveAsExcel(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{name} - время");
            tester.AllResults.Clear();

            tester2.SaveAsExcel(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{name} - память");
            tester2.AllResults.Clear();
        }
    }
}