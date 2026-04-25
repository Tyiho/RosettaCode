using BenchmarkDotNet.Attributes;

using Code = RosettaCode.Problems.KnightsPath.Merl;

namespace RosettaCode.Problems.KnightsTour.Merl
{
	[MemoryDiagnoser]
	public class KnightsTourBenchmarks
	{
		[Benchmark(Baseline = true)] //picking the main as our baseline just for comparison
		public void EvaluateKnighsTour()
		{
			Code.KnightsTour tour = new();

			tour.EvaluateKnightsTour();
		}

		[Benchmark]
		public void EvaluateRooksTour()
		{
			Code.KnightsTour tour = new();

			tour.EvaluateRooksTour();
		}

		[Benchmark]
		public void EvaluateQueensTour()
		{
			Code.KnightsTour tour = new();

			tour.EvaluateRooksTour();
		}
	}
}
