using System;
using System.Collections.Generic;
using System.Text;

namespace RosettaCode.Problems.KnightsTour.Trystan.Game;

public interface IPiece
{
    /// <summary>
    /// Gets the current position within the data source.
    /// </summary>
    Position Position { get; }
    /// <summary>
    /// Gets the name of the team associated with the current context.
    /// </summary>
    string Team { get; }
    /// <summary>
    /// Determines whether moving the piece to the specified coordinates on the given chessboard is a valid move
    /// according to the rules of chess.
    /// </summary>
    /// <param name="destinationX">The zero-based column index of the destination square.</param>
    /// <param name="destinationY">The zero-based row index of the destination square.</param>
    /// <param name="board">The chessboard on which to validate the move. Must not be null.</param>
    /// <returns>true if the move to the specified coordinates is valid; otherwise, false.</returns>
    bool IsMoveValid(int destinationX, int destinationY, ChessBoard board) => IsMoveValid(new Position(destinationX,destinationY), board);
    /// <summary>
    /// Determines whether a move to the specified position on the given chessboard is valid according to the current
    /// game rules.
    /// </summary>
    /// <param name="position">The target position to validate for the move. Represents the destination square on the chessboard.</param>
    /// <param name="board">The current state of the chessboard on which the move is to be validated.</param>
    /// <returns>true if the move to the specified position is valid; otherwise, false.</returns>
    bool IsMoveValid(Position position, ChessBoard board);
    /// <summary>
    /// Returns a list of all valid moves for the current player on the specified chessboard.
    /// </summary>
    /// <param name="board">The chessboard representing the current state of the game. Must not be null.</param>
    /// <returns>A list of positions representing all valid moves available to the current player. The list is empty if no valid
    /// moves are available.</returns>
    List<Position> GetAllValidMoves(ChessBoard board);
    /// <summary>
    /// Updates the current position to the specified value.
    /// </summary>
    /// <param name="position">The new position to set. Cannot be null.</param>
    void UpdatePosition(Position position);
}
