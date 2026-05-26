using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

using FluentAssertions;

using RosettaCode.Problems.KnightsPath.Merl;
using RosettaCode.Problems.KnightsTour.Merl;

namespace RosettaCodeTest.Problems.KnightsTour.Merl
{
	[TestClass]
	public class ExtensionMethodsTests
	{

		public static IEnumerable<object[]> ToAlgebraicNotationTestCases()
		{
			//For a standard 8x8 board:
			yield return [new IntVector2(0, 0), "a1"];
			yield return [new IntVector2(1, 0), "b1"];
			yield return [new IntVector2(2, 0), "c1"];
			yield return [new IntVector2(3, 0), "d1"];
			yield return [new IntVector2(4, 0), "e1"];
			yield return [new IntVector2(5, 0), "f1"];
			yield return [new IntVector2(6, 0), "g1"];

			yield return [new IntVector2(0, 1), "a2"];
			yield return [new IntVector2(0, 2), "a3"];
			yield return [new IntVector2(0, 3), "a4"];
			yield return [new IntVector2(0, 4), "a5"];
			yield return [new IntVector2(0, 5), "a6"];
			yield return [new IntVector2(0, 6), "a7"];
			yield return [new IntVector2(0, 7), "a8"];
		}

		[TestMethod]
		[DynamicData(nameof(ToAlgebraicNotationTestCases))]
		public void ToAlgebraicNotation_CorrectlyIdentifes_ProvidedPosition(IntVector2 position, string expectedNotation)
		{
			// Act/Assert
			position.ToAlgebraicNotation().Should().Be(expectedNotation);
		}

	
		public static IEnumerable<object[]> ToRouteStringTestCases()
		{
			//no entries, empty string
			yield return [new Queue<PiecePosition>(), ""];

			//1 entry, just the single point in algebraic notation
			yield return [new Queue<PiecePosition>([new PiecePosition(new IntVector2(0, 0), new IntVector2(0, 0))]), "a1"];

			//more than one entry, we have a string with the positions in original order
			//with a delimiter of " -> ". Should not have an extra delimiter at the end.
			const string DELIMITER = " -> ";

			yield return [new List<PiecePosition>([new PiecePosition(new IntVector2(0, 0), new IntVector2(0, 0)),
												new PiecePosition(new IntVector2(1, 1), new IntVector2(0, 0)),
												new PiecePosition(new IntVector2(2, 2), new IntVector2(0, 0))]), 
						  $"a1{DELIMITER}b2{DELIMITER}c3"];
		}

		[TestMethod]
		[DynamicData(nameof(ToRouteStringTestCases))]
		public void ToRouteString_GeneratesAnAlgebraicNotationPath_ForTheGivenPositions(List<PiecePosition> route, string expectedRoute)
		{
			// Act/Assert
			route.ToRouteString().Should().Be(expectedRoute);
		}
	}
}
