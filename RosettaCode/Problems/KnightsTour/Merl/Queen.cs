using System;
using System.Collections.Generic;
using System.Text;

using RosettaCode.Problems.KnightsPath.Merl;

namespace RosettaCode.Problems.KnightsTour.Merl
{
	public class Queen : ChessPiece
	{
		public override List<IntVector2> GetMoves(IntVector2 position, ChessBoard board)
		{
			List<IntVector2> moves = new();

			//any number of horizontal or vertical spaces on the board.
			for (int x = 0; x < board.Width; x++)
			{
				int deltaX = x - position.X;

				if (deltaX != 0)
				{
					moves.Add(new IntVector2(deltaX, 0));
				}
			}

			for (int y = 0; y < board.Height; y++)
			{
				int deltaY = y - position.Y;

				if (deltaY != 0)
				{
					moves.Add(new IntVector2(0, deltaY));
				}
			}

			//and any number of diagonal spaces
			for (int delta = 0; delta < board.Width; delta++)
			{
				//right & up
				IntVector2 move = new(delta, delta);

				if (board.IsMoveOnBoard(CurrentPosition, move))
				{
					moves.Add(move);
				}

				//right & down
				move = new(delta, -delta);

				if (board.IsMoveOnBoard(CurrentPosition, move))
				{
					moves.Add(move);
				}

				//left & up
				move = new(-delta, delta);

				if (board.IsMoveOnBoard(CurrentPosition, move))
				{
					moves.Add(move);
				}

				//left & down
				move = new(-delta, -delta);

				if (board.IsMoveOnBoard(CurrentPosition, move))
				{
					moves.Add(move);
				}

			}

			return moves;
		}
	}
}
