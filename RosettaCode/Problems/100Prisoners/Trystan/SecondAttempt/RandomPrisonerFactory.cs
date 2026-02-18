using RosettaCode.Problems._100Prisoners.Trystan.SecondAttempt.Interfaces;

namespace RosettaCode.Problems._100Prisoners.Trystan.SecondAttempt;

/// <summary>
///     Provides a factory for creating prisoners that use a random search strategy.
/// </summary>
public class RandomPrisonerFactory : IPrisonerFactory
{

    /// <summary>
    ///     Creates a new prisoner instance with the specified prisoner number and maximum search depth that uses the random search strategy.
    /// </summary>
    /// <param name="prisonerNumber">
    ///     The unique number assigned to the prisoner. Must be a non-negative integer.
    /// </param>
    /// <param name="maxSearchDepth">
    ///     The maximum number of boxes the prisoner is allowed to search. Must be a non-negative integer.
    /// </param>
    /// <returns>
    ///     A new instance of a prisoner configured with the specified number and search depth.
    /// </returns>
    public Prisoner CreatePrisoner(uint prisonerNumber, uint maxSearchDepth)
    {
        return new RandomPrisoner(prisonerNumber, maxSearchDepth);
    }
}