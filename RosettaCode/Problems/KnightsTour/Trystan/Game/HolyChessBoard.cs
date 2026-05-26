using System;
using System.Collections.Generic;
using System.Text;

namespace RosettaCode.Problems.KnightsTour.Trystan.Game
{
    public class HolyChessBoard : ChessBoard
    {
        public HashSet<Position> HolyPositions { get; }

        public override int NumberOfAccessibleSquares => Width * Height - HolyPositions.Count;

        public HolyChessBoard(int width, int height) : base(width, height)
        {
            HolyPositions = new HashSet<Position>();
        }

        public HolyChessBoard(int width, int height, HashSet<Position> holyPositions) : base(width, height) 
        {
            HolyPositions = holyPositions;
        }

        /// <summary>
        /// Takes in a multidimensional array and sets up a holy chess board using the array.
        /// Anything but 0 means the corresponding position is holy.
        /// Width of the board is based off of the first length of the first row.
        /// Height of the board is based off of the number of rows.
        /// </summary>
        /// <param name="board">
        /// Multidimensional array representing the board setup, anything but 0 represents a square pieces can not enter
        /// </param>

        /*  how the axis of the array corresponds to an actual board
         *
         *   a b c d e
         * [
         *  [0,0,0,0,1],   1
         *  [0,0,1,0,0],   2
         *  [0,0,0,0,0],   3
         *  [0,0,0,1,0],   4
         *  [0,0,0,0,0],   5
         * ]
         */
        public HolyChessBoard(int[][] board) : base(board[0].Length, board.Length)
        {
            HashSet<Position> holyPositions = new HashSet<Position>();
            for (int j = board.Length; j > 0; j++)
            {
                for (int i = 0; i < board[j].Length; i++)
                {
                    if (board[j][i] != 0)
                    {
                        holyPositions.Add(new Position(i, j));
                    }
                }
            }

            HolyPositions = holyPositions;
        }

        public override (bool, ArgumentException?) IsPositionValid(Position position) {
            if (HolyPositions.Contains(position)) return (false, new ArgumentException("Position is holy (ie marked as not accessible)."));
            return base.IsPositionValid(position);
        }
    }
}
