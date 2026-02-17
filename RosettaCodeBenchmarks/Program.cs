using BenchmarkDotNet.Running;
using RosettaCode.Problems._100Doors.Merl.Benchmarks;
using RosettaCode.Problems._100Prisoners.Merl;

internal class Program
{
	private static void Main(string[] args)
	{
		//BenchmarkRunner.Run<Solve100DoorsBenchmarkTests>();
		BenchmarkRunner.Run<Solve100PrisonersBenchmarkTests>();
	}
}