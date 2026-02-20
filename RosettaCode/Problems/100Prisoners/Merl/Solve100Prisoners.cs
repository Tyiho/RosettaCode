using System;
using System.Collections.Generic;
using System.Text;

namespace RosettaCode.Problems._100Prisoners.Merl
{
	/* Notes:
	 * We only care if the entire set of prisoners succeedes, so any failure is an immediate exit condition.
	 * For representation, each prisoner is assigned a number and the "name" stored in a draw is the prisoner's number.
	 * 
	 * The problem doesn't state a couple of interesting cases:
	 * Does the random strategy presume the prisoner has no memory of what was picked?
	 * In the optimal strategy, what happens if:
	 *	a drawer is self-referencing?
	 *	a set of drawers form a cycle?
	 *
	 * Maybe there's just too much stress for a prisoner to remember the drawers they have visited. :)
	 */

	public class Solve100Prisoners
	{
		public static void EvaluateSolutionStrategies()
		{
			const int DESIRED_ITERATIONS = 300000; //aka "several thousand isntances"

			int randomSuccesses = 0;
			int randomWithMemorySuccesses = 0;
			int optimalSuccesses = 0;
			int optimalWithMemorySuccesses = 0;

			for (var iter = 0; iter < DESIRED_ITERATIONS; iter++)
			{
				//to be fair, use the same drawers for each strategy.
				//otherwise we can't be certain we can't compare more than just success percentages.
				int[] drawers = Enumerable.Range(0, 100).Shuffle().ToArray();

				if (RunStrategy(drawers, RunPrisoner_RandomStrategy)) randomSuccesses++;
				if (RunStrategy(drawers, RunPrisoner_RandomWithMemoryStrategy)) randomWithMemorySuccesses++;
				if (RunStrategy(drawers, RunPrisoner_OptimalStrategy)) optimalSuccesses++;
				if (RunStrategy(drawers, RunPrisoner_OptimalWithMemoryStrategy)) optimalWithMemorySuccesses++;
			}

			//forcing iterations to a double or we get integer division that always results in 0.
			double randomPercentage = randomSuccesses / (double)DESIRED_ITERATIONS;
			double randomMemoryPercentage = randomWithMemorySuccesses / (double)DESIRED_ITERATIONS;
			double optimalPercentage = optimalSuccesses / (double)DESIRED_ITERATIONS;
			double optimalMemoryPercentage = optimalWithMemorySuccesses / (double)DESIRED_ITERATIONS;

			Console.WriteLine($"100 Prisoners Strategites over {DESIRED_ITERATIONS} simulations");
			Console.WriteLine($"Random: {randomSuccesses} or {randomPercentage:P}");
			Console.WriteLine($"Random w/Memory: {randomWithMemorySuccesses} or {randomMemoryPercentage:P}");
			Console.WriteLine($"Optimal: {optimalSuccesses} or {optimalPercentage:P}");
			Console.WriteLine($"Optimal w/Memory: {optimalWithMemorySuccesses} or {optimalMemoryPercentage:P}");
		}

		public static void EvaluateSolutionStrategy(string strategyName, Func<int, int[], bool> strategy, int iterations, bool displayOutput = true)
		{
			int successes = 0;

			for (var iter = 0; iter < iterations; iter++)
			{
				int[] drawers = Enumerable.Range(0, 100).Shuffle().ToArray();

				if (RunStrategy(drawers, strategy)) successes++;
			}

			if (displayOutput) Console.WriteLine($"{strategyName}: {successes} or {(successes / (double)iterations):P}");
		}


		public static bool RunStrategy(int[] drawers, Func<int, int[], bool> strategy)
		{
			bool success = true;

			for (int prisoner = 0; prisoner < 100; prisoner++)
			{
				success &= strategy(prisoner, drawers);

				if (!success) break;
			}

			return success;
		}


		public static bool RunPrisoner_RandomStrategy(int prisoner, int[] drawers)
		{
			bool found = false;
			int attempts = 0;
			Random rnd = new Random();

			do
			{
				attempts++;

				int drawer;

				//random drawer not already visited
				drawer = rnd.Next(drawers.Length);

				found = drawers[drawer] == prisoner;
			}
			while (!found && attempts < 50);

			return found;
		}

		public static bool RunPrisoner_RandomWithMemoryStrategy(int prisoner, int[] drawers)
		{
			bool found = false;
			int attempts = 0;
			Random rnd = new Random();

			HashSet<int> searches = new HashSet<int>();

			do
			{
				attempts++;

				int drawer;

				//random drawer not already visited
				do
				{
					drawer = rnd.Next(drawers.Length);
				}
				while (searches.Contains(drawer));

				searches.Add(drawer);

				found = drawers[drawer] == prisoner;
			}
			while (!found && attempts < 50);

			return found;
		}

		public static bool RunPrisoner_OptimalStrategy(int prisoner, int[] drawers)
		{
			bool found = false;
			int attempts = 0;
			Random rnd = new Random();

			HashSet<int> searches = new HashSet<int>();

			int drawer = prisoner;

			do
			{
				attempts++;

				found = drawers[drawer] == prisoner;

				//next drawer to try is the value contained in the current drawer
				drawer = drawers[drawer];
			}
			while(!found && attempts < 50);

			return found;
		}

		public static bool RunPrisoner_OptimalWithMemoryStrategy(int prisoner, int[] drawers)
		{
			bool found = false;
			int attempts = 0;
			Random rnd = new Random();

			HashSet<int> searches = new HashSet<int>();

			//each prisoner starts at their own position
			int drawer = prisoner;

			do
			{
				attempts++;

				searches.Add(drawer);

				found = drawers[drawer] == prisoner;

				if (!found)
				{ 
					//next drawer is current value
					drawer = drawers[drawer];

					//but if we've seen it, try to shift right
					while (searches.Contains(drawer))
					{
						//with wrap-around
						drawer = ++drawer % drawers.Length;
					}
				}
			}
			while (!found && attempts < 50);

			return found;
		}
	}
}
