namespace RosettaCode.Problems.SumTo100.Trystan.Attempt2;

public class SumTo100
{
    public readonly Dictionary<int,SumTo100Result> Results = new Dictionary<int, SumTo100Result>();

    public readonly SumTo100Parameters Parameters;

    public SumTo100(SumTo100Parameters parameters)
    {
        Parameters = parameters;

        int d = (int)Math.Pow(parameters.KeyBase, parameters.Digits.Length - 1);
        for (int i = 0; i < (int)Math.Pow(parameters.KeyBase, parameters.Digits.Length); i++)
        {
            
            int t = i / d;
            if (t != 0)
            {
                // only subtraction is allowed as an operator before the digits
                if (parameters.UseSubtraction is false) continue;
                if (parameters.UseAddition)
                {
                    if (t != 2) continue;
                } else if (t != 1) continue;
            }

            if (!Results.ContainsKey(i)) Results.Add(i, new SumTo100Result(parameters, i));
        }
    }

    public SumTo100() : this(new SumTo100Parameters())
    {
    }


    public List<SumTo100Result> GetAllSumsThatEqual100()
    {
        return Results.Values.Where(result => result.Result == 100).ToList();
    }
    public List<SumTo100Result> GetAllSumsThatEqual(int x)
    {
        return Results.Values.Where(result => result.Result == x).ToList();
    }



    /// <summary>
    /// Returns the number of sums in the collection that are equal to the specified value.
    /// </summary>
    /// <param name="x">The value to compare against the sums in the collection.</param>
    /// <returns>The number of sums that are equal to the specified value. Returns 0 if no sums match.</returns>
    public int GetNumberOfSumsThatEqual(int x)
    {
        return Results.Count(kvp => kvp.Value.Result == x);
    }

    /// <summary>
    /// Finds the smallest positive integer that cannot be represented as the sum of available numbers according to the
    /// current implementation of GetNumberOfSumsThatEqual.
    /// </summary>
    /// <remarks>This method relies on the behavior of GetNumberOfSumsThatEqual to determine which numbers can
    /// be represented as sums. The result depends on the logic and data used by GetNumberOfSumsThatEqual.</remarks>
    /// <returns>The first positive integer for which GetNumberOfSumsThatEqual returns 0, indicating it cannot be formed as a
    /// sum.</returns>
    public int GetFirstPositiveNumberThatCannotBeSummed()
    {
        int i = 1;
        while (GetNumberOfSumsThatEqual(i) > 0)
        {
            i++;
        }

        return i;
    }

    /// <summary>
    /// Returns the sum value that appears most frequently in the collection of stored sums.
    /// </summary>
    /// <returns>The sum value with the highest occurrence count. If multiple sums share the highest count, one of them is
    /// returned.</returns>
    public int GetMostCommonSum()
    {
        return Results.GroupBy(kvp => kvp.Value.Result)
            .OrderByDescending(g => g.Count())
            .First().Key;
    }

    /// <summary>
    /// Returns the ten largest unique sum values from the collection.
    /// </summary>
    /// <remarks>Duplicate sum values are removed before determining the top ten results. The returned array
    /// is sorted in descending order.</remarks>
    /// <returns>An array of up to ten integers representing the largest unique sums, ordered from largest to smallest. The array
    /// may contain fewer than ten elements if there are not enough unique sums.</returns>
    public int[] GetTopTenLargestSums()
    {
        //I actually had to use my brain and fix this answer :) (I wanted to remove repeats)
        return Results.GroupBy(kvp => kvp.Value.Result)
            .OrderByDescending(g => g.Key)
            .Take(10)
            .Select(g => g.Key)
            .ToArray();
    }

}