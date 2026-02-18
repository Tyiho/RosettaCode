namespace RosettaCode.Problems._100Prisoners.Trystan.SecondAttempt;

/// <summary>
///     Represents a prisoner that uses the optimal search strategy for locating their own number within a set of drawers,
///     as defined in the '100 prisoners problem'.
/// </summary>
/// <remarks>
///     The optimal search strategy implemented by this class follows the cycle-based approach commonly used
///     to maximize the probability of success in the '100 prisoners problem'. Each instance maintains its own search state
///     and is intended for use in simulations or analyses of this problem.
/// </remarks>
/// <param name="prisonerNumber">
///     The unique number assigned to the prisoner. This value determines the starting point for the search sequence.
/// </param>
/// <param name="maxSearchDepth">
///     The maximum number of drawers the prisoner is allowed to open during the search. Must be a positive integer.
/// </param>
public class OptimalPrisoner(uint prisonerNumber, uint maxSearchDepth) : Prisoner(prisonerNumber, maxSearchDepth)
{
    private uint _drawerToSearch = prisonerNumber;

    /// <summary>
    ///     Performs a recursive search to determine whether the target value can be found within the allowed search depth
    ///     using the specified sequence of drawers.
    /// </summary>
    /// <remarks>
    ///     This method is typically used in scenarios where a search must be performed with a limited
    ///     number of steps, such as in the context of the '100 prisoners problem'. The search follows a specific sequence
    ///     determined by the values in the drawers array. The method is recursive and may have performance implications for
    ///     large search depths.
    /// </remarks>
    /// <param name="currentDepth">
    ///     The current depth of the search. Must be less than the maximum allowed search depth.
    /// </param>
    /// <param name="drawers">
    ///     An integer array representing the contents of each drawer. Each element corresponds to a drawer and contains a value to
    ///     be searched.
    /// </param>
    /// <returns>
    ///     true if the target value is found within the allowed search depth; otherwise, false.
    /// </returns>
    public override bool SearchFunction(uint currentDepth, uint[] drawers)
    {
        if (currentDepth >= MaxSearchDepth) return false;

        if (drawers[_drawerToSearch] == PrisonerNumber) return true;

        _drawerToSearch = drawers[_drawerToSearch];

        return SearchFunction(currentDepth + 1, drawers);
    }
}