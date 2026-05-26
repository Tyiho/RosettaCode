using RosettaCode.Problems.KnightsTour.Trystan.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace RosettaCode.Problems.KnightsTour.Trystan.Game.Pieces;

public class Knight : IPiece
{
    public Position Position { get; private set; }

    private int x => Position.X;
    private int y => Position.Y;

    public string Team { get; }

    public void UpdatePosition(Position position) => Position = position;

    /// <summary>
    /// Checks if a given move is valid for a knight piece from the current position.
    /// Does not check if the destination square is occupied or if the move would put the king in check,
    /// as those rules are handled by the ChessBoard class.
    /// </summary>
    /// <param name="destinationX">The x position the knight is moving to.</param>
    /// <param name="destinationY">The y position the knight is moving to.</param>
    /// <param name="board">The chess board the knight is on.</param>
    /// <returns>True if the move is valid, false otherwise.</returns>
    public bool IsMoveValid(Position position, ChessBoard board)
    {
        if(!board.IsPositionValid(position).Item1) return false;
        int deltaX = Math.Abs(x - position.X);
        int deltaY = Math.Abs(y - position.Y);
        if (deltaX == 2 && deltaY == 1 || deltaX == 1 && deltaY == 2) return true;
        return false;
    }

    /// <summary>
    /// Returns all valid moves for the knight piece in its current position.
    /// Does not check if the destination square is occupied or if the move would put the king in check,
    /// as those rules are handled by the ChessBoard class.
    /// </summary>
    /// <param name="board">The chess board the knight is on.</param>
    /// <returns>A list of tuples representing the valid moves for the knight.</returns>
    public List<Position> GetAllValidMoves(ChessBoard board)
    {
        List<Position> validMoves = [
            (x+2,y+1),
            (x+2,y-1),
            (x-2,y+1),
            (x-2,y-1),
            (x + 1, y + 2),
            (x - 1, y + 2),
            (x + 1, y - 2),
            (x - 1, y - 2),
        ];

        validMoves.RemoveAll(move => board.IsPositionValid(move).Item1 is false);

        return validMoves;
    }

    public Knight(Position position, string team)
    {
        Position = position;
        Team = team;
    }
}