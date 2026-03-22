using System;
using System.Collections.Generic;
using System.Text;

namespace RosettaCode.Problems.SumTo100.Trystan;

public class SumTo100
{
    public Dictionary<Int16, int> _sums = new Dictionary<Int16, int>();
    private readonly short[] _powersOf3 = [1, 3, 9, 27, 81, 243, 729, 2187, 6561];

    /// <summary>
    ///     Recursively generates and stores all possible sums by applying combine, add, and subtract operations to the
    ///     digits 0 through 9, based on the specified operation sequence key.
    /// </summary>
    /// <remarks>This method explores all possible combinations of operations for the digits 0 through 9,
    /// using a base-3 encoding in the key to represent the operation sequence. Results are cached to avoid redundant
    /// calculations. Intended for internal use within the class.</remarks>
    /// <param name="depth">The current recursion depth, representing the position in the sequence of operations.</param>
    /// <param name="key">A short integer encoding the sequence of operations (combine, add, subtract) to apply to the digits.
    /// Each digit in the key when converted to base 3 represents an operation at a specific position (from right to left).</param>
    private void FindAllSumsRecursion(int depth, short key)
    {
        if (depth > 8)
        {

            if (_sums.ContainsKey(key)) return;

            int sum = 0;

            Stack<int> currentDigits = new Stack<int>([0,1,2,3,4,5,6,7,8,9]);


            var currentKey = key;

            for (int i = 0; i < 9; i++)
            {
                switch (currentKey % 3)
                {
                    case 0: //combine (ex: 2,34 => 234)
                        int a = currentDigits.Pop();
                        int b = currentDigits.Pop();
                        var aa = a;
                        do
                        {
                            b *= 10;
                            aa /= 10;

                        } while (aa != 0);

                        currentDigits.Push(a + b);
                        break;
                    case 1: //add
                        sum += currentDigits.Pop();
                        break;
                    case 2: //subtract
                        sum -= currentDigits.Pop();
                        break;
                }

                currentKey /= 3;
            }

            if (currentDigits.Count == 1)
            {
                sum += currentDigits.Pop();
            }

            _sums.Add(key, sum);

            return;
        }

        //all combinations of 0, 1, and 2 for the 9 digits (combine, add, subtract)
        for (int i = 0; i < 3; i++)
        {
            var currentKey = key;
            switch (i)
            {
                case 0:
                    currentKey *= 3;//_powersOf3[depth];
                    //currentKey += 0;

                    if (depth == 0)
                    {
                        //The first operation we do not include {nothing} because it is redundant (ex: +2 is the same as 2)
                        break;
                    }

                    break;
                case 1:
                    //key *= _powersOf3[depth];
                    var dep = depth;
                    currentKey *= 3;
                    currentKey += 1;
                    break;
                case 2:
                    //key *= _powersOf3[depth];
                    currentKey *= 3;
                    currentKey += 2;
                    break;
            }

            FindAllSumsRecursion(depth + 1, currentKey);

        }
    }
    /// <summary>
    /// Finds all expressions using the digits 1 through 9 in order, combined with addition, subtraction, or
    /// concatenation, that evaluate to 100.
    /// </summary>
    /// <remarks>Each string in the returned list is a mathematical expression formed by inserting '+', '-',
    /// or no operator (concatenation) between the digits 1 to 9, such that the resulting expression evaluates to 100.
    /// The expressions are constructed in left-to-right order, and all possible combinations are considered.</remarks>
    /// <returns>A list of strings, each representing a valid expression that uses the digits 1 through 9 in sequence with
    /// addition, subtraction, or concatenation to total 100. The list is empty if no such expressions are found.</returns>
    public List<string> GetAllSumsThatEqual100()
    {
        List<string> results = _sums.Where(kvp => kvp.Value == 100)
            .Select(kvp =>
            {
                StringBuilder sb = new StringBuilder("9");
                short key = kvp.Key;
                for (int i = 0; i < 9; i++)
                {

                    switch (key % 3)
                    {
                        case 0: //combine
                            //sb.Insert(0, "");
                            break;
                        case 1: //add
                            sb.Insert(0, "+");
                            break;
                        case 2: //subtract
                            sb.Insert(0, "-");
                            break;
                    }

                    key /= 3;

                    var t = 9 - i - 1;
                    if (t != 0)
                    {
                        sb.Insert(0, t.ToString());
                    }
                    else
                    {
                        if (sb[0] == '+')
                        {
                            sb.Remove(0, 1);
                        }
                    }
                }

                //return kvp.Key.ToString();
                return sb.ToString();
            })
            .ToList();
        return results;
    }

    /// <summary>
    /// Returns all string expressions representing combinations of the digits 1 through 9, using addition and
    /// subtraction, that evaluate to the specified sum.
    /// </summary>
    /// <remarks>Each returned string represents an expression formed by inserting '+', '-', or no operator
    /// between the digits 1 through 9. The method searches for all possible combinations that result in the specified
    /// sum.</remarks>
    /// <param name="x">The target sum to match. Only expressions that evaluate to this value are returned.</param>
    /// <returns>A list of string expressions, each representing a valid combination of the digits 1 through 9 with addition and
    /// subtraction operators that equals the specified sum. The list is empty if no such expressions exist.</returns>
    public List<string> GetAllSumsThatEqual(int x)
    {
        List<string> results = _sums.Where(kvp => kvp.Value == x)
            .Select(kvp =>
            {
                StringBuilder sb = new StringBuilder("9");
                short key = kvp.Key;
                for (int i = 0; i < 9; i++)
                {

                    switch (key % 3)
                    {
                        case 0: //combine
                            //sb.Insert(0, "");
                            break;
                        case 1: //add
                            sb.Insert(0, "+");
                            break;
                        case 2: //subtract
                            sb.Insert(0, "-");
                            break;
                    }

                    key /= 3;

                    var t = 9 - i - 1;
                    if (t != 0)
                    {
                        sb.Insert(0, t.ToString());
                    }
                    else
                    {
                        if (sb[0] == '+')
                        {
                            sb.Remove(0, 1);
                        }
                    }
                }
                
                return sb.ToString();
            })
            .ToList();
        return results;

    }
    
    /// <summary>
    /// Returns the number of sums in the collection that are equal to the specified value.
    /// </summary>
    /// <param name="x">The value to compare against the sums in the collection.</param>
    /// <returns>The number of sums that are equal to the specified value. Returns 0 if no sums match.</returns>
    public int GetNumberOfSumsThatEqual(int x)
    {
        return _sums.Count(kvp => kvp.Value == x);
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
        return _sums.GroupBy(kvp => kvp.Value)
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
        return _sums.GroupBy(kvp => kvp.Value)
            .OrderByDescending(g => g.Key)
            .Take(10)
            .Select(g => g.Key)
            .ToArray();
    }

    public SumTo100()
    {
       FindAllSumsRecursion(0, 0);
    }
}