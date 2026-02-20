using FluentAssertions;
using RosettaCode.Problems._100Prisoners.Trystan.SecondAttempt;

namespace RosettaCodeTest.Problems._100Prisoners.Trystan.SecondAttempt;

[TestClass]
public class RandomPrisonerTest
{
    [TestMethod]
    public void RandomPrisoner_Type_IsInstanceOfType()
    {
        RandomPrisoner prisoner = new RandomPrisoner(0, 50);
        Assert.IsInstanceOfType(prisoner, typeof(Prisoner));
    }

    [TestMethod]
    public void RandomPrisoner_PrisonerNumber_MatchesConstructor()
    {
        RandomPrisoner prisoner = new RandomPrisoner(5, 50);
        Assert.AreEqual(5U, prisoner.PrisonerNumber);
    }

    [TestMethod]
    public void RandomPrisoner_MaxSearchDepth_MatchesConstructor()
    {
        RandomPrisoner prisoner = new RandomPrisoner(5, 50);
        Assert.AreEqual(50U, prisoner.MaxSearchDepth);
    }

    [TestMethod]
    public void RandomPrisoner_SearchFunction_ShouldNotThrow()
    {
        uint[] drawers = [5, 0, 1, 2, 3, 4];
        RandomPrisoner prisoner = new RandomPrisoner(5, 50);
        Action act = () => prisoner.SearchFunction(0, drawers);

        act.Should().NotThrow();
    }
}