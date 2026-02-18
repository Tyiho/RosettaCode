using System;
using System.Collections.Generic;
using System.Text;

namespace RosettaCode.Problems._100Prisoners.Trystan.FirstAttempt;
public class OptimalPrisoner : Prisoner
{

    /*
     * This prisoner will optimally guess by first opening the drawer with their own number,
     * then the drawer with the number of the card they just found,
     * and so on until they find their own card or reach the maximum depth.
     */
    private static bool _optimalGuessFunction(int currentDepth, int maxDepth, int prisonerNumber, int lastNumber, int[] drawers)
    {
        if(lastNumber == 0) lastNumber = prisonerNumber;

        // check if we've reached the maximum depth of guesses
        if (currentDepth > maxDepth) return false;

        /*
         * search the drawer corresponding to the current prisoner number,
         * if not found, continue searching the drawer corresponding to the card number found in the previous drawer
         */

        return drawers[lastNumber - 1] == prisonerNumber || _optimalGuessFunction(currentDepth + 1, maxDepth, prisonerNumber, drawers[lastNumber - 1], drawers);
    }

    public OptimalPrisoner(int prisonerNumber) : base(prisonerNumber, _optimalGuessFunction) { }
}
