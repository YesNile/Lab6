using System;

namespace lab_6_ochkoshnik.tester.Tester.interfaces
{
    public interface IStepTester
    {
        public void Test(Func<(double, int)> algorithm, int iterNumber, string name);
    }
}