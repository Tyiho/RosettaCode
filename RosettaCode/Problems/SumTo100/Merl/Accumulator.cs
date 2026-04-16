using System;
using System.Collections.Generic;
using System.Text;

namespace RosettaCode.Problems.SumTo100.Merl
{
	public struct Accumulator : IEquatable<Accumulator>, IComparable<Accumulator>, IComparable<int>
	{
		#region Public Fields

		public int Value;
		public bool IsNaN;

		#endregion

		#region Constructors

		public Accumulator(int value) : this(value, false) { }

		public Accumulator(int value, bool isNaN = false)
		{
			Value = value;
			IsNaN = isNaN;
		}

		#endregion

		#region Implementation of IEquatable<Accumulator>

		public bool Equals(Accumulator other)
		{
			return Value == other.Value && IsNaN == other.IsNaN;
		}

		#endregion

		#region Impelementation of IComparable<Accumulator>

		public int CompareTo(Accumulator other)
		{
			//both NaN, same position
			if (IsNaN && other.IsNaN) return 0;

			//this is a value, other is NaN, this sorts in first
			if (other.IsNaN) return -1;

			//both value, compare the value.
			return Value.CompareTo(other.Value);
		}

		#endregion

		#region Implementation of IComparable<int>

		public int CompareTo(int other)
		{
			if (IsNaN) return 1; //NaN follow

			return other.CompareTo(Value);
		}

		#endregion

		#region Coersion Operators

		/// <summary>
		/// Provides the current accumulator value as an integer.
		/// </summary>
		/// <param name="acc"></param>
		/// <exception cref="NotFiniteNumberException"
		public static implicit operator int(Accumulator acc) =>
			acc.IsNaN ?
				throw new NotFiniteNumberException("Cannot convert a non-number to an integer.") :
				acc.Value;

		/// <summary>
		/// Creates a new accumulator with a seed value.
		/// Implicit conversion cannot also accept a NaN indicator and is presumed, therefore, to be false.
		/// </summary>
		/// <param name="currentValue"></param>
		public static implicit operator Accumulator(int currentValue) => new Accumulator(currentValue);

		#endregion

		public override string ToString() => IsNaN ? "NaN" : Value.ToString();
	}
}
