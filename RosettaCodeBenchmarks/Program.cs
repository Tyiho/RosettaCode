using BenchmarkDotNet.Running;
using RosettaCode.Problems._100Doors.Merl.Benchmarks;

internal class Program
{
	private static void Main(string[] args)
	{
		BenchmarkRunner.Run<Solve100DoorsBenchmarkTests>();
	}
}