using System;
using System.Collections.Generic;
using System.Text;

using RosettaCode.Problems._100Prisoners.Merl;

namespace RosettaCodeTest.Problems._100Prisoners.Merl
{
	[TestClass]
	public class Solve100PrisonersTests
	{
		[TestMethod]
		public void RunSimulations()
		{
			//just using this to run the code.
			//We could try and create tests for the code but
			//we'd need a design that lets us evaluation each iteration.

			Solve100Prisoners.EvaluateSolutionStrategies();
		}
	}
}
