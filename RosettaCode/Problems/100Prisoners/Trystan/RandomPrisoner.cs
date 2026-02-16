using System;
using System.Collections.Generic;
using System.Text;

namespace RosettaCode.Problems._100Prisoners.Trystan
{
    public class RandomPrisoner : Prisoner
    {

        /*
         * This prisoner will randomly select drawers to open
         */

        private static bool _randomGuessFunction(int currentDepth, int maxDepth, int prisonerNumber, int _, int[] drawers)
        {
            // check if we've reached the maximum depth of guesses
            if (currentDepth > maxDepth) return false;

            // randomly select a drawer to open
            return drawers[Random.Shared.Next(drawers.Length)] == prisonerNumber || _randomGuessFunction(currentDepth + 1, maxDepth, prisonerNumber, _, drawers);
        }

        public RandomPrisoner(int prisonerNumber) : base(prisonerNumber, _randomGuessFunction) { }
    }
}
