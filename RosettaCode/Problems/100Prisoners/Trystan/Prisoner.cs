namespace RosettaCode.Problems._100Prisoners.Trystan
{
    public abstract class Prisoner
    {

        /*
         *
         * This class represents a prisoner in the 100 prisoners problem.
         * Each prisoner has a unique number
         * Each prisoner has a guessing strategy represented by _guessFunction
         */

        protected int PrisonerNumber { get; }

        protected static Func<int,int, int, int, int[], bool> NeverMatchPredicate = (currentDepth,maxDepth, prisonerNumber, _, drawers) => false;

        private readonly Func<int, int, int, int, int[], bool> _guessFunction = Prisoner.NeverMatchPredicate;


        // execute the guess function for this prisoner, with the given drawers and max depth
        public bool SearchForCard(int maxDepth,int[] drawers)
        {
            return _guessFunction(1, maxDepth, this.PrisonerNumber, 0, drawers);
        }

        // constructor for the prisoner, takes a prisoner number and a guessing strategy
        protected Prisoner(int prisonerNumber, Func<int, int, int, int, int[], bool> predicate)
        {
            PrisonerNumber = prisonerNumber;
            
            _guessFunction = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

    }
}
