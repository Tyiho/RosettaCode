using RosettaCode.Problems._100Doors.Trystan;

namespace RosettaCodeTest.Problems._100Doors.Trystan;

[TestClass]
public class _100DoorsTrystan_Test
{

    [TestMethod]
    public void Test_RunProblem()
    {
        var problem = new _100DoorsTrystan();
        problem.RunProblem();

        var expected = new bool[100];
        for (int i = 0; i < expected.Length; i++)
        {
            expected[i] = false;
        }

        //only doors that remain open are those that are toggled an odd number of times,
        //which is only the case for perfect squares
        expected[0] = true; //door 1 is toggled by pass 1, so it remains open
        expected[3] = true; //door 4 is toggled by passes 1,2,4, so it remains open
        expected[8] = true; //door 9 is toggled by passes 1,3,9, so it remains open
        expected[15] = true; //door 16 is toggled by passes 1,2,4,8,16, so it remains open
        expected[24] = true; //...
        expected[35] = true;
        expected[48] = true;
        expected[63] = true;
        expected[80] = true;
        expected[99] = true;

        CollectionAssert.AreEqual(expected, problem.Result);
    }
}
