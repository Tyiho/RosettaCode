namespace RosettaCode.Problems._100Prisoners.Trystan.SecondAttempt;

/// <summary>
///     Represents an abstract base class for a prisoner participating in a search scenario, encapsulating the prisoner's
///     unique number and search constraints.
/// </summary>
/// <param name="prisonerNumber">
///     The unique number assigned to the prisoner. This value identifies the card the prisoner must find during the search.
/// </param>
/// <param name="maxSearchDepth">
///     The maximum number of search levels or steps the prisoner is allowed to perform. Must be a non-negative integer.
/// </param>
public abstract class Prisoner(uint prisonerNumber, uint maxSearchDepth)
{
    /// <summary>
    ///     Gets the unique number assigned to the prisoner that the prisoner will need to find.
    /// </summary>
    public uint PrisonerNumber { get; } = prisonerNumber;

    /// <summary>
    ///     Gets the maximum depth, in levels, that the search operation will traverse.
    /// </summary>
    public uint MaxSearchDepth { get; } = maxSearchDepth;

    /// <summary>
    ///     Performs a search operation based on the specified depth and the provided array of drawers.
    /// </summary>
    /// <param name="currentDepth">
    ///     The current depth of the search operation. Must be a non-negative integer.
    /// </param>
    /// <param name="drawers">
    ///     An array of unsigned integers representing the drawers to be considered in the search. Cannot be null.
    /// </param>
    /// <returns>
    ///     true if the search operation succeeds; otherwise, false.
    /// </returns>
    public abstract bool SearchFunction(uint currentDepth, uint[] drawers);

    /// <summary>
    ///     Searches the specified drawers for a card and indicates whether a card is found.
    /// </summary>
    /// <param name="drawers">
    ///     An array of unsigned integers representing the drawers to search. Each element corresponds to a drawer
    ///     identifier or value to be checked.
    /// </param>
    /// <returns>
    ///     true if a card is found in any of the specified drawers; otherwise, false.
    /// </returns>
    public bool SearchForCard(uint[] drawers) => SearchFunction(0, drawers);
}
