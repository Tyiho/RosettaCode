using RosettaCode.Problems.SumTo100.Trystan.Attempt2;

namespace RosettaCodeTest.Problems.SumTo100.Trystan.Attempt2;

[TestClass]
public class SumTo100Test
{
    [TestMethod]
    public void GetAllSumsThatEqual100_ResultLength_IsGreaterThan()
    {
        var sumTo100 = new RosettaCode.Problems.SumTo100.Trystan.Attempt2.SumTo100();

        List<SumTo100Result> strings = sumTo100.GetAllSumsThatEqual100();

        foreach (var s in strings)
        {
            Console.WriteLine(s);
        }

        Assert.IsGreaterThan(0, strings.Count);
    }

    [TestMethod]
    public void GetFirstPositiveNumberThatCannotBeSummed_Result_Equals()
    {
        var sumTo100 = new RosettaCode.Problems.SumTo100.Trystan.Attempt2.SumTo100();

        Assert.AreEqual(211, sumTo100.GetFirstPositiveNumberThatCannotBeSummed());
    }

    [TestMethod]
    public void GetMostCommonSum_Result_Equals()
    {
        var sumTo100 = new RosettaCode.Problems.SumTo100.Trystan.Attempt2.SumTo100();

        // The most common sum is 9, which occurs 46 times
        Console.WriteLine(sumTo100.GetNumberOfSumsThatEqual(9));
        // This ties with -9, which also occurs 46 times
        Console.WriteLine(sumTo100.GetNumberOfSumsThatEqual(-9));

        List<SumTo100Result> strings1 = sumTo100.GetAllSumsThatEqual(9);
        Console.WriteLine("All sums that equal 9:");
        foreach (var s in strings1)
        {
            Console.WriteLine(s);
        }

        Console.WriteLine();
        Console.WriteLine();

        List<SumTo100Result> strings = sumTo100.GetAllSumsThatEqual(-9);
        Console.WriteLine("All sums that equal -9:");
        foreach (var s in strings)
        {
            Console.WriteLine(s);
        }

        //my method just returns -9 first, so it returns -9 as the most common sum, but 9 is also a valid answer
        Assert.AreEqual(-9, sumTo100.GetMostCommonSum());
    }

    [TestMethod]
    public void GetTopTenLargestSums_Result_Equals()
    {
        var sumTo100 = new RosettaCode.Problems.SumTo100.Trystan.Attempt2.SumTo100();
        double[] expected =
        [
            123456789,
            23456790,
            23456788,
            12345687,
            12345669,
            3456801,
            3456792,
            3456790,
            3456788,
            3456786,
        ];
        double[] actual = sumTo100.GetTopTenLargestSums();

        Assert.IsTrue(expected.SequenceEqual(actual));
    }

    [TestMethod]
    public void GetAllExpressionsThatEqual7_Count_IsGreaterThan()
    {
        SumTo100Parameters parameters = new SumTo100Parameters([1, 2, 3], true, true, true, false);
        var sumTo100 = new RosettaCode.Problems.SumTo100.Trystan.Attempt2.SumTo100(parameters);
        List<SumTo100Result> strings = sumTo100.GetAllSumsThatEqual(7);
        foreach (var s in strings)
        {
            Console.WriteLine(s);
        }

        Assert.IsGreaterThan(0, strings.Count);
    }

    [TestMethod]
    public void GetAllExpressionsThatEqual0_857142857143_Count_IsGreaterThan()
    {
        SumTo100Parameters parameters = new SumTo100Parameters([2, 3, 7], true, true, true, true);
        var sumTo100 = new RosettaCode.Problems.SumTo100.Trystan.Attempt2.SumTo100(parameters);
        List<SumTo100Result> strings = sumTo100.GetAllSumsThatEqual(.857142857143);
        foreach (var s in strings)
        {
            Console.WriteLine(s);
        }

        Assert.IsGreaterThan(0, strings.Count);
    }
}
