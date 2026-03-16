using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RosettaCode.Problems._100Prisoners.Trystan.SecondAttempt;

[MemoryDiagnoser]
public class Simulate100PrisonersBenchmarkTests
{
    [Benchmark]
    public void RandomPrisonerStrategy()
    {
        Simulate100Prisoners.Simulate100RandomPrisoners10000Times();
    }

    [Benchmark]
    public void OptimalPrisonerStrategy()
    {
        Simulate100Prisoners.Simulate100OptimalPrisoners10000Times();
    }
}