using System;
using lab_6_ochkoshnik.hash_table;
using lab_6_ochkoshnik.tester.Tester;

namespace lab_6_ochkoshnik.tester
{
    public class TestHashTable
    {
        public static void TestingOwnHashFunction()
        {
            for (int i = 0; i < 6; i++)
            {
                HashTableChains<string, DataItem> table = new HashTableChains<string, DataItem>(10_000);
                var type = (HashingType) i;
                table.HashingTypes = new[] {type};

                TestTable((count) => TestHashTableAdd(table, count), $"Тест таблицы с цепочками ({type.ToString()})",
                    1);
            }

            for (int i = 0; i < 6; i++)
            {
                HashTableOpen<string, DataItem> table = new HashTableOpen<string, DataItem>(10_000);
                var type = (HashingType) i;
                table.HashingTypes = new[] {type};

                TestTable((count) => TestHashTableAdd(table, count), $"Тест таблицы с цепочками ({type.ToString()})",
                    1);
            }
        }

        public static void TestingHashFunctionByClass()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
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

                    TestTable((count) => TestHashTableAdd(table, count),
                        $"Тест таблицы с цепочками ({researchType.ToString()})",
                        1);
                }
            }
        }

        private static void TestHashTableAdd(AbstractHashTable<string, DataItem> table, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var user = DataItem.RandomInstance();
                table.Add(user.Id, user);
            }
        }

        private static void TestTable(Action<int> func, string name, int iterCount)
        {
            var tester = new TimeTester();
            var tester2 = new MemoryTester();
            for (int i = 1; i < 1_00; i += 1)
            {
                Console.WriteLine($"Тест алгоритма: {name} | Итерация: {i}");
                var count = i;
                tester.Test(() => func.Invoke(count), iterCount, name);
                tester2.Test(() => func.Invoke(count), iterCount, name);
            }

            tester.SaveAsExcel(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{name} - время");
            tester.AllResults.Clear();

            tester2.SaveAsExcel(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{name} - память");
            tester2.AllResults.Clear();
        }
    }
}