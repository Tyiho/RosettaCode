using FluentAssertions;
using Newtonsoft.Json.Linq;
using RosettaCode.Problems._100Prisoners.Trystan.SecondAttempt;
using System.Reflection;

namespace RosettaCodeTest.Problems._100Prisoners.Trystan.SecondAttempt;

[TestClass]
public class PrisonerTest
{
    [TestMethod]
    public void Prisoner_Class_IsAbstract()
    {
        Type type = typeof(Prisoner);
        Assert.IsTrue(type.IsAbstract);
    }
}