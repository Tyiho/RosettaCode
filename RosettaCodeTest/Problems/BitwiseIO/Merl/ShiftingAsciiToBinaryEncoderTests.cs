using System.Text;

using FluentAssertions;

using RosettaCode.Problems.BitwiseIO.Merl;

namespace RosettaCodeTest.Problems.BitwiseIO.Merl
{
	[TestClass]
	public class ShiftingAsciiToBinaryEncoderTests
	{
		public static IEnumerable<object[]> TestStrings()
		{
			//Parameters Are:
			//	the test string,
			//	the string as a byte[],
			//	the expected compressed bytes,
			//	the expected number of bytes written

			string testString = "STRING";
			yield return new object[]
			{
				testString,
				Encoding.UTF8.GetBytes(testString),
				new byte[]
						{
							0b01100110,
							0b11010001,
							0b10010010,
							0b10010101,
							0b11001001,
							0b11000000
						},
				testString.Length * 7
			};

			testString = "ABCDEFGH"; //full block of 8 characters, test the complete cycle
			yield return new object[]
			{
				testString,
				Encoding.UTF8.GetBytes(testString),
				new byte[]
						{
							0b01000010,
							0b10001001,
							0b00011010,
							0b01000100,
							0b10101001,
							0b10010011,
							0b10101000
						},
				testString.Length * 7
			};

			testString = "ABCDEFGHI"; //one more than a full block
			yield return new object[]
			{
				testString,
				Encoding.UTF8.GetBytes(testString),
				new byte[]
						{
							0b01000010,
							0b10001001,
							0b00011010,
							0b01000100,
							0b10101001,
							0b10010011,
							0b10101000,
							0b01010010
						},
				testString.Length * 7
			};
		}


		[TestMethod]
		[DynamicData(nameof(TestStrings))]
		public void WriteString_ProperlyConverts_TestString(string testString, byte[] sourceBytes, byte[] expectedBytes, long expectedBitsWritten)
		{
			//Using ArraySegment as this could represent part of a buffer, but it is where the encoder should work.

			//Arrange
			ArraySegment<byte> actualBytes = new byte[(long)Math.Ceiling(expectedBitsWritten / 8D)];

			var encoder = new ShiftingAsciiToBinaryEncoder();

			//Act
			encoder.WriteString(testString, actualBytes);

			//Assert
			actualBytes.Should().Equal(expectedBytes);
		}


		[TestMethod]
		[DynamicData(nameof(TestStrings))]
		public void WriteString_PushedProperNumberOfBits_ForTheTestString(string testString, byte[] sourceBytes, byte[] expectedBytes, long expectedBitsWritten)
		{
			//Using ArraySegment as this could represent part of a buffer, but it is where the encoder should work.

			//Arrange
			ArraySegment<byte> actualBytes = new byte[(long)Math.Ceiling(expectedBitsWritten / 8D)];

			var encoder = new ShiftingAsciiToBinaryEncoder();

			//Act
			var actualBytesWritten = encoder.WriteString(testString, actualBytes);

			//Assert
			actualBytesWritten.Should().Be(expectedBitsWritten);
		}


		[TestMethod]
		[DynamicData(nameof(TestStrings))]
		public void ReadBytes_ReadsExpectedBytes_FromTheTestBuffer(string expectedString, byte[] expectedBytes, byte[] buffer, long expectedBitsRead)
		{
			//Using ArraySegment as this could represent part of a buffer, but it is where the encoder should work.

			//Arrange
			ArraySegment<byte> testSegment = new(buffer);

			var encoder = new ShiftingAsciiToBinaryEncoder();

			//Act
			var actualBytesRead = encoder.ReadBytes(testSegment, expectedString.Length);

			//Assert
			actualBytesRead.ToArray().Should().Equal(expectedBytes);
		}


		[TestMethod]
		[DynamicData(nameof(TestStrings))]
		public void ReadString_ReadsExpectedString_FromTheTestBuffer(string expectedString, byte[] expectedBytes, byte[] buffer, long expectedBitsRead)
		{
			//Using ArraySegment as this could represent part of a buffer, but it is where the encoder should work.

			//Arrange
			ArraySegment<byte> testSegment = new(buffer);

			var encoder = new ShiftingAsciiToBinaryEncoder();

			//Act
			var actualString = encoder.ReadString(testSegment, expectedString.Length);

			//Assert
			actualString.Should().Be(expectedString);
		}
	}
}
