using System;
using System.Collections.Generic;
using System.Text;

namespace RosettaCode.Problems._100Prisoners.Trystan
{
    public static class _100PrisonersTrystan
    {

        /* autocomplete helped a lot with this function.
         * I don't think I would've known to use Activator.CreateInstance without it.
         * Nor would have know how to validate the prisonerType parameter to ensure it is a non-abstract type that inherits from Prisoner.
         * Well... I probably could've figured it out with some searching, but it was nice to have it suggested to me.
         *
         * This function sets up the scenario by creating the specified number of drawers and prisoners.
         * It randomly assigns card numbers to the drawers and prisoner numbers to the prisoners, ensuring that each card number and prisoner number is unique.
         * The function returns a tuple containing the array of drawers and the array of prisoners.
         */
        public static Tuple<int[], Prisoner[]> SetupScenario(int numberOfPrisoners, Type prisonerType)
        {
            // Validate the prisonerType parameter to ensure it is a non-abstract type that inherits from Prisoner
            if (prisonerType is not { IsAbstract: false } || !typeof(Prisoner).IsAssignableFrom(prisonerType))
            {
                throw new ArgumentException("prisonerType must be a non-abstract type that inherits from Prisoner");
            }

            // Create an array of drawers and a list of card numbers to assign to the drawers
            int[] drawers = new int[numberOfPrisoners];
            List<int> cardNumbers = new List<int>(numberOfPrisoners);
            for (int i = 0; i < numberOfPrisoners; i++)
            {
                cardNumbers.Add(i + 1);
            }
            for (int i = 0; i < numberOfPrisoners; i++)
            {
                // Randomly select a card number from the list and assign it to the current drawer, then remove it from the list to ensure uniqueness
                int cardIndex = Random.Shared.Next(0, cardNumbers.Count);
                drawers[i] = cardNumbers[cardIndex];
                cardNumbers.RemoveAt(cardIndex);
            }

            // Create an array of prisoners and a list of card numbers to assign to the prisoners
            Prisoner[] prisoners = new Prisoner[numberOfPrisoners];
            for (int i = 0; i < numberOfPrisoners; i++)
            {
                cardNumbers.Add(i + 1);
            }
            for (int i = 0; i < numberOfPrisoners; i++)
            {
                // Use Activator.CreateInstance to create an instance of the specified prisonerType, passing in a random prisoner number as a parameter
                int cardIndex = Random.Shared.Next(0, cardNumbers.Count);
                Prisoner? prisoner = (Prisoner?)Activator.CreateInstance(prisonerType, cardNumbers[cardIndex]);
                prisoners[i] = prisoner ?? throw new InvalidOperationException($"Failed to create instance of {prisonerType.Name} for prisoner number {i + 1}");
                //remove the assigned prisoner number from the list to ensure uniqueness
                cardNumbers.RemoveAt(cardIndex);
            }

            //return a tuple containing the array of drawers and the array of prisoners
            return new(drawers, prisoners);
        }

        /*
         *  This method Starts a new task that simulates a given number of RandomPrisoners searching for their cards in the drawers.
         *
         *  It takes in a number of prisoners and a maximum search depth, which determines how many drawers each prisoner will search through.
         *  It returns a boolean task that indicates whether all prisoners were able to find their cards within the specified search depth.
         */
        public static Task<bool> Simulate100RandomPrisoners(int numberOfPrisoners, int maxSearchDepth)
        {
            return Task<bool>.Factory.StartNew(() =>
            {
                Tuple<int[], Prisoner[]> scenario = SetupScenario(numberOfPrisoners, typeof(RandomPrisoner));
                int[] drawers = scenario.Item1;
                Prisoner[] prisoners = scenario.Item2;

                foreach(Prisoner prisoner in prisoners)
                {
                    if (!prisoner.SearchForCard(maxSearchDepth, drawers)) return false;
                }
                return true;
            });

        }

        /*
         *  This method Starts a new task that simulates a given nymber of RandomPrisoners searching for their cards in the drawers.
         *
         *  It takes in a number of prisoners and a maximum search depth, which determines how many drawers each prisoner will search through.
         * It returns a boolean task that indicates whether all prisoners were able to find their cards within the specified search depth.
         */
        public static Task<bool> Simulate100OptimalPrisoners(int numberOfPrisoners, int maxSearchDepth)
        {
            return Task<bool>.Factory.StartNew(() =>
            {
                Tuple<int[], Prisoner[]> scenario = SetupScenario(numberOfPrisoners, typeof(OptimalPrisoner));
                int[] drawers = scenario.Item1;
                Prisoner[] prisoners = scenario.Item2;

                foreach (Prisoner prisoner in prisoners)
                {
                    if (!prisoner.SearchForCard(maxSearchDepth, drawers)) return false;
                }
                return true;
            });
        }

        /*
         * Simulates 10000 attempts of the random prisoners strategy and returns the success rate as a float between 0 and 1.
         */
        public static float Simulate10000AttemptsOfRandomPrisoners()
        {
            int successes = 0;
            int attempts = 10000;

            Task<bool>[] tasks = new Task<bool>[attempts];

            for (int i = 0; i < attempts; i++)
            {
                tasks[i] = Simulate100RandomPrisoners(100, 50);
            }

            Task.WaitAll(tasks);

            foreach (Task<bool> task in tasks)
            {
                if (task.Result)
                {
                    successes++;
                }
            }

            return (float)successes / attempts;
        }

        /*
         * Simulates 10000 attempts of the optimal prisoners strategy and returns the success rate as a float between 0 and 1.
         */
        public static float Simulate10000AttemptsOfOptimalPrisoners()
        {
            int successes = 0;
            int attempts = 10000;

            Task<bool>[] tasks = new Task<bool>[attempts];

            for (int i = 0; i < attempts; i++)
            {
                tasks[i] = Simulate100OptimalPrisoners(100, 50);
            }

            Task.WaitAll(tasks);

            foreach (Task<bool> task in tasks)
            {
                if (task.Result)
                {
                    successes++;
                }
            }

            return (float)successes / attempts;
        }

    }
}
