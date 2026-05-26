using System.Collections.Frozen;

using RosettaCode.Problems.KnightsTour.Merl;

namespace RosettaCode.Problems.KnightsPath.Merl
{
	public class Knight : ChessPiece
	{
		//for a knight, all possible moves are fixed in the various 'L' patterns around the current position
		//These are "Knight Moves" (apologies to Bob Seger and band)
		private static FrozenSet<IntVector2> s_PossibleMoves =
			[
				//clockwise from ~12
				new IntVector2( 1,  2),
				new IntVector2( 2,  1),
				new IntVector2( 2, -1),
				new IntVector2( 1, -2),
				new IntVector2(-1, -2),
				new IntVector2(-2, -1),
				new IntVector2(-2,  1),
				new IntVector2(-1,  2),
			];

		/// <summary>
		/// Returns the legal moves for this piece from the given position.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="board"></param>
		/// <returns></returns>
		public override List<IntVector2> GetMoves(IntVector2 position, ChessBoard board) =>
			s_PossibleMoves.Where(move => board.IsMoveOnBoard(position, move)).ToList();
	}
}
