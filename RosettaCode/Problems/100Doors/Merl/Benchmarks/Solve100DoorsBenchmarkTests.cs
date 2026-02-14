using System;
using System.Collections.Generic;
using System.Text;

using BenchmarkDotNet.Attributes;

namespace RosettaCode.Problems._100Doors.Merl.Benchmarks
{
	[MemoryDiagnoser]
	public class Solve100DoorsBenchmarkTests
	{
		[Benchmark(Baseline = true)]
		public void Basic()
		{
			var solutions = new Solve100Doors();

			solutions.Solve_Basic_Unoptimized(100, 100, out _, out _);
		}

		[Benchmark]
		public void Instanced()
		{
			var solutions = new Solve100Doors();

			solutions.Solve_Class_Unoptimized(100, 100, out _, out _);
		}

		[Benchmark]
		public void Togglers_Hieararchy()
		{
			var solutions = new Solve100Doors();

			solutions.Solve_Togglers_Hierarchy_Unoptimized(100, 100, out _, out _);
		}

		[Benchmark]
		public void Togglers()
		{
			var solutions = new Solve100Doors();

			solutions.Solve_Togglers_Unoptimized(100, 100, out _, out _);
		}


		[Benchmark]
		public void Basic_Parallel()
		{
			var solutions = new Solve100Doors();

			solutions.Solve_Basic_Parallel(100, 100, out _, out _);
		}

		[Benchmark]
		public void Instanced_Parallel()
		{
			var solutions = new Solve100Doors();

			solutions.Solve_Class_Parallel(100, 100, out _, out _);
		}

		[Benchmark]
		public void Togglers_Hierarchy_Parallel()
		{
			var solutions = new Solve100Doors();

			solutions.Solve_Togglers_Hierarchy_Parallel(100, 100, out _, out _);
		}

		[Benchmark]
		public void Togglers_Parallel()
		{
			var solutions = new Solve100Doors();

			solutions.Solve_Togglers_Parallel(100, 100, out _, out _);
		}
	}
}
