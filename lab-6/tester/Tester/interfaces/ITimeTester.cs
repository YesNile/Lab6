using System;

namespace lab_6.tester.Tester.interfaces
{
    public interface ITimeTester
    {
        public void Test(Action algorithm, int iterNumber, string name);
    }
}