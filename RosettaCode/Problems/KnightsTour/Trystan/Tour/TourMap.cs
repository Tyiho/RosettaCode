using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using RosettaCode.Problems.KnightsTour.Trystan.Game;
using RosettaCode.Problems.KnightsTour.Trystan.Game.Pieces;

using GraphLibrary.Structs;

namespace RosettaCode.Problems.KnightsTour.Trystan.Tour;

/// <summary>
/// Class for solving a Knights tour on a given chess board.
/// I decided to use a graph based approach, where each square is a Position (vertex) and edges represent valid knight moves.
/// This allows for a more efficient search for the tour, as we can easily get all valid moves from a given position.
/// The search algorithm used is a depth first search to find a hamiltonian path, which is implemented in both a synchronous and asynchronous version.
/// The asynchronous version allows for better performance on larger boards, as it can take advantage of multiple CPU cores.
/// The search can be cancelled using a cancellation token, which is useful for long running searches on larger boards.
/// </summary>
/// <param name="board">The chess board on which to perform the tour.</param>
/// <param name="knight">The knight piece to use for the tour.</param>
public class TourMap(ChessBoard board, Knight knight)
{
    private readonly ChessBoard _board = board;
    private readonly Knight _knight = knight;
    private Graph<Position> _graph = new();
    private bool _isGraphInitialized = false;

    private void InitializeGraph()
    {
        for (int i = 0; i < _board.Width; i++)
        {
            for (int j = 0; j < _board.Height; j++)
            {
                Position pos = new Position(i, j);
                if (!_board.IsPositionValid(pos).Item1) continue;
                _graph.AddVertex(pos);
                Knight knight = new Knight(pos,"");
                List<Position> moves = knight.GetAllValidMoves(_board);
                foreach (Position move in moves)
                {
                    _graph.AddEdge(pos, move);
                }
            }
        }

        _isGraphInitialized = true;
    }

    private bool IsTourComplete(Queue<Position> path)
    {
        return path.Count == _board.NumberOfAccessibleSquares;
    }

    // A potentially more effective algorithm is https://www.dcs.gla.ac.uk/~pat/jchoco/lds/hcp/p131-martello.pdf
    // But I don't understand enough notation to translate to c#

    public Task<(bool, Queue<Position>?)> DepthFirstSearchTask(Queue<Position> visited, CancellationToken? token = null)
    {
            
            return Task<(bool, Queue<Position>?)>.Run(() =>
        { 
            List<Task<(bool, Queue<Position>?)>> tasks = new List<Task<(bool, Queue<Position> ?)>>();
            CancellationTokenSource cts = new CancellationTokenSource();

            foreach (Position neighbor in _graph.GetNeighbors(visited.Last()))
            {
                if(token is not null) token.Value.ThrowIfCancellationRequested();

                if (!visited.Contains(neighbor))
                {
                    Queue<Position> newVisited = new Queue<Position>(visited);
                    newVisited.Enqueue(neighbor);
                    if (IsTourComplete(newVisited))
                    {
                        return (true, newVisited);
                    }

                    //check if we need to pass through a cancellation token
                    if (token is null) //pass through this methods token
                    {
                        if (Process.GetCurrentProcess().Threads.Count < Environment.ProcessorCount * 2) tasks.Add(DepthFirstSearchTask(newVisited, cts.Token));
                        else tasks.Add(Task.Run(() => DepthFirstSearch(newVisited), cts.Token));
                    }
                    else // pass through cancellation token
                    {
                        if (Process.GetCurrentProcess().Threads.Count < Environment.ProcessorCount * 2) tasks.Add(DepthFirstSearchTask(newVisited, token));
                        else tasks.Add(Task.Run(() => DepthFirstSearch(newVisited), token.Value));
                    }


                }
            }

            while (tasks.Count > 0)
            {
                var completedTask = Task.WhenAny(tasks).Result;
                tasks.Remove(completedTask);
                var result = completedTask.Result;
                if (result.Item1)
                {
                    if (token is null) cts.Cancel();
                    return result;
                }
            }
            return (false, null);
        }, token ?? CancellationToken.None);
    }

    public (bool, Queue<Position>?) DepthFirstSearch(Queue<Position> visited)
    {
        foreach (Position neighbor in _graph.GetNeighbors(visited.Last()))
        {
            if (!visited.Contains(neighbor))
            {
                Queue<Position> newVisited = new Queue<Position>(visited);
                newVisited.Enqueue(neighbor);
                if (IsTourComplete(newVisited))
                {
                    return (true, newVisited);
                }
                var result = DepthFirstSearch(newVisited);
                if (result.Item1)
                {
                    return result;
                }
            }
        }
        return (false, null);
    }

    public (HashSet<Position>, HashSet<Position>) GetRedAndNonRedVertices(Position pivot, (HashSet<Position>, HashSet<Position>) vertices)
    {
        bool isPivotRed = vertices.Item1.Contains(pivot);
        foreach (var neighbor in _graph.GetNeighbors(pivot))
        {
            //if the neighbor is already in either set, skip it
            if (vertices.Item1.Contains(neighbor) || vertices.Item2.Contains(neighbor)) continue;

            //if the pivot is red, then the neighbor is non-red, and vice versa
            if (isPivotRed) vertices.Item2.Add(neighbor);
            else vertices.Item1.Add(neighbor);

            //update the sets with the neighbors of the neighbor recursively
            (HashSet<Position> red, HashSet<Position> nonRed) = GetRedAndNonRedVertices(neighbor, vertices);
            vertices.Item1.UnionWith(red);
            vertices.Item2.UnionWith(nonRed);
        }

        //return the updated sets
        return vertices;
    }

    public bool IsTourImpossible()
    {
        int count = 0;
        foreach (Position pos in _graph.Vertices)
        {
            //disconnected vertex
            if(_graph.Degree(pos) == 0) return true;

            //dead end vertex
            if (_graph.Degree(pos) == 1) count++;

            //if there are more than 2 dead end vertices, then a tour is impossible
            if (count > 2) return true;
        }

        //if the graph is not bipartite balanced, then a tour is impossible
        (HashSet<Position> redVertices, HashSet<Position> nonRedVertices) = GetRedAndNonRedVertices(_knight.Position, (new HashSet<Position>(), new HashSet<Position>()));
        if(Math.Abs(redVertices.Count - nonRedVertices.Count) > 1) return true;

        //disconnected graph, if the union of the red and non-red vertices does not equal the total number of vertices, then a tour is impossible
        if (redVertices.Union(nonRedVertices).Count() != _graph.Vertices.Count) return true;
        return false;
    }

    /// <summary>
    /// Attempts to find a complete knight's tour on the current chessboard configuration.
    /// </summary>
    /// <remarks>This method performs a search for a valid knight's tour starting from the knight's current
    /// position. The search is synchronous and may take significant time for large boards or complex
    /// configurations.</remarks>
    /// <returns>A tuple containing a value indicating whether a tour was found and a queue of positions representing the tour
    /// path. The queue is null if no tour exists.</returns>
    public (bool, Queue<Position>?) SearchForTour()
    {
        if(!_isGraphInitialized) InitializeGraph();

        //easy conditions to check if a tour is impossible
        bool isTourImpossible = IsTourImpossible();
        if (isTourImpossible) return (false, null);

        //if we get here, then a tour may be possible, so we perform a depth first search to find it
        //if we are unlucky, this could be an exotic search for a golden goose and a tour may not exist
        Queue<Position> path = new Queue<Position>();
        path.Enqueue(_knight.Position);

        Task<(bool, Queue<Position>?)> task = DepthFirstSearchTask(path, CancellationToken.None);
        Task.WaitAll(task);
        (bool tourFound, Queue<Position>? tourPath) = task.Result;

        return (tourFound, tourPath);
    }
}

