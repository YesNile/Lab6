namespace lab_6_ochkoshnik.tester.Tester.classes
{
    public record TestResult<TResult>(int ID, string AlgorithmName, TResult Result, TResult[] LocalResults);
}
