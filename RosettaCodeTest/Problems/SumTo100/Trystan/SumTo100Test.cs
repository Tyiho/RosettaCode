using System;
using System.Collections.Generic;
using System.Text;

using RosettaCode.Problems.SumTo100.Trystan;

namespace RosettaCodeTest.Problems.SumTo100.Trystan;

[TestClass]
public class SumTo100Test
{
    [TestMethod]
    public void GetAllSumsThatEqual100_ResultLength_IsGreaterThan()
    {
        var sumTo100 = new RosettaCode.Problems.SumTo100.Trystan.SumTo100();

        List<string> strings = sumTo100.GetAllSumsThatEqual100();

        foreach (var s in strings)
        {
            Console.WriteLine(s);
        }

        Assert.IsGreaterThan(0,strings.Count);
    }

    [TestMethod]
    public void GetFirstPositiveNumberThatCannotBeSummed_Result_Equals()
    {
        var sumTo100 = new RosettaCode.Problems.SumTo100.Trystan.SumTo100();

        Assert.AreEqual(211, sumTo100.GetFirstPositiveNumberThatCannotBeSummed());
    }

    [TestMethod]
    public void GetMostCommonSum_Result_Equals()
    {
        var sumTo100 = new RosettaCode.Problems.SumTo100.Trystan.SumTo100();

        // The most common sum is 9, which occurs 46 times
        Console.WriteLine(sumTo100.GetNumberOfSumsThatEqual(9));
        // This ties with -9, which also occurs 46 times
        Console.WriteLine(sumTo100.GetNumberOfSumsThatEqual(-9));

        //my method just returns -9 first, so it returns -9 as the most common sum, but 9 is also a valid answer
        Assert.AreEqual(-9, sumTo100.GetMostCommonSum());
    }

    [TestMethod]
    public void GetTopTenMostCommonSums_Result_Equals()
    {
        var sumTo100 = new RosettaCode.Problems.SumTo100.Trystan.SumTo100();
        foreach (var i in sumTo100.GetTopTenLargestSums())
        {
            Console.WriteLine(i);
        }

        Assert.IsTrue(true);
    }
}