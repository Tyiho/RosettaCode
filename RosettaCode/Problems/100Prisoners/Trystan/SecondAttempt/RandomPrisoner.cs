namespace RosettaCode.Problems._100Prisoners.Trystan.SecondAttempt;

/// <summary>
///     Represents a prisoner that searches for their number using a random strategy within the allowed search depth.
/// </summary>
/// <remarks>
///     The random search strategy does not follow any deterministic pattern and may result in lower success
///     rates compared to more systematic approaches. This implementation is primarily intended for demonstration or
///     comparison purposes in scenarios such as the '100 prisoners problem.'
/// </remarks>
/// <param name="prisonerNumber">
///     The unique number assigned to the prisoner. This value is used as the target to search for in the drawers.
/// </param>
/// <param name="maxSearchDepth">
///     The maximum number of drawers the prisoner is allowed to open during the search. Must be a positive integer.
/// </param>
public class RandomPrisoner(uint prisonerNumber, uint maxSearchDepth) : Prisoner(prisonerNumber, maxSearchDepth)
{

    /// <summary>
    ///     Performs a random search to determine whether the target value can be found within the allowed search depth
    ///     using the specified sequence of drawers.
    /// </summary>
    /// <remarks>
    ///     This method is not very efficient and is generally not recommended for solving the '100 prisoners problem' due to its random nature.
    ///     The search does not follow a specific sequence and may require multiple attempts to find the target value, especially as the search depth increases.
    ///     It is included here for illustrative purposes to demonstrate an alternative approach to searching through the drawers.
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

        return drawers[Random.Shared.Next(drawers.Length)] == PrisonerNumber 
               || SearchFunction(currentDepth + 1, drawers);
    }
}

