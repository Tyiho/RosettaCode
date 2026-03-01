using System;
using System.Collections.Generic;
using System.Text;

namespace RosettaCode.Problems.BitwiseIO.Merl
{
	public class ShiftingAsciiToBinaryEncoder : AsciiToBinaryEncoder
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
		public override Span<byte> ReadBytes(Span<byte> source, int numCharacters, int startPos = 0, int startBit = 0)
		{
			//bytes needed = start + (encodedBitCount + starting bit) as whole number of bytes
			var bytesNeeded = startPos + Math.Ceiling((numCharacters * 7 + startBit) / 8d);

			if (source.Length < bytesNeeded)
			{
				throw new InvalidOperationException($"Target buffer of size ({source.Length}) cannot hold the necessary data from the given starting position (byte:{startPos} bit{startBit}.  Would need at least {bytesNeeded} bytes in total.");
			}


			Span<byte> buffer = new byte[numCharacters];

			for (int charPos = 0; charPos < numCharacters; charPos++)
			{
				byte value = 0;

				if (startBit == 0)
				{
					//simple shift to the right, dropping the last bit
					//could be a shift and mask, but the result would be the same.
					//first bit is always 0
					value = (byte)(source[startPos] >>> 1);
				}

				if (startBit == 1)
				{
					//drop the high bit, take the rest from the original position
					//truncate the shifted bits back to a byte or we can't drop the 
					//unwanted high end.
					value = (byte)((byte)((source[startPos]) << 1) >>> 1);
				}

				if (startBit > 1)
				{
					//bits at current position become the higher order bits
					value = (byte)(source[startPos] << startBit);

					//while the start of the next position fill in the rest
					//this will copy an extra bit
					value |= (byte)(source[++startPos] >>> 8 - startBit);

					//finally, shift the constructed bits back into position,
					//dropping the extra bit copied
					value = (byte)(value >> 1);
				}

				//move back to its ASCII value
				buffer[charPos] = (byte)(value + 0x20);

				//advance starting bit position
				startBit = (startBit + 7) % 8;

				//if we're at the start of a byte, advance position
				if (startBit == 0) startPos++;
			}

			return buffer;
		}

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
		public override string ReadString(Span<byte> source, int numCharacters, int startPos = 0, int startBit = 0)
		{
			var bytes = ReadBytes(source, numCharacters, startPos, startBit);

			var builder = new StringBuilder();

			foreach (byte character in bytes)
			{
				builder.Append((char)character);
			}

			return builder.ToString();
		}

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
		public override long WriteBytes(Span<byte> source, Span<byte> buffer, int startPos = 0, int startBit = 0)
		{
			if (source.Length == 0) return 0;

			//total expected bit count:
			var sourceBitCount = source.Length * 7;

			//bytes needed = start + (encodedBitCount + starting bit) as whole number of bytes
			var bytesNeeded = startPos + Math.Ceiling((sourceBitCount + startBit) / 8d);

			if (buffer.Length < bytesNeeded)
			{
				throw new InvalidOperationException($"Target buffer of size ({buffer.Length}) cannot hold the converted source from the given starting position (byte:{startPos} bit:{startBit}.  Need at least {bytesNeeded} bytes in total.");
			}

			long bytesWritten = 0;

			for(int charPos = 0; charPos < source.Length; charPos++) 
			{ 
				//re-base so that our first char value = 0;
				byte valToWrite = (byte)(source[charPos] - 0x20);

				if (startBit == 0)
				{
					buffer[startPos] |= (byte)(valToWrite << 1);
				}

				if (startBit == 1)
				{
					buffer[startPos] |= valToWrite;
				}

				if (startBit > 1)
				{
					//right shift is enough to elimite bits and pad with 0
					buffer[startPos] |= (byte)(valToWrite >>> startBit - 1);

					//and left shift is enought to elimintate the bits we don't want
					buffer[++startPos] |= (byte)(valToWrite << (8 - startBit) + 1);
				}

				startBit = (startBit + 7) % 8;

				if (startBit == 0) startPos++;

				bytesWritten += 7;
			}

			return bytesWritten;
		}


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
		public override long WriteString(string source, Span<byte> buffer, int startPos = 0, int startBit = 0) =>
			string.IsNullOrWhiteSpace(source)
				? 0
				: WriteBytes(Encoding.UTF8.GetBytes(source), buffer, startPos, startBit);
	}
}
