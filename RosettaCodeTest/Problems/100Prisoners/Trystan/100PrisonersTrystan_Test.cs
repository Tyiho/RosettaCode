using System;
using System.Collections.Generic;
using System.Text;
using RosettaCode.Problems._100Doors.Trystan;
using RosettaCode.Problems._100Prisoners.Trystan;

namespace RosettaCodeTest.Problems._100Prisoners.Trystan
{
    [TestClass]
    public class _100PrisonersTrystan_Test
    {
        [TestMethod]
        public void Test_SimulateOptimalPrisoners()
        {
            float result = _100PrisonersTrystan.Simulate10000AttemptsOfOptimalPrisoners();

            Console.WriteLine($"Expected success rate to be ~ 31%, this time we got {result * 100}%");

            Assert.IsGreaterThan<float>(0.29f, result);
        }

        [TestMethod]
        public void Test_SimulateRandomPrisoners()
        {
            float result = _100PrisonersTrystan.Simulate10000AttemptsOfRandomPrisoners();

            Console.WriteLine($"Expected success rate to be above 30%, but got {result * 100}%");


            /*
             * odds of all 100 prisoners find their card by randomly selecting drawers is:
             * insanely small
             * 4.5608153331*10^(-41) ≈ 0 (if I've done my math right)
             *
             */

            Assert.AreEqual(0,result);
        }

        [TestMethod]
        public void Test_SetupScenario()
        {
            Tuple<int[], Prisoner[]> scenario = _100PrisonersTrystan.SetupScenario(100, typeof(OptimalPrisoner));

            CollectionAssert.AllItemsAreUnique(scenario.Item1);
            CollectionAssert.AllItemsAreUnique(scenario.Item2);
        }

        [TestMethod]
        public void Test_OptimalPrisonerSearch()
        {
            int[] drawers = new int[] { 8, 2, 10, 4, 3, 1, 7, 9, 5, 6 };
            OptimalPrisoner prisoner = new OptimalPrisoner(Random.Shared.Next(1,11));
            bool result = prisoner.SearchForCard(10, drawers);
            //this should be true via the pigeonhole principle
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void Test_RandomPrisonerSearch()
        {
            int[] drawers = new int[] { 8, 2, 10, 4, 3, 1, 7, 9, 5, 6 };
            RandomPrisoner prisoner = new RandomPrisoner(Random.Shared.Next(1,11));
            bool result = prisoner.SearchForCard(10, drawers);
            // we can't assert anything here since the result is random, but we can at least run the function to ensure it doesn't throw any exceptions
            Console.WriteLine(result);
        }
    }
}
