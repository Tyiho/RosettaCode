using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Primitives;

namespace RosettaCode.Problems.BitwiseIO.Merl
{
	/*
	 * Notes:
	 * I thought that maybe this approach could save a little work when we write out bits.
	 * It may be more complicated than it is worth.  
	 */

	/// <summary>
	/// An ASCII to binary encoder with a bit packing into the converted indary data.
	/// Supports the basic (not extended) printable character set from 0x20 - 0x7e.
	/// Compaction to 7-bits per-character allows for a single additional stored character in the output. (8 packed in the space for 7 unpacked).
	/// This implementation uses a set of bit masks to pull out the bits from the source bytes instead of additional shifting operations.
	/// </summary>
	public class MaskedAsciiToBinaryEncoder : AsciiToBinaryEncoder
	{
		#region Static Members

		//highest order bit is unused
		//index represents the starting bit position
		//which means we take the first (7 - start)
		private static byte[] s_FirstPartMasks =
			[
				0b01111111,
				0b01111111,
				0b01111110,
				0b01111100,
				0b01111000,
				0b01110000,
				0b01100000,
				0b01000000
			];

		//and then the 2nd part is the remainder of the bits
		//not taken in the first part
		private static byte[] s_SecondPartMasks =
			[
				0b00000000,
				0b00000000,
				0b00000001,
				0b00000011,
				0b00000111,
				0b00001111,
				0b00011111,
				0b00111111
			];

		#endregion


		#region Overrides of AsciiToBinaryEncoder

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
		/// <exception cref="InvalidOperationException">
		/// Thrown if the provided <paramref name="buffer"/> cannot hold the converted contents after the specificed position.
		/// </exception>
		public override long WriteString(string source, Span<byte> buffer, int startPos = 0, int startBit = 0) =>
			string.IsNullOrWhiteSpace(source)
				? 0 
				: WriteBytes(Encoding.UTF8.GetBytes(source), buffer, startPos, startBit);

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
				throw new InvalidOperationException($"Target buffer of size ({buffer.Length}) cannot hold the converted source from the given starting position (byte:{startPos} bit{startBit}.  Need at least {bytesNeeded} bytes in total.");
			}

			long bytesWritten = 0;

			for (int charPos = 0; charPos < source.Length; charPos++ ) 
			{
				//shift our first acceptable character's value to zero,
				//and shift the bits one to the left to discard the unused highest bit
				byte valToWrite = (byte)(source[charPos] - 0x20);

				var maskIndex = startBit % 8;

				//split value to cross byte boundaries
				var firstChunk = (byte)(valToWrite & s_FirstPartMasks[maskIndex]);

				//position the first chunk
				if (startBit == 0) 
				{
					//shift left as we don't use the first bit
					buffer[startPos] |= (byte)(firstChunk << 1);
				}

				if (startBit == 1)
				{
					//in-place, first bit is always 0 due to the rebase.
					buffer[startPos] |= firstChunk;
				}

				//not able to be contained in the current byte
				if (startBit > 1)
				{
					var secondChunk = (byte)(valToWrite & s_SecondPartMasks[maskIndex]);

					//shift right
					buffer[startPos] |= (byte)(firstChunk >>> startBit - 1);

					//remainder always starts of the next full byte and we discard the high order bit
					buffer[++startPos] |= (byte)(secondChunk << (8 - startBit) + 1);
				}

				//advance starting bit position
				startBit = (startBit + 7) % 8;

				//if we're starting at the beginning of a byte, 
				//move to the next position.
				if (startBit == 0) startPos++;

				bytesWritten += 7;
			}

			//bytes written should always be the number of characters * 7
			return bytesWritten;
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

			foreach(byte character in bytes) 
			{ 
				builder.Append((char)character);
			}

			return builder.ToString();
		}

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
					//drop the high bit, take the rest
					value = (byte)(source[startPos] & s_FirstPartMasks[startBit]);
				}

				if (startBit > 1)
				{
					//remaining bits at current position become the higher order bits
					value = (byte)((source[startPos] << startBit - 1) & s_FirstPartMasks[startBit]);

					//while the start of the next position fill in the rest
					value |= (byte)((source[++startPos] >>> 1 + (8 - startBit)) & s_SecondPartMasks[startBit]);
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

		#endregion
	}
}
