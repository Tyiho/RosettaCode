namespace RosettaCode.Problems.BitwiseIO.Merl
{
	/// <summary>
	/// An ASCII to binary encoder with a bit packing into the converted indary data.
	/// Supports the basic (not extended) printable character set from 0x20 - 0x7e.
	/// Compaction to 7-bits per-character allows for a single additional stored character in the output. (8 packed in the space for 7 unpacked).
	/// </summary>
	public abstract class AsciiToBinaryEncoder
	{
		/// <summary>
		/// Decodes the provided <paramref name="source"/>, returning a result buffer.
		/// Input is commenced from the <paramref name="startPos"/> at the specified <paramref name="startBit"/>
		/// </summary>
		/// <param name="source">The source bytes to be read.</param>
		/// <param name="numCharacters">The desired number of characters to be read from the <paramref name="source"/></param>
		/// <param name="startPos">The position of the starting byte for the encoded result.  0 unless otherwise specified.</param>
		/// <param name="startBit">The position of the starting bit in the starting byte for the encoded result.  0 unless otherwise specified</param>
		/// <returns>
		/// A Span containing the characters read in their original sequence in <paramref name="source"/>.
		/// </returns>
		/// <exception cref="InvalidOperationException">
		/// Thrown if the provided <paramref name="source"/> does not contain sufficient contents after the specificed position for the desired <paramref name="numCharacters"/>
		/// </exception>
		public abstract Span<byte> ReadBytes(Span<byte> source, int numCharacters, int startPos = 0, int startBit = 0);

		/// <summary>
		/// Decodes the provided <paramref name="source"/>, returning the result as a string.
		/// Input is commenced from the <paramref name="startPos"/> at the specified <paramref name="startBit"/>
		/// </summary>
		/// <param name="source">The source bytes to be read.</param>
		/// <param name="numCharacters">The desired number of characters to be read from the <paramref name="source"/></param>
		/// <param name="startPos">The position of the starting byte for the encoded result.  0 unless otherwise specified.</param>
		/// <param name="startBit">The position of the starting bit in the starting byte for the encoded result.  0 unless otherwise specified</param>
		/// <returns>
		/// A string composed of the characters read in their original sequence in <paramref name="source"/>.
		/// </returns>
		/// <exception cref="InvalidOperationException">
		/// Thrown if the provided <paramref name="source"/> does not contain sufficient contents after the specificed position for the desired <paramref name="numCharacters"/>
		/// </exception>
		public abstract string ReadString(Span<byte> source, int numCharacters, int startPos = 0, int startBit = 0);

		/// <summary>
		/// Encodes the provided <paramref name="source"/> string onto the target <paramref name="buffer"/>
		/// starting from the specified <paramref name="startBit"/> position (default o 0).
		/// Data in the last written bit of the last written byte will not be adjusted.
		/// It is recommended to use a zero-initialized buffer.
		/// </summary>
		/// <param name="source">The source bytes to be written.</param>
		/// <param name="buffer">The buffer into which the <paramref name="source"/> will be encoded.</param>
		/// <param name="startPos">The position of the starting byte for the encoded result.  0 unless otherwise specified.</param>
		/// <param name="startBit">The position of the starting bit in the starting byte for the encoded result.  0 unless otherwise specified</param>
		/// <returns>
		/// The number of bits written.
		/// Take this valuea %8 to identify the number of bytes written, 
		/// with the remainder being the potential new starting bit position
		/// </returns>
		/// <exception cref="InvalidOperationException">
		/// Thrown if the provided <paramref name="buffer"/> cannot hold the converted contents after the specificed position.
		/// </exception>
		public abstract long WriteBytes(Span<byte> source, Span<byte> buffer, int startPos = 0, int startBit = 0);

		/// <summary>
		/// Encodes the provided <paramref name="source"/> string onto the target <paramref name="buffer"/>
		/// starting from the specified <paramref name="startBit"/> position (default o 0).
		/// Data in the last written bit of the last written byte will not be adjusted.
		/// It is recommended to use a zero-initialized buffer.
		/// </summary>
		/// <param name="source">The source string. Null or Whitespace-only strings will not be written.</param>
		/// <param name="buffer">The buffer into which the <paramref name="source"/> will be encoded.</param>
		/// <param name="startPos">The position of the starting byte for the encoded result.  0 unless otherwise specified.</param>
		/// <param name="startBit">The position of the starting bit in the starting byte for the encoded result.  0 unless otherwise specified</param>
		/// <returns>
		/// The number of bits written.
		/// Take this valuea %8 to identify the number of bytes written, 
		/// with the remainder being the potential new starting bit position
		/// </returns>
		public abstract long WriteString(string source, Span<byte> buffer, int startPos = 0, int startBit = 0);
	}
}