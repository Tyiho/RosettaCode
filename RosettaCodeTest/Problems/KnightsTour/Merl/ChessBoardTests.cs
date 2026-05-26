using System.Numerics;

using FluentAssertions;

using RosettaCode.Problems.KnightsPath.Merl;
using RosettaCode.Problems.KnightsTour.Merl;

namespace RosettaCodeTest.Problems.KnightsTour.Merl
{
	[TestClass]
	public class ChessBoardTests
	{
		[TestMethod]
		public void IsMoveOnBoard_AnswersTrue_WhenFinalPositionLiesWithinBounds()
		{
			// Arrange
			ChessBoard board = new(2, 2);

			IntVector2 fromPosition = new IntVector2(0, 0);
			IntVector2 move = new(1, 0);

			// Act/Assert
			board.IsMoveOnBoard(fromPosition, move).Should().BeTrue();
		}

		[TestMethod]
		public void IsMoveOnBoard_AnswersFalse_WhenFinalPositionLiesOutsideBounds()
		{
			// Arrange
			ChessBoard board = new(2, 2);

			IntVector2 fromPosition = new IntVector2(0, 0);
			IntVector2 move = new(-1, 0);

			// Act/Assert
			board.IsMoveOnBoard(fromPosition, move).Should().BeFalse();
		}

		[TestMethod]
		public void IsMoveOnBoard_AnswersTrue_WhenFinalPositionLiesWithinBoundsAndIsNotAProhibitedSpace()
		{
			// Arrange
			IntVector2[] exclusions = [new IntVector2(0, 1), new IntVector2(1, 1)];

			ChessBoard board = new(2, 2, exclusions);

			IntVector2 fromPosition = new IntVector2(0, 0);
			IntVector2 move = new(1, 0);

			// Act/Assert
			board.IsMoveOnBoard(fromPosition, move).Should().BeTrue();
		}

		[TestMethod]
		public void IsMoveOnBoard_AnswersFalse_WhenFinalPositionLiesWithinBoundsButIsAProhibitedSpace()
		{
			// Arrange
			IntVector2[] exclusions = [new IntVector2(0, 1), new IntVector2(1, 1)];

			ChessBoard board = new(2, 2, exclusions);

			IntVector2 fromPosition = new IntVector2(0, 0);
			IntVector2 move = new(0, 1);

			// Act/Assert
			board.IsMoveOnBoard(fromPosition, move).Should().BeFalse();
		}

		//without getting into private state, Visit and ClearVisit are tied to HasBeenVisited for testing.

		[TestMethod]
		public void HasBeenVisited_AnswersTrue_WhenThePositionHasBeenVisited()
		{
			// Arrange
			ChessBoard board = new(2, 2);
			IntVector2 position = new IntVector2(1, 1);

			// Act
			board.Visit(position);

			// Assert
			board.HasBeenVisited(position).Should().BeTrue();
		}

		[TestMethod]
		public void HasBeenVisited_AnswesFalse_WhenThePositionHasNotBeenVisited()
		{
			// Arrange
			ChessBoard board = new(2, 2);
			IntVector2 position = new IntVector2(1, 1);

			// Assert
			board.HasBeenVisited(position).Should().BeFalse();
		}

		[TestMethod]
		public void HaveAllPositionsBeenVisited_ReturnsFalse_WhenBoardHasAtLeastOneUnvisitedPosition()
		{
			// Arrange
			ChessBoard board = new(2, 1);

			// Act
			board.Visit(new IntVector2(1, 0));

			// Assert
			board.HaveAllPositionsBeenVisited().Should().BeFalse();
		}

		[TestMethod]
		public void HaveAllPositionsBeenVisited_ReturnsTrue_WhenAllPositionsOnTheBoardHaveBeenVisited()
		{
			// Arrange
			ChessBoard board = new(2, 1);

			// Act
			board.Visit(new IntVector2(1, 0));
			board.Visit(new IntVector2(0, 0));

			// Assert
			board.HaveAllPositionsBeenVisited().Should().BeTrue();
		}

		[TestMethod]
		public void HaveAllPositionsBeenVisited_ReturnsTrue_WhenAllNonExceptionPositionsOnTheBoardHaveBeenVisited()
		{
			// Arrange
			IntVector2[] exceptions = [new IntVector2(0, 0)];
			ChessBoard board = new(2, 1, exceptions);

			// Act
			board.Visit(new IntVector2(1, 0));

			// Assert
			board.HaveAllPositionsBeenVisited().Should().BeTrue();
		}

	}
}
