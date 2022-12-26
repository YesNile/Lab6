using System.Collections.Generic;
using lab_6_ochkoshnik.tester.Tester.classes;

namespace lab_6_ochkoshnik.tester.Tester.interfaces
{
    public interface ITester<TResult>
    {
        public TestResult<TResult> LastResult { get; }
        public IList<TestResult<TResult>> AllResults { get; }

        public void SaveAsExcel(string path, string name, bool enableEmissions = true);
    }
}