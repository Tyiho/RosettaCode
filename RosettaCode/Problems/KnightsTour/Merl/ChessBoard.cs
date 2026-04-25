using System.Collections.Frozen;
using System.Text;

using RosettaCode.Problems.KnightsTour.Merl;

namespace RosettaCode.Problems.KnightsPath.Merl
{
	public class ChessBoard
	{
		private int m_Width;
		private int m_Height;

		/// <summary>
		/// Holds the board, recording the visited positions
		/// (x,y)
		/// </summary>
		private bool[,] m_VisitedPositions;

		/// <summary>
		/// Holds positions that are not availble (see Holy Knight's tour)
		/// </summary>
		private FrozenSet<IntVector2> m_UnavailablePositions = [];

		public int Width => m_Width;
		public int Height => m_Height;

		public ChessBoard() 
		{
			/* for Unit Test only! */

			m_VisitedPositions = new bool[1,1];
		}

		public ChessBoard(int width, int height)
		{
			m_Width = width;
			m_Height = height;
			m_VisitedPositions = new bool[width, height];
		}

		public ChessBoard(int width, int height, IntVector2[] unavailablePositions) : this(width, height)
		{
			m_UnavailablePositions = FrozenSet.Create(unavailablePositions);
		}

		public virtual bool IsMoveOnBoard(IntVector2 currentPosition, IntVector2 move)
		{
			IntVector2 newPosition = currentPosition + move;

			bool onBoard = newPosition.X >= 0 && newPosition.X < m_Width &&
						   newPosition.Y >= 0 && newPosition.Y < m_Height &&
						   !m_UnavailablePositions.Contains(newPosition);

			return onBoard;
		}


		public virtual bool HasBeenVisited(PiecePosition position) => HasBeenVisited(position.Position);

		public virtual bool HasBeenVisited(IntVector2 position) => m_VisitedPositions[(int)position.X, (int)position.Y];

		public virtual void Visit(IntVector2 position) => m_VisitedPositions[(int)position.X, (int)position.Y] = true;
		
		public virtual void ClearVisit(IntVector2 position) => m_VisitedPositions[(int)position.X, (int)position.Y] = false;

		public virtual bool HaveAllPositionsBeenVisited()
		{
			bool covered = true;

			for (int x = 0;  x < m_Width; x++)
			{
				for (int y = 0; y < m_Height; y++) 
				{
					IntVector2 space = new(x, y);

					//unavailable positions don't count
					if (m_UnavailablePositions.Contains(space))
					{
						//just move on.
						continue;
					}

					if (!m_VisitedPositions[x,y])
					{
						covered = false;
						break;
					}
				}

				if (!covered)
				{
					break;
				}
			}

			return covered;
		}

		public enum BoardMarkers
		{
			Blank,
			Piece,
			Unusable,
		}

		public virtual StringBuilder RenderBoard(List<PiecePosition> path, ChessPiece piece)
		{
			//convert board to hold positions
			int[,] moveBoard = new int[m_Width, m_Height];

			int positionCount = 1;

			//place each position in the path
			foreach(IntVector2 position in path.Select(pm => pm.Position))
			{
				//recording it's move number
				moveBoard[(int)position.X, (int)position.Y] = positionCount++;
			}

			//place the piece
			moveBoard[(int)piece.CurrentPosition.X, (int)piece.CurrentPosition.Y] = -1; //flag the piece position

			//mark unavailable positions so they don't look like unused locations
			foreach (var position in m_UnavailablePositions)
			{
				moveBoard[(int)position.X, (int)position.Y] = -2;
			}


			StringBuilder sb = new StringBuilder();

			//how many characters would we need?
			// +---+---+ ... +---+
			// + 1 + 2 + ... + 8 +
			// +---+---+ ... +---+

			int digitCount = (m_Width * m_Height).NumberOfDigits();
			int cellWidth = 4 + digitCount;


			for (int y = m_Height-1; y >=0; y--)
			{
				//print the row header
				for (int cell = 0; cell < m_Width; cell++)
				{
					sb.Append("+");
					sb.Append('-', digitCount + 2);
				}

				sb.Append('+');
				sb.AppendLine();


				//print the positions
				for (int x = 0; x < m_Width; x++)
				{
					switch (moveBoard[x, y])
					{
						case -2:
							//unusable
							sb.Append($"+ {".".PadLeft(digitCount)} ");
							break;

						case -1:
							//piece
							sb.Append($"+ {"@".PadLeft(digitCount)} ");
							break;

						case 0:
							//not visited (blank)
							sb.Append($"+ {" ".PadLeft(digitCount)} ");
							break;

						default:
							//position count
							sb.Append($"+ {moveBoard[x, y].ToString().PadLeft(digitCount)} ");
							break;
					}
				}

				sb.Append('+');
				sb.AppendLine();
			}

			//print the footer
			for (int cell = 0; cell < m_Width; cell++)
			{
				sb.Append("+");
				sb.Append('-', digitCount + 2);
			}

			sb.Append('+');
			sb.AppendLine();

			return sb;
		}
	}
}
