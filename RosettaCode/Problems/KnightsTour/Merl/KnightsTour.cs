using RosettaCode.Problems.KnightsTour.Merl;

namespace RosettaCode.Problems.KnightsPath.Merl
{
	public class KnightsTour
	{
		/* 
		 * Availae Positions marked,
		 * Spaces are occupied.

		  0 0 0
		  0   0 0 
		  0 0 0 0 0 0 0
		0 0 0     0   0
		0   0     0 0 0
		1 0 0 0 0 0 0
			0 0   0
			  0 0 0
		 */
		public static IntVector2[] HolyTourExclusions = [
					new IntVector2(0,7), new IntVector2(4,7), new IntVector2(5,7), new IntVector2(6,7), new IntVector2(7,7),
					new IntVector2(0,6), new IntVector2(2,6), new IntVector2(5,6), new IntVector2(6,6), new IntVector2(7,6),
					new IntVector2(0,5),
					new IntVector2(3,4), new IntVector2(4,4), new IntVector2(6,4),
					new IntVector2(1,3), new IntVector2(3,3), new IntVector2(4,3),
					new IntVector2(7,2),
					new IntVector2(0,1), new IntVector2(1,1), new IntVector2(4,1), new IntVector2(6,1), new IntVector2(7,1),
					new IntVector2(0,0), new IntVector2(1,0), new IntVector2(2,0), new IntVector2(6,0), new IntVector2(7,0)
				];

		public void SolveKnightsTour()
		{
			bool routeFound = Evaluate(8, 8, new Knight(), new IntVector2(0, 0), [], out var route);

			Console.WriteLine($"Route found? {routeFound}");

			if (routeFound)
				Console.WriteLine(route.ToRouteString());
		}

		public void EvaluateKnightsTour()
		{
			//visit all spaces on the standard chess board, nothing is occupied
			Evaluate(8, 8, new Knight(), new IntVector2(0, 0), [], out var route);
		}


		public void SolveHolyTour()
		{
			IntVector2 startingPosition = new IntVector2(0, 2);

			bool routeFound = Evaluate(8, 8, new Knight(), startingPosition, HolyTourExclusions, out var route);

			Console.WriteLine($"Route found? {routeFound}");

			if (routeFound)
				Console.WriteLine(route.ToRouteString());
		}

		public void EvaluateHolyTour()
		{
			IntVector2 startingPosition = new IntVector2(0, 2);

			Evaluate(8, 8, new Knight(), startingPosition, HolyTourExclusions, out var route);
		}

		public void SolveRooksTour()
		{
			IntVector2 startingPosition = new IntVector2(0, 2);

			bool routeFound = Evaluate(8, 8, new Rook(), startingPosition, HolyTourExclusions, out var route);

			Console.WriteLine($"Route found? {routeFound}");

			if (routeFound)
				Console.WriteLine(route.ToRouteString());
		}


		public void EvaluateRooksTour()
		{
			//visit all spaces on the standard chess board, nothing is occupied
			Evaluate(8, 8, new Rook(), new IntVector2(0, 0), [], out var route);
		}


		public void SolveQueensTour()
		{
			IntVector2 startingPosition = new IntVector2(0, 2);

			bool routeFound = Evaluate(8, 8, new Queen(), startingPosition, HolyTourExclusions, out var route);

			Console.WriteLine($"Route found? {routeFound}");

			if (routeFound)
				Console.WriteLine(route.ToRouteString());
		}

		public void EvaluateQueensTour()
		{
			//visit all spaces on the standard chess board, nothing is occupied
			Evaluate(8, 8, new Queen(), new IntVector2(0, 0), [], out var route);
		}



		public bool Evaluate(int width, int height, ChessPiece piece, IntVector2 startingPosition, IntVector2[] unavailablePositions, out List<PiecePosition> route)
		{
			List<PiecePosition> path = new();
			PiecePosition currentMove;

			ChessBoard board = new(width, height, unavailablePositions);

			//initial positoin for the piece
			piece.SetPosition(startingPosition);

			//also counts as a visitation 
			board.Visit(startingPosition);

			//and a step along the path.
			var possibleMoves = piece.GetMoves(board);
			currentMove = new PiecePosition(startingPosition, PiecePosition.InitialPlacement, possibleMoves);

			path.Add(currentMove);

			//Console.WriteLine("Starting Board:");
			//board.PrintToConsole(path, piece);

			/* -- just print the initial board:
			route = [];
			return false;
			*/


			//really 2 ways we can end.
			//1) we've run out of moves
			//2) we've found a path that covers the board.
			while (!board.HaveAllPositionsBeenVisited() && path.Count > 0)
			{
				//did we run out of moves at the current position?
				if (currentMove.UntestedMoves.Count == 0)
				{
					//then we have technically backtracked and need to remove the results of the previous move(s)
					var lastMove = currentMove;

					path.Remove(currentMove);

					if (path.Count > 0)
					{
						currentMove = path[^1];
						
						board.ClearVisit(lastMove.Position);

						//Console.WriteLine($"[{path.Count}] Backtrack from {lastMove.Position.ToAlgebraicNotation()} to {currentMove.Position.ToAlgebraicNotation()}");

						//Console.WriteLine($"Backtracked Path: {path.ToRouteString()}");

						piece.SetPosition(currentMove.Position);
						//Console.WriteLine($"Piece Reloacted To: {piece.CurrentPosition.ToAlgebraicNotation()}");

						//board.PrintToConsole(path, piece);
					}
					else
					{
						//Console.WriteLine("Back to Start.");
					}
				}
				else
				{
					IntVector2 candidate = currentMove.UntestedMoves.Pop();

					PiecePosition nextMove = currentMove.EvaluateMove(candidate);

					//we don't visit the same space again
					if (!board.HasBeenVisited(nextMove.Position))
					{
						//while the new position should be the same as what was evaluated,
						//we will still take the value that the knight believes it is now locating.
						if (piece.TryTakeMove(nextMove.Move, board, out var unused))
						{
							//Console.WriteLine($"[{path.Count}] Executing {nextMove}");
							currentMove = nextMove;

							//mark the visitation
							board.Visit(piece.CurrentPosition);

							//record the move as part of the path to travel
							path.Add(nextMove);

							//populate next set of moves from the new position
							piece.GetMoves(board)
								 .Where(move => !board.HasBeenVisited(piece.EvaluateMove(move))).ToList()
								 .ForEach(nextMove.UntestedMoves.Push);

							//Console.WriteLine($"Current Path: {path.ToRouteString()}");
							//board.PrintToConsole(path, piece);
						}
					}
					else
					{
						//Console.WriteLine($"[{path.Count}] Position already visited: {nextMove.Position.ToAlgebraicNotation()}");
					}

					
				}
			}


			//Console.WriteLine("Evaluation Completed.  Final Board:");
			//board.PrintToConsole(path, piece);

			//send back the route in the order taken
			route = path;

			//and return if we successfully visited all available board positions.
			return board.HaveAllPositionsBeenVisited();
		}
	}
}
