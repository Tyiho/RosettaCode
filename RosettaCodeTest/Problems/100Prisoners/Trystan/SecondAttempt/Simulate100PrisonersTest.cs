using RosettaCode.Problems._100Prisoners.Trystan.SecondAttempt;

namespace RosettaCodeTest.Problems._100Prisoners.Trystan.SecondAttempt;

[TestClass]
public class Simulate100PrisonersTest
{
    ////////// SetupScenario tests //////////

    [TestMethod]
    public void SetupScenario_Drawers_HasCount()
    {
        Simulate100Prisoners.SetupScenario<OptimalPrisonerFactory>(100, 50, out uint[] drawers, out Prisoner[] prisoners);
        Assert.HasCount(100, drawers);
    }

    [TestMethod]
    public void SetupScenario_Prisoners_HasCount()
    {
        Simulate100Prisoners.SetupScenario<OptimalPrisonerFactory>(100, 50, out uint[] drawers, out Prisoner[] prisoners);
        Assert.HasCount(100, prisoners);
    }

    [TestMethod]
    public void SetupScenario_PrisonerMaxSearchDepth_AreEqual()
    {
        Simulate100Prisoners.SetupScenario<OptimalPrisonerFactory>(1, 50, out uint[] drawers, out Prisoner[] prisoners);
        Assert.AreEqual<uint>(50, prisoners[0].MaxSearchDepth);
    }

    [TestMethod]
    public void SetupScenario_Drawers_AllAreUnique()
    {
        Simulate100Prisoners.SetupScenario<OptimalPrisonerFactory>(100, 50, out uint[] drawers, out Prisoner[] prisoners);

        CollectionAssert.AllItemsAreUnique(drawers);
    }

    [TestMethod]
    public void SetupScenario_Prisoners_AllAreUnique()
    {
        Simulate100Prisoners.SetupScenario<OptimalPrisonerFactory>(100, 50, out uint[] drawers, out Prisoner[] prisoners);
        CollectionAssert.AllItemsAreUnique(prisoners);
    }

    [TestMethod]
    public void SetupScenario_DrawersSet_AreEquivalent()
    {
        Simulate100Prisoners.SetupScenario<OptimalPrisonerFactory>(100, 50, out uint[] drawers, out Prisoner[] prisoners);

        for (int i = 0; i < drawers.Length; i++)
        {
            Console.WriteLine(drawers[i]);
        }

        CollectionAssert.AreEquivalent(drawers, Enumerable.Range(0, 100).Select(i=>(uint)i).ToArray());
    }

    [TestMethod]
    public void SetupScenario_PrisonersSet_AreEquivalent()
    {
        Simulate100Prisoners.SetupScenario<OptimalPrisonerFactory>(100, 50, out uint[] drawers, out Prisoner[] prisoners);
        CollectionAssert.AreEquivalent(prisoners.Select(prisoner=>prisoner.PrisonerNumber).ToArray(), Enumerable.Range(0, 100).Select(i => (uint)i).ToArray());
    }

    ////////// SimulatePrisoners tests //////////

    [TestMethod]
    public void SimulatePrisoners_Task_IsNotNull()
    {
        Task<bool> simulationTask = Simulate100Prisoners.SimulatePrisoners<OptimalPrisonerFactory>(100, 50);
        Assert.IsNotNull(simulationTask);
    }

    ////////// Simulate100Prisoners10000Times tests //////////

    [TestMethod]
    public void Simulate100Prisoners10000Times_RandomPrisonerFactoryResult_IsWithinRange()
    {
        float successRate = Simulate100Prisoners.Simulate100Prisoners10000Times<RandomPrisonerFactory>();
        Assert.IsInRange(0,.0001,successRate); //contains ~100% of the distribution of success rates for 10,000 simulations
    }

    [TestMethod]
    public void Simulate100Prisoners10000Times_OptimalPrisonerFactoryResult_IsWithinRange()
    {
        float successRate = Simulate100Prisoners.Simulate100Prisoners10000Times<OptimalPrisonerFactory>();
        Assert.IsInRange(.29, .33, successRate); //contains ~100% of the distribution of success rates for 10,000 simulations
    }

    ////////// Simulate100RandomPrisoners10000Times test //////////

    [TestMethod]
    public void Simulate100RandomPrisoners10000Times_Result_IsWithinRange()
    {
        float successRate = Simulate100Prisoners.Simulate100RandomPrisoners10000Times();
        Assert.IsInRange(0,.0001,successRate); //contains ~100% of the distribution of success rates for 10,000 simulations
    }

    ////////// Simulate100OptimalPrisoners10000Times test //////////

    [TestMethod]
    public void Simulate100OptimalPrisoners10000Times_Result_IsWithinRange()
    {
        float successRate = Simulate100Prisoners.Simulate100OptimalPrisoners10000Times();
        Assert.IsInRange(.29, .33, successRate); //contains ~100% of the distribution of success rates for 10,000 simulations
    }
}