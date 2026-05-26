using System;
using System.Collections.Generic;
using System.Text;

namespace RosettaCode.Problems.KnightsTour.Trystan.Game;

public class ChessBoard
{
    public int Width { get; }
    public int Height { get; }

    public List<IPiece> Pieces { get; } = new List<IPiece>();
    public List<IPiece> CapturedPieces { get; } = new List<IPiece>();

    /// <summary>
    /// Gets the total number of accessible squares in the area, calculated as the product of the width and height.
    /// </summary>
    public virtual int NumberOfAccessibleSquares => Width * Height;

    public ChessBoard(int width, int height)
    {
        Width = width;
        Height = height;
    }

    private void CapturePiece(IPiece piece)
    {
        Pieces.Remove(piece);
        CapturedPieces.Add(piece);
    }

    /// <summary>
    /// Determines whether the specified position is within the valid bounds of the area.
    /// </summary>
    /// <param name="position">The position to validate. The position's coordinates are checked against the current width and height
    /// constraints.</param>
    /// <returns>A tuple containing a boolean value that is <see langword="true"/> if the position is valid; otherwise, <see
    /// langword="false"/>. If the position is invalid, the tuple also contains an <see cref="ArgumentException"/>
    /// describing the error; otherwise, the exception is <see langword="null"/>.</returns>
    public virtual (bool, ArgumentException?) IsPositionValid(Position position)
    {
        if (position.X < 0 || position.X >= Width || position.Y < 0 || position.Y >= Height)
        {
            return (false, new ArgumentException("Position is out of bounds."));
        }

        return (true, null);
    }

    /// <summary>
    /// Determines whether the specified position on the board is occupied by a piece.
    /// </summary>
    /// <param name="position">The position to check for occupancy.</param>
    /// <returns>A tuple where the first value indicates whether the position is occupied, and the second value is the piece
    /// occupying the position if present; otherwise, null.</returns>
    public (bool, IPiece?) IsOccupied(Position position)
    {
        foreach (var piece in Pieces)
        {
            if (piece.Position.X == position.X && piece.Position.Y == position.Y)
            {
                return (true, piece);
            }
        }

        return (false, null);
    }

    /// <summary>
    /// Adds a piece to the board at the specified position.
    /// </summary>
    /// <param name="piece">The piece to add to the board. The position of the piece must be within the bounds of the board and not already
    /// occupied.</param>
    /// <exception cref="ArgumentException">Thrown if the position of the piece is out of bounds or if the position is already occupied by another piece.</exception>
    public void AddPiece(IPiece piece)
    {
        (bool isValid, ArgumentException? exception) = IsPositionValid(piece.Position);
        if (!isValid)
        {
            throw exception!;
        }
        if (IsOccupied(piece.Position).Item1)
        {
            throw new ArgumentException("Position is already occupied by another piece.");
        }
        Pieces.Add(piece);
    }

    /// <summary>
    /// A method to move a piece to a new position on the board.
    /// This method checks if the piece is on the board,
    /// if the destination position is valid and within bounds,
    /// if the move is valid for the piece according to its movement rules,
    /// and if the destination position is not occupied by another piece owned by the player.
    /// If any of these conditions are not met, an appropriate exception is thrown.
    /// If the move is valid and the destination position is occupied by an opponent's piece,
    /// that piece is captured and removed from the board before moving the piece to the new position.
    /// </summary>
    /// <param name="piece">The piece to move.</param>
    /// <param name="destination">The destination position for the piece.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when the piece is not on the board, the destination position is out of bounds,
    /// the move is invalid for the piece,
    /// or the destination position is occupied by another piece owned by the player.
    /// </exception>
    public void MovePiece(IPiece piece, Position destination)
    {
        if (!Pieces.Contains(piece))
        {
            throw new ArgumentException("Piece is not on the board.");
        }

        (bool isMoveValidOnBoard, ArgumentException? exceptionAboutWhy) = IsPositionValid(destination);
        if (!isMoveValidOnBoard)
        {
            throw exceptionAboutWhy!;
        }
        
        if (!piece.IsMoveValid(destination, this))
        {
            throw new ArgumentException("Invalid move for the piece.");
        }

        var occupied = IsOccupied(destination);
        if (occupied.Item1)
        {
            if(occupied.Item2?.Team != piece.Team) this.CapturePiece(occupied.Item2!); // Capture the piece if it's an opponent's piece
            else throw new ArgumentException("Destination position is already occupied by another piece owned by the player.");
        }
        
    }
}
