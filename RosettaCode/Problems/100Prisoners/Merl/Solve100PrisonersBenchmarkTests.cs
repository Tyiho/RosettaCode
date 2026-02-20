using System;
using System.Collections.Generic;
using System.Text;

using BenchmarkDotNet.Attributes;

namespace RosettaCode.Problems._100Prisoners.Merl
{
	[MemoryDiagnoser]
	public class Solve100PrisonersBenchmarkTests
	{
		private const int DESIRED_ITERATIONS = 30000;
		private const bool CONSOLE_OUTOUT = false;

		[Benchmark]
		public void Random()
		{
			Solve100Prisoners.EvaluateSolutionStrategy("Random", Solve100Prisoners.RunPrisoner_RandomStrategy, DESIRED_ITERATIONS, CONSOLE_OUTOUT);
		}

		[Benchmark]
		public void RandomWithMemory()
		{
			Solve100Prisoners.EvaluateSolutionStrategy("Random w/Memory", Solve100Prisoners.RunPrisoner_RandomWithMemoryStrategy, DESIRED_ITERATIONS, CONSOLE_OUTOUT);
		}

		[Benchmark]
		public void Optimal()
		{
			Solve100Prisoners.EvaluateSolutionStrategy("Optimal", Solve100Prisoners.RunPrisoner_OptimalStrategy, DESIRED_ITERATIONS, CONSOLE_OUTOUT);
		}

		[Benchmark]
		public void OptimalWithMemory()
		{
			Solve100Prisoners.EvaluateSolutionStrategy("Optimal w/Memory", Solve100Prisoners.RunPrisoner_OptimalWithMemoryStrategy, DESIRED_ITERATIONS, CONSOLE_OUTOUT);
		}
	}
}
