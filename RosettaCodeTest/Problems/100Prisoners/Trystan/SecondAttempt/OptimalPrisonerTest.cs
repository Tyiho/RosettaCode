using RosettaCode.Problems._100Prisoners.Trystan.SecondAttempt;

namespace RosettaCodeTest.Problems._100Prisoners.Trystan.SecondAttempt;

[TestClass]
public class OptimalPrisonerTest
{

    [TestMethod]
    public void OptimalPrisoner_Type_IsInstanceOfType()
    {
        OptimalPrisoner prisoner = new OptimalPrisoner(0, 50);
        Assert.IsInstanceOfType(prisoner, typeof(Prisoner));
    }

    [TestMethod]
    public void OptimalPrisoner_PrisonerNumber_MatchesConstructor()
    {
        OptimalPrisoner prisoner = new OptimalPrisoner(5, 50);
        Assert.AreEqual(5U, prisoner.PrisonerNumber);
    }

    [TestMethod]
    public void OptimalPrisoner_MaxSearchDepth_MatchesConstructor()
    {
        OptimalPrisoner prisoner = new OptimalPrisoner(5, 50);
        Assert.AreEqual(50U, prisoner.MaxSearchDepth);
    }

    [TestMethod]
    public void OptimalPrisoner_SearchFunction_FindsNumber()
    {
        uint[] drawers = [5, 0, 1, 2, 3, 4];
        OptimalPrisoner prisoner = new OptimalPrisoner(5, 50);
        bool result = prisoner.SearchFunction(0, drawers);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void OptimalPrisoner_SearchFunction_DoesNotFindNumber()
    {
        uint[] drawers = [5, 3, 1, 0, 2, 4];
        OptimalPrisoner prisoner = new OptimalPrisoner(5, 3);
        bool result = prisoner.SearchFunction(0, drawers);
        Assert.IsFalse(result);
    }
}