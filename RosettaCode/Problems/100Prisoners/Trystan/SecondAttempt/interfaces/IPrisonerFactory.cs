namespace RosettaCode.Problems._100Prisoners.Trystan.SecondAttempt.Interfaces;

/// <summary>
///     Defines a factory for creating Prisoner instances with specified configuration parameters.
/// </summary>
public interface IPrisonerFactory
{
    /// <summary>
    ///     Creates a new Prisoner instance with the specified prisoner number and maximum search depth.
    /// </summary>
    /// <param name="prisonerNumber">
    ///     The unique number assigned to the prisoner. Must be a non-negative integer.
    /// </param>
    /// <param name="maxSearchDepth">
    ///     The maximum number of searches the prisoner is allowed to perform. Must be a non-negative integer.
    /// </param>
    /// <returns>
    ///     A Prisoner object initialized with the specified prisoner number and maximum search depth.
    /// </returns>
    public Prisoner CreatePrisoner(uint prisonerNumber, uint maxSearchDepth);
}