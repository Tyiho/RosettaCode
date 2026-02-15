using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

using FluentAssertions;

using RosettaCode.Problems._100Doors.Merl;

namespace RosettaCodeTest.Problems._100Doors.Merl
{
	[TestClass]
	public class Solve100DoorsTests
	{
		//only doors that remain open are those that are toggled an odd number of times,
		//which is only the case for perfect squares
		private static ImmutableList<uint> s_ExpectedOpenDoors = [1,4,9,16,25,36,49,64,81,100];


		[TestMethod]
		public void Solve_Basic_Unoptimized_CorrectlyIdentifiesOpenDoors_For100Doors100Passes()
		{
			//Arrange
			var solutions = new Solve100Doors();

			//Act
			solutions.Solve_Basic_Unoptimized(100, 100, out var openDoors, out _);

			//Assert
			openDoors.Should().BeEquivalentTo(s_ExpectedOpenDoors.ToList());
		}

		[TestMethod]
		public void Solve_Basic_Parallel_CorrectlyIdentifiesOpenDoors_For100Doors100Passes()
		{
			//Arrange
			var solutions = new Solve100Doors();

			//Act
			solutions.Solve_Basic_Parallel(100, 100, out var openDoors, out _);

			//Assert
			openDoors.Should().BeEquivalentTo(s_ExpectedOpenDoors.ToList());
		}

		[TestMethod]
		public void Solve_Class_Unoptimized_CorrectlyIdentifiesOpenDoors_For100Doors100Passes()
		{
			//Arrange
			var solutions = new Solve100Doors();

			//Act
			solutions.Solve_Class_Unoptimized(100, 100, out var openDoors, out _);

			//Assert
			openDoors.Should().BeEquivalentTo(s_ExpectedOpenDoors.ToList());
		}

		[TestMethod]
		public void Solve_Class_Parallel_CorrectlyIdentifiesOpenDoors_For100Doors100Passes()
		{
			//Arrange
			var solutions = new Solve100Doors();

			//Act
			solutions.Solve_Class_Parallel(100, 100, out var openDoors, out _);

			//Assert
			openDoors.Should().BeEquivalentTo(s_ExpectedOpenDoors.ToList());
		}

		[TestMethod]
		public void Solve_Togglers_Hierarchy_Unoptimized_CorrectlyIdentifiesOpenDoors_For100Doors100Passes()
		{
			//Arrange
			var solutions = new Solve100Doors();

			//Act
			solutions.Solve_Togglers_Hierarchy_Unoptimized(100, 100, out var openDoors, out _);

			//Assert
			openDoors.Should().BeEquivalentTo(s_ExpectedOpenDoors.ToList());
		}

		[TestMethod]
		public void Solve_Togglers_Hierarchy_Parallel_CorrectlyIdentifiesOpenDoors_For100Doors100Passes()
		{
			//Arrange
			var solutions = new Solve100Doors();

			//Act
			solutions.Solve_Togglers_Hierarchy_Parallel(100, 100, out var openDoors, out _);

			//Assert
			openDoors.Should().BeEquivalentTo(s_ExpectedOpenDoors.ToList());
		}

		[TestMethod]
		public void Solve_Togglers_Unoptimized_CorrectlyIdentifiesOpenDoors_For100Doors100Passes()
		{
			//Arrange
			var solutions = new Solve100Doors();

			//Act
			solutions.Solve_Togglers_Unoptimized(100, 100, out var openDoors, out _);

			//Assert
			openDoors.Should().BeEquivalentTo(s_ExpectedOpenDoors.ToList());
		}

		[TestMethod]
		public void Solve_Togglers_Parallel_CorrectlyIdentifiesOpenDoors_For100Doors100Passes()
		{
			//Arrange
			var solutions = new Solve100Doors();

			//Act
			solutions.Solve_Togglers_Parallel(100, 100, out var openDoors, out _);

			//Assert
			openDoors.Should().BeEquivalentTo(s_ExpectedOpenDoors.ToList());
		}
	}
}
