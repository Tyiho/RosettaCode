using System;
using System.Collections.Generic;
using System.Text;

using FluentAssertions;

using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsTCPIP;

using RosettaCode.Problems.SumTo100.Merl;

namespace RosettaCodeTest.Problems.SumTo100.Merl
{
	[TestClass]
	public class EvaluateSumTo100Tests
	{
		[TestMethod]
		[Ignore]
		public void Runner()
		{
			var eval = new EvaluateSumTo100();

			var results = eval.EvaluateSums([1,2,3,4,5,6,7,8,9], ['+', '-']);

			Console.WriteLine("Done!");
		}

		[TestMethod]
		[Ignore]
		public void PerformProblemEvaluation()
		{
			var eval = new EvaluateSumTo100();

			eval.PerformStandardEvaluation();

			Console.WriteLine("Done!");
		}

		public static IEnumerable<object[]> ExpressionAsStringTestCases()
		{
			//digits
			for (int i = 1; i <=9; i++)
			{
				yield return [ new int[] {i}, i.ToString() ];
			}

			//operators
			yield return [new int[] { '+' }, "+"];
			yield return [new int[] { '-' }, "-"];
			yield return [new int[] { '*' }, "*"];
			yield return [new int[] { '/' }, "/"];


			//and a few positional combinations
			yield return [new int[] { '+', 1, 2, 3 }, "+123"];
			yield return [new int[] { 1, '-', 2, 3 }, "1-23"];
			yield return [new int[] { 1, 2, '*', 3 }, "12*3"];
			yield return [new int[] { 1, 2, 3, '/'}, "123/"];
		}

		[TestMethod]
		[DynamicData(nameof(ExpressionAsStringTestCases))]
		public void ExpressionAsString_PropertyConverts_ExpressionsFromIntegerArrayToString(int[] expression, string expectedResult)
		{
			// Act / Assert
			EvaluateSumTo100.ExpressionAsString(expression).Should().Be(expectedResult);
		}


		public static IEnumerable<object[]> PerformOperationTestCases()
		{
			yield return [new Accumulator(1), '+', 1, new Accumulator(2)];
			yield return [new Accumulator(1), '-', 1, new Accumulator(0)];
			yield return [new Accumulator(1), '*', 2, new Accumulator(2)];
			yield return [new Accumulator(2), '/', 2, new Accumulator(1)];

			//we're currently leaving the value in the accumulator at whatever it was
			//when we encountered the NaN condition
			yield return [new Accumulator(1), '/', 0, new Accumulator(1, true)];

		}

		[TestMethod]
		[DynamicData(nameof(PerformOperationTestCases))]
		public void PerformOperation_Yields_CorrectOperatorEvaluation(Accumulator acc, int nextOperator, int nextValue, Accumulator expectedResult)
		{
			// Act
			EvaluateSumTo100.PerformOperation(nextOperator, nextValue, ref acc);

			// Assert
			acc.Should().Be(expectedResult);
		}
	}
}
