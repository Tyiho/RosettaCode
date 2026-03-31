namespace RosettaCode.Problems.SumTo100.Trystan.Attempt2;

public readonly struct SumTo100Parameters
{
    public readonly int[] Digits;
    public readonly bool UseAddition;
    public readonly bool UseSubtraction;
    public readonly bool UseMultiplication;
    public readonly bool UseDivision;
    public readonly byte KeyBase {
        get {             
            byte keyBase = 1;
            if (UseAddition) keyBase++;
            if (UseSubtraction) keyBase++;
            if (UseMultiplication) keyBase++;
            if (UseDivision) keyBase++;
            return keyBase;
        }
    }

    public SumTo100Parameters(int[] digits, bool useAddition, bool useSubtraction, bool useMultiplication, bool useDivision)
    {
        ArgumentNullException.ThrowIfNull(digits);
        Digits = digits;
        UseAddition = useAddition;
        UseSubtraction = useSubtraction;
        UseMultiplication = useMultiplication;
        UseDivision = useDivision;
    }
    public SumTo100Parameters(int[] digits) : this(digits, true, true, false, false) { }

    public SumTo100Parameters() : this([ 1, 2, 3, 4, 5, 6, 7, 8, 9 ]) { }

}