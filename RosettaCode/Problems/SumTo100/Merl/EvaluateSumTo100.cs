using System.Buffers;
using System.Collections.Frozen;
using System.Diagnostics;
using System.Text;

using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace RosettaCode.Problems.SumTo100.Merl
{
	public class EvaluateSumTo100
	{
		private static FrozenSet<int> m_SupportedOperators = ['+', '-', '*', '/'];


		/// <summary>
		/// Performs the traditional Sum to 100 for the digits 1-9 and the add/subtract operators
		/// Outputs information about the solutions identified to the console per the
		/// desired points in the Rosetta Code desription.
		/// </summary>
		public void PerformStandardEvaluation()
		{
			Console.WriteLine("Sum to 100 for the digits 1-9 and the add/substract operators");
			Console.WriteLine("Evaluating...");

			var duration = new Stopwatch();

			duration.Start();

			Dictionary<Accumulator, List<string>> results = EvaluateSums("123456789", ['+', '-']);

			duration.Stop();

			Console.WriteLine($"Results: (in {duration.ElapsedMilliseconds} ms)");

			//we have to remove any that resulted in NaN
			var unusable = results.Where(r => r.Key.IsNaN).ToList();

			//
			// -- Record that we had invalid expressions/results
			// -- and trim the result set
			//
			if (unusable.Count > 0)
			{
				Console.WriteLine($"There were {unusable.Count} expressions which yielded NaN and are not being considered for solutions.");

				results = results.Except(unusable).ToDictionary();
			}

			//
			// -- Solutions for 100
			//
			if (results.TryGetValue(100, out var solutions))
			{
				Console.WriteLine($"{solutions.Count} solutions for sum of 100:");

				foreach (string solution in solutions)
				{
					Console.WriteLine($"\t{solution}");
				}
			}
			else
			{
				Console.WriteLine("There were no solutions for sum of 100!");
			}

			if (results.Count == 0)
			{
				Console.WriteLine("There were no valid soluitons at all.");
			}
			else
			{
				//
				// -- Max Solutions for a value from 0 to infinity
				//
				var mostSolutions = results.Where(r => r.Key >= 0).OrderByDescending(kvp => kvp.Value.Count).First();
				Console.WriteLine($"The sum with the most number of solutions was: {mostSolutions.Key} with {mostSolutions.Value.Count} expressions.");

				//
				// -- lowest positive sum with no solutions:
				//
				int unsolved = 0;
				for (int min = 1; min < 123456789; min++) //max is just the whole number as any split would be less.
				{
					if (!results.ContainsKey(min))
					{
						unsolved = min;
						break;
					}
				}

				if (unsolved != 0)
				{
					Console.WriteLine($"The lowest positive sum that could not be expressed was: {unsolved}");
				}
				else
				{
					Console.WriteLine("There was no positive sum which could not be expressed .");
				}

				//
				// -- 10 highest values wtih solutions
				//
				var hightestValues = results.Keys.OrderByDescending(k => k).Take(10).ToList();

				Console.WriteLine($"The up-to 10 highest numbers expressable were: {string.Join(", ", hightestValues)}");
			}
		}

		/// <summary>
		/// Returns the possible evaluations that are the result of inserting one or more of the provided <paramref name="operators"/>
		/// into the sequence of provided <paramref name="digits"/> ordered as provided without adjustment.
		/// </summary>
		/// <param name="digits">the string of digits, typically the digits from 1 through 9 in increasing numerical order.</param>
		/// <param name="operators">the characters for the set of operators to apply, typically just addition and substraction</param>
		/// <returns></returns>
		public Dictionary<Accumulator, List<string>> EvaluateSums(string digits, char[] operators)
		{
			//convert input digits to their numerical value
			int[] integerDigits = digits.ToCharArray().Where(char.IsNumber).Select(c => c - '0').ToArray();

			//return the possible results
			var intResults = EvaluateSums(integerDigits, operators);

			var dictResults = intResults.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Select(ExpressionAsString).ToList());

			intResults.Clear();

			return dictResults;
		}

		/// <summary>
		/// Converts the provided <see cref="Exception"/> into a string for frieindly display
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		public static string ExpressionAsString(int[] expression) =>
			expression.Length == 0 ?
				"Empty!" :
				expression.Aggregate(new StringBuilder(expression.Length), (builder, intVal) =>
					{
						char val = (char)intVal;

						if (intVal is <= 9)
						{
							val += '0';
						}

						builder.Append(val);

						return builder;
					}).ToString();

		/// <summary>
		/// Builds a grouped result set for the specified digits and operators.
		/// Keyed by the evaluated value, stored as an <see cref="Accumulator"/>
		/// and containin a list of the expressions yielding the evaluated value.
		/// </summary>
		/// <param name="digits"></param>
		/// <param name="operators"></param>
		/// <returns></returns>
		public Dictionary<Accumulator, List<int[]>> EvaluateSums(int[] digits, char[] operators) =>
			PossibleExpressions(digits, operators).Aggregate(new Dictionary<Accumulator, List<int[]>>(), (results, expression) =>
				{
					Accumulator value = EvaluateExpression(expression, m_SupportedOperators);

					if (!results.TryGetValue(value, out var solutions))
					{
						results[value] = [expression];
					}
					else
					{
						solutions.Add(expression);
					}

					return results;
				});

		/// <summary>
		/// Performs an evaluatoin of the provided <paramref name="expression"/>.
		/// If an operator is not supported, the result will be indicated as NaN.
		/// </summary>
		/// <param name="expression"></param>
		/// <param name="operators"></param>
		/// <returns></returns>
		public static Accumulator EvaluateExpression(int[] expression, FrozenSet<int> operators)
		{
			int nextValue = 0;

			Queue<int> values = new();
			Queue<int> opers = new();

			for (int digitPos = 0; digitPos < expression.Length; digitPos++)
			{
				if (operators.Contains(expression[digitPos]))
				{
					values.Enqueue(nextValue);
					opers.Enqueue(expression[digitPos]);

					nextValue = 0;
				}
				else if (expression[digitPos] is >= 0 and <= 9)
				{
					nextValue = nextValue * 10 + expression[digitPos];
				}
				else
				{
					//we received junk!
					return new Accumulator(0, true);
				}
			}

			//we should have ended with a value in the last position
			values.Enqueue(nextValue);

			Accumulator acc = values.Dequeue();

			while(!acc.IsNaN && opers.TryDequeue(out int nextOp))
			{
				PerformOperation(nextOp, values.Dequeue(), ref acc);
			}

			return acc;
		}

		/// <summary>
		/// Applies the specified operation with the specified value to the
		/// accumulator, with the result remaining in the accumulator.
		/// </summary>
		/// <param name="nextOperator"></param>
		/// <param name="nextValue"></param>
		/// <param name="acc"></param>
		public static void PerformOperation(int nextOperator, int nextValue, ref Accumulator acc)
		{
			//nothing can fix a NaN
			if (acc.IsNaN) return;

			try { 
				switch (nextOperator)
				{
					case '+':
						acc += nextValue;
						break;

					case '-':
						acc -= nextValue;
						break;

					case '*':
						acc *= nextValue;
						break;

					case '/':
						//handle divide by 0 w/o gracefully
						if (nextValue == 0)
						{
							acc.IsNaN = true;
						}
						else
						{ 
							acc /= nextValue;
						}
						break;
				}
			}
			catch(Exception)
			{
				acc.IsNaN = true;
			}

		}

		/// <summary>
		/// Generates the possible expressions for the given sets of <paramref name="digits"/> and <paramref name="operators"/>
		/// Digits are consumed in-order.
		/// Operators may start an expression
		/// Operators must be followed by at least one digit.
		/// </summary>
		/// <param name="digits"></param>
		/// <param name="operators"></param>
		/// <returns></returns>
		public static IEnumerable<int[]> PossibleExpressions(int[] digits, char[] operators)
		{
			var maxOperators = digits.Length;

			//loop over the possible number of supported operators
			for (int opCount = 1; opCount <= maxOperators; opCount++)
			{
				//this is the size of the expresion when filled
				int exprSize = digits.Length + opCount;

				//and this is the last position where we can start placing operators
				//length of the expression, less 2 * num ops, +1 for placing the op, -1 to convert to offset
				int lastStartingOpPos = exprSize - (2 * opCount);

				//create the expression workspace
				int[] expression = new int[exprSize];

				List<int[]> expressions = [];
				BuildExpressions(expression, digits, operators, opCount, 0, lastStartingOpPos, 0, 0, expressions);

				foreach (var item in expressions)
				{
					yield return item;
				}

			}
		}

		/// <summary>
		/// Generates valid expressions, consuming the digits and operators provided,
		/// starting at the given expression position and placing the next operator at the 
		/// specified operator position.  Generation continues recursively until we have exhausted
		/// the possible first operator starting positions and the number of operators to include
		/// in the expression.
		/// </summary>
		/// <param name="expression">the current expression state</param>
		/// <param name="digits">the set of available digits</param>
		/// <param name="operators">the set of available operators</param>
		/// <param name="operatorCount">the number of operators to be placed</param>
		/// <param name="nextOperatorPosition">the position of the next operator placement at the current remaining operator count</param>
		/// <param name="finalOperatorPosition">the position of the last operator placement at the current remaining operator count</param>
		/// <param name="expressionPosition">the current position in the expression to fill</param>
		/// <param name="nextDigitIndex">the index of the next digit to consume</param>
		/// <param name="expressions">Holds the results from building expressions</param>
		private static void BuildExpressions(int[] expression,
											 int[] digits,
											 char[] operators,
											 int operatorCount,
											 int nextOperatorPosition,
											 int finalOperatorPosition,
											 int expressionPosition,
											 int nextDigitIndex,
											 List<int[]> expressions)
		{
			//special case, no more operators to place:
			if (operatorCount == 0)
			{
				//just fill in with digits
				while(expressionPosition < expression.Length)
				{
					expression[expressionPosition++] = digits[nextDigitIndex++];
				}
			}

			//if the current position is at the end, yield the result
			if (expressionPosition == expression.Length)
			{
				expressions.Add((int[])expression.Clone());

				return;
			}

			//fill to next operator
			while(expressionPosition < nextOperatorPosition)
			{
				//just take the next digit
				expression[expressionPosition++] = digits[nextDigitIndex++];
			}


			//build sub-expressions for each operator at the current position
			foreach (var oper in operators)
			{
				//place the operator and then a digit
				//but don't change our variables
				//as we'll need them to remain the same as we loop over the operators
				expression[nextOperatorPosition] = oper;
				expression[nextOperatorPosition + 1] = digits[nextDigitIndex];

				//recurse to build the remaining expression
				//with the new remaining operator count, the
				//final position shifted forard for the next operator,
				//the expression position located after this operator and its required digit,
				//and the next digit that can be consumed.
				BuildExpressions(expression, 
									digits, 
									operators, 
									operatorCount - 1, 
									nextOperatorPosition + 2, 
									finalOperatorPosition + 2, 
									expressionPosition + 2, 
									nextDigitIndex + 1, 
									expressions);
			}

			//with this starting postiion evaluated, identify the next possible position
			nextOperatorPosition++;

			//and as long as we haven't exceeded the final first operator starting position,
			if (nextOperatorPosition <= finalOperatorPosition)
			{ 
				//we start generating again for the set of expressions with the first operator
				//moved to its next possible starting position in the expression
				BuildExpressions(expression, 
								 digits, 
								 operators, 
								 operatorCount, 
								 nextOperatorPosition, 
								 finalOperatorPosition, 
								 expressionPosition, 
								 nextDigitIndex, 
								 expressions);
			}
		}
	}
}
