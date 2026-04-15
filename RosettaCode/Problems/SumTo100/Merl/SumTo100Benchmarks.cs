using System;
using System.Collections.Generic;
using System.Text;

using BenchmarkDotNet.Attributes;

namespace RosettaCode.Problems.SumTo100.Merl
{
	[MemoryDiagnoser]
	public class SumTo100Benchmarks
	{
		private char[] m_Operators = ['+', '-'];
		private int[] m_Digits = [1,2,3,4,5,6,7,8,9];
		private string m_DigitsString = "1234567890";

		[Benchmark(Baseline = true)]
		public void EvaluateSums_ArrayInputs()
		{
			var executor = new EvaluateSumTo100();

			_ = executor.EvaluateSums(m_Digits, m_Operators);
		}

		[Benchmark]
		public void EvaluateSums_StringInput()
		{
			var executor = new EvaluateSumTo100();

			//not really a fair comparison as the string variant, beyond evaluation,
			//also includes returning results as strings for easier reporting

			_ = executor.EvaluateSums(m_DigitsString, m_Operators);
		}
	}
}
