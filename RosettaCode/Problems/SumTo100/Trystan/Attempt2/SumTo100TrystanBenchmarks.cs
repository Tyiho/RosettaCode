

using BenchmarkDotNet.Attributes;

namespace RosettaCode.Problems.SumTo100.Trystan.Attempt2;

[MemoryDiagnoser]
public class SumTo100TrystanBenchmarks
{
    [Benchmark]
    public void GetAllSumsThatEqual100()
    {
        var sumTo100 = new RosettaCode.Problems.SumTo100.Trystan.Attempt2.SumTo100();
        List<SumTo100Result> strings = sumTo100.GetAllSumsThatEqual100();
    }

    [Benchmark]
    public void GetAllSumsThatEqual100WithCustomInput()
    {
        SumTo100Parameters parameters = new SumTo100Parameters(
            [1, 2, 3, 4, 5, 6, 7, 8, 9], 
            true, 
            true, 
            true, 
            true);

        var sumTo100 = new RosettaCode.Problems.SumTo100.Trystan.Attempt2.SumTo100(parameters);
        List<SumTo100Result> strings = sumTo100.GetAllSumsThatEqual100();
    }
}

