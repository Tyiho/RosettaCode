using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace RosettaCode.Problems.KnightsTour.Merl
{
	/// <summary>
	/// Thisis hopefully a more space conservative version of IntVector2 (vs Vector2) in many cases.
	/// For this solution, we're not considering more than simple +/- operations.
	/// </summary>
	/// <typeparam name="int"></typeparam>
	public struct IntVector2 : IEquatable<IntVector2>
	{
		public int X;
		public int Y;

		public IntVector2(int x, int y) => (X, Y) = (x, y);

		public static IntVector2 operator +(IntVector2 left, IntVector2 right) =>
			new IntVector2(left.X + right.X, left.Y + right.Y);

		public static IntVector2 operator -(IntVector2 left, IntVector2 right) =>
			new IntVector2(left.X - right.X, left.Y - right.Y);


		public bool Equals(IntVector2 other) =>	X == other.X && Y == other.Y;

		public override bool Equals([NotNullWhen(true)] object? obj) => obj is IntVector2 other && Equals(other);

		public override int GetHashCode() => HashCode.Combine(X, Y);


		public static bool operator ==(IntVector2 left, IntVector2 right) => left.X == right.X && left.Y == right.Y;
		public static bool operator !=(IntVector2 left, IntVector2 right) => left.X != right.X && left.Y != right.Y;

		/// <summary>
		/// Promotes an <see cref="IntVector2"/> to a <see cref="Vector2"/>
		/// </summary>
		/// <param name="vector"
		public static implicit operator Vector2(IntVector2 vector) => new Vector2(vector.X, vector.Y);

		/// <summary>
		/// Coverts a Vector2 to an IntVector2 by directly casting the X and Y components to integers
		/// </summary>
		/// <param name="vector"></param>
		public static explicit operator IntVector2(Vector2 vector) => new((int)vector.X, (int)vector.Y);
	}
}
