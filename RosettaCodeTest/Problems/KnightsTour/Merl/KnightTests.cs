using FluentAssertions;

using NSubstitute;

using RosettaCode.Problems.KnightsPath.Merl;
using RosettaCode.Problems.KnightsTour.Merl;

namespace RosettaCodeTest.Problems.KnightsTour.Merl
{
	[TestClass]
	public class KnightTests
	{
		[TestMethod]
		public void GetMoves_ChecksIfResults_WouldBeOnTheBoard()
		{
			// Arrange
			ChessBoard board = Substitute.For<ChessBoard>();

			Knight piece = new();

			IntVector2 position = new IntVector2(3, 3); //where all moves are valid


			// Act
			_ = piece.GetMoves(position, board);

			// Assert
			board.Received().IsMoveOnBoard(position, Arg.Any<IntVector2>());
		}


		[TestMethod]
		public void GetMoves_ReturnsAllKnightMoves_WhenAllMovesAreAvailableOnTheBoard()
		{
			// Arrange
			ChessBoard board = Substitute.For<ChessBoard>();
			board.IsMoveOnBoard(default, default).ReturnsForAnyArgs(true);

			Knight piece = new();

			IntVector2 position = new IntVector2(3, 3); //where all moves are valid

			// Act/Assert
			piece.GetMoves(position, board).Should().HaveCount(8);
		}

		[TestMethod]
		public void GetMoves_ReturnsNoMoves_WhenNoMovesAreAvailableOnTheBoard()
		{
			// Arrange
			ChessBoard board = Substitute.For<ChessBoard>();
			board.IsMoveOnBoard(default, default).ReturnsForAnyArgs(false);

			Knight piece = new();

			IntVector2 position = new IntVector2(3, 3); //where all moves are valid

			// Act/Assert
			piece.GetMoves(position, board).Should().BeEmpty();
		}
	}
}
