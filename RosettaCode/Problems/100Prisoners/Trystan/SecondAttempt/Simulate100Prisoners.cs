using RosettaCode.Problems._100Prisoners.Trystan.SecondAttempt.Interfaces;

namespace RosettaCode.Problems._100Prisoners.Trystan.SecondAttempt;

public static class Simulate100Prisoners
{
    /// <summary>
    ///     Setups up a given scenario for the 100 prisoners problem.
    ///     It creates a random drawer configuration and a set of prisoners based on the provided factory.
    /// </summary>
    /// <typeparam name="TPrisonerFactory">
    ///     The factory type that provides the strategy for creating prisoner instances.
    ///     Must implement <see cref="IPrisonerFactory"/> and have a parameterless constructor.
    /// </typeparam>
    /// <param name="numberOfPrisoners">
    ///     number of prisoners and drawers in the scenario.
    /// </param>
    /// <param name="maxSearchDepth">
    ///     how many drawers each prisoner is allowed to search before they fail.
    /// </param>
    /// <param name="drawers">
    ///     output parameter that will contain the random drawer configuration after the method is called.
    /// </param>
    /// <param name="prisoners">
    ///     output parameter that will contain the array of prisoners created by the provided factory after the method is called.
    /// </param>
    public static void SetupScenario<TPrisonerFactory>(uint numberOfPrisoners, uint maxSearchDepth, out uint[] drawers, out Prisoner[] prisoners) where TPrisonerFactory : IPrisonerFactory, new()
    {
        drawers = new uint[numberOfPrisoners];
        for (uint i = 0; i < numberOfPrisoners; i++)
        {
            drawers[i] = i;
        }

        //shuffle the drawers
        drawers = drawers.AsEnumerable().Shuffle().ToArray();

        //create prisoners using the provided factory
        IPrisonerFactory prisonerFactory = new TPrisonerFactory();

        prisoners = new Prisoner[numberOfPrisoners];
        for (uint i = 0; i < numberOfPrisoners; i++)
        {
            prisoners[i] = prisonerFactory.CreatePrisoner(i, maxSearchDepth);
        }
    }

    /// <summary>
    ///     Simulates the given number of prisoners and drawers scenario using the specified prisoner factory to create the prisoners.
    /// </summary>
    /// <remarks>
    ///     This method is useful for evaluating different prisoner strategies by specifying various
    ///     implementations of <see cref="IPrisonerFactory"/>. The simulation is performed synchronously and the result is
    ///     returned as a completed task.
    /// </remarks>
    /// <typeparam name="TPrisonerFactory">
    ///     The factory type that provides the strategy for creating prisoner instances.
    ///     Must implement <see cref="IPrisonerFactory"/> and have a parameterless constructor.
    /// </typeparam>
    /// <param name="numberOfPrisoners">
    ///     number of prisoners and drawers in the scenario.
    /// </param>
    /// <param name="maxSearchDepth">
    ///     how many drawers each prisoner is allowed to search before they fail.
    /// </param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result is <see langword="true"/> if all prisoners
    ///     successfully find their own card within the allowed number of searches; otherwise, <see langword="false"/>.
    /// </returns>
    public static Task<bool> SimulatePrisoners<TPrisonerFactory>(uint numberOfPrisoners, uint maxSearchDepth) where TPrisonerFactory : IPrisonerFactory, new()
    {
        SetupScenario<TPrisonerFactory>(numberOfPrisoners, maxSearchDepth, out uint[] drawers, out Prisoner[] prisoners);
        return Task.FromResult(prisoners.All(p => p.SearchForCard(drawers)));
    }

    /// <summary>
    ///     Simulates the 100 prisoners problem 10,000 times using the specified prisoner factory to create the prisoners.
    /// </summary>
    /// <typeparam name="TPrisonerFactory">
    ///     The factory type that provides the strategy for creating prisoner instances.
    ///     Must implement <see cref="IPrisonerFactory"/> and have a parameterless constructor.
    /// </typeparam>
    /// <returns>
    ///     A float that represents the success rate of the prisoners' strategy over 10,000 simulations.
    ///     The value is between 0 and 1, where 1 means all simulations were successful and 0 means none were successful.
    /// </returns>
    public static float Simulate100Prisoners10000Times<TPrisonerFactory>() where TPrisonerFactory : IPrisonerFactory, new()
    {
        uint numberOfPrisoners = 100;
        uint maxSearchDepth = 50;

        int successes = 0;
        int numberOfTrials = 10000;


        Task<bool>[] simulations = new Task<bool>[numberOfTrials];

        for (uint i = 0; i < numberOfTrials; i++)
        {
            simulations[i] = SimulatePrisoners<TPrisonerFactory>(numberOfPrisoners, maxSearchDepth);
        }

        Task.WaitAll(simulations);

        successes = simulations.Count((task) => task.Result);

        return (float)successes / numberOfTrials;
    }

    /// <summary>
    ///     Simulates the 100 prisoners problem using a random strategy, repeating the simulation 10,000 times.
    /// </summary>
    /// <remarks>
    ///     This method uses a random selection strategy for each prisoner in the simulation. The result
    ///     provides an empirical estimate of the success rate for the random approach over 10,000 trials.
    /// </remarks>
    /// <returns>
    ///     A float that represents the success rate of the prisoners' strategy over 10,000 simulations.
    ///     The value is between 0 and 1, where 1 means all simulations were successful and 0 means none were successful.
    /// </returns>
    public static float Simulate100RandomPrisoners10000Times()
    {
        return Simulate100Prisoners10000Times<RandomPrisonerFactory>();
    }

    /// <summary>
    ///     Simulates the 100 prisoners problem using an optimal strategy, repeating the simulation 10,000 times.
    /// </summary>
    /// <remarks>
    ///     This method uses a random selection strategy for each prisoner in the simulation. The result
    ///     provides an empirical estimate of the success rate for the random approach over 10,000 trials.
    /// </remarks>
    /// <returns>
    ///     A float that represents the success rate of the prisoners' strategy over 10,000 simulations.
    ///     The value is between 0 and 1, where 1 means all simulations were successful and 0 means none were successful.
    /// </returns>
    public static float Simulate100OptimalPrisoners10000Times()
    {
        return Simulate100Prisoners10000Times<OptimalPrisonerFactory>();
    }

}