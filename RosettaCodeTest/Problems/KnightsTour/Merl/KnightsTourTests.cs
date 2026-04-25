using System.Numerics;

using RosettaCode.Problems.KnightsPath.Merl;
using RosettaCode.Problems.KnightsTour.Merl;

using Code = RosettaCode.Problems.KnightsPath.Merl;

namespace RosettaCodeTest.Problems.KnightsTour.Merl
{
	[TestClass]
	public class KnightsTourTests
	{
		//because it's more fun to just debug! :)
		[TestMethod]
		public void DoItYourWay()
		{
			Code.KnightsTour tour = new();

			bool found = tour.Evaluate(6, 6, new Knight(), new IntVector2(0, 0), [], out var route);
		}


		[TestMethod]
		public void DoItNormally()
		{
			Code.KnightsTour tour = new();

			tour.SolveKnightsTour();
		}

		[TestMethod]
		public void DoItHoly()
		{
			Code.KnightsTour tour = new();

			tour.SolveHolyTour();
		}

		[TestMethod]
		public void RookItNormally()
		{
			Code.KnightsTour tour = new();

			tour.SolveRooksTour();
		}

		[TestMethod]
		public void QueenItNormally()
		{
			Code.KnightsTour tour = new();

			tour.SolveQueensTour();
		}
	}
}
