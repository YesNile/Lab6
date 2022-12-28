using System.Collections.Generic;
using lab_6.tester.Tester.classes;

namespace lab_6.tester.Tester.interfaces
{
    public interface ITester<TResult>
    {
        public TestResult<TResult> LastResult { get; }
        public IList<TestResult<TResult>> AllResults { get; }

        public void SaveAsExcel(string path, string name, bool enableEmissions = true);
    }
}