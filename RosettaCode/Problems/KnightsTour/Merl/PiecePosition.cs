using System.Diagnostics.CodeAnalysis;

using RosettaCode.Problems.KnightsPath.Merl;

namespace RosettaCode.Problems.KnightsTour.Merl
{
	public struct PiecePosition : IEquatable<PiecePosition>
	{
		public static IntVector2 InitialPlacement = new IntVector2(0, 0);
		public static PiecePosition InvalidPosition = new(new IntVector2(-1, -1), new(0, 0));


		public PiecePosition(IntVector2 position, IntVector2 moveTaken)
		{
			Move = moveTaken;
			Position = position;
			UntestedMoves = [];
		}

		public PiecePosition(IntVector2 fromPosition, IntVector2 move, IEnumerable<IntVector2> possibleMoves) : this(fromPosition, move)
		{
			UntestedMoves = new(possibleMoves);
		}

		public IntVector2 Position { get; }

		public IntVector2 Move { get; }

		/// <summary>
		/// For simplicty, reports the previous position that taking the recorded move produced this position
		/// </summary>
		private IntVector2 FromPosition => Position - Move;

		public override string ToString() => $"{(Move != InitialPlacement ? FromPosition.ToAlgebraicNotation() : "S")} ({Move}) -> {Position.ToAlgebraicNotation()}";

		public Stack<IntVector2> UntestedMoves { get; }

		public PiecePosition EvaluateMove(IntVector2 move) => new(Position + move, move);


		public bool Equals(PiecePosition other) => Position == other.Position && Move == other.Move;

		public override bool Equals([NotNullWhen(true)] object? obj) => obj is PiecePosition other && Equals(other);

		public override int GetHashCode() => HashCode.Combine(Position, Move);
	}
}
