using RosettaCode.Problems.KnightsTour.Merl;

namespace RosettaCode.Problems.KnightsPath.Merl
{
	public static class ExtensionMethods
	{
		public static string ToAlgebraicNotation(this IntVector2 position)	=> 
			$"{(char)('a' + position.X)}{position.Y + 1}";

		public static string ToRouteString(this List<PiecePosition> route) =>
			route.Count == 0 ? "" : string.Join(" -> ", route.Select(pm => pm.Position.ToAlgebraicNotation()));

		public static void PrintToConsole(this ChessBoard board, List<PiecePosition> path, ChessPiece piece) =>
			Console.WriteLine(board.RenderBoard(path, piece).ToString());

		public static int NumberOfDigits(this int value) =>
			value switch
			{
				< 10 => 1,
				< 100 => 2,
				< 1000 => 3,
				< 10000 => 4,
				< 100000 => 5,

				//this isn't very performant, but it covers the rest. 
				//we're not really expecting a board with a position count
				//anwhere near this large.
				_ => value.ToString().Length
			};
	}
}
