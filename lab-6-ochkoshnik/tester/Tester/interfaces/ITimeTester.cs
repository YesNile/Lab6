using System;

namespace lab_6_ochkoshnik.tester.Tester.interfaces
{
    public interface ITimeTester
    {
        public void Test(Action algorithm, int iterNumber, string name);
    }
}