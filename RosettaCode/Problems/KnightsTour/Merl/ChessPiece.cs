using RosettaCode.Problems.KnightsTour.Merl;

namespace RosettaCode.Problems.KnightsPath.Merl
{
	public abstract class ChessPiece
	{
		/// <summary>
		/// This piece's current position
		/// </summary>
		public IntVector2 CurrentPosition { get; private set; }

		/// <summary>
		/// Returns the legal moves for this piece from its current position.
		/// </summary>
		/// <param name="board"></param>
		/// <returns></returns>
		public virtual List<IntVector2> GetMoves(ChessBoard board) => GetMoves(CurrentPosition, board);

		/// <summary>
		/// Returns the legal moves for this piece from the given position.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="board"></param>
		/// <returns></returns>
		public abstract List<IntVector2> GetMoves(IntVector2 position, ChessBoard board);

		/// <summary>
		/// Places this piece at the given position.
		/// Typically used for setup and backtracking.
		/// </summary>
		/// <param name="position"></param>
		public void SetPosition(IntVector2 position)
		{
			CurrentPosition = position;
		}

		/// <summary>
		/// Returns the result of applying the specified move to this piece's current position.
		/// Does not guarantee move accuracy for the piece or the result being valid.
		/// </summary>
		/// <param name="move"></param>
		/// <returns></returns>
		public virtual PiecePosition EvaluateMove(IntVector2 move) => EvaluateMove(CurrentPosition, move);

		/// <summary>
		/// Returns the result of applying the specified move from a specified position.
		/// Does not guarantee move accuracy for the piece or the result being valid.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="move"></param>
		/// <returns></returns>
		public virtual PiecePosition EvaluateMove(IntVector2 position, IntVector2 move) => new PiecePosition(position + move, move);


		/// <summary>
		/// Attempts to execure the specified move for this piece on the given board.
		/// Returns a boolean indicating successful movement.
		/// </summary>
		/// <param name="move">the desired move to take</param>
		/// <param name="board">the board state</param>
		/// <param name="newPosition">The new position, when successful, or <see cref="PiecePosition.InvalidPosition"/> otherwise.</param>
		/// <returns></returns>
		public virtual bool TryTakeMove(IntVector2 move, ChessBoard board, out PiecePosition newPosition)
		{
			newPosition = PiecePosition.InvalidPosition;

			if (board.IsMoveOnBoard(CurrentPosition, move))
			{
				newPosition = EvaluateMove(CurrentPosition, move);

				CurrentPosition = newPosition.Position;

				return true;
			}

			return false;
		}
	}
}
