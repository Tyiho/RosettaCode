using System;
using System.Collections.Generic;
using System.Text;

using BenchmarkDotNet.Attributes;

namespace RosettaCode.Problems.BitwiseIO.Merl
{
	[MemoryDiagnoser]
	public class BitwiseIoBenchmarks
	{
		private const string TEST_STRING = "TestString";

		private byte[] m_DataBuffer = [];


		[GlobalSetup]
		public void GlobalSetup()
		{
			if (m_DataBuffer.Length == 0)
			{
				//need to build the data buffer for the test string
				m_DataBuffer = new byte[TEST_STRING.Length];

				//and put the compressed data into the buffer by using one of our implementations.
				//It has been unit tested to generate the correct bytes.
				var encoder = new ShiftingAsciiToBinaryEncoder();
				encoder.WriteString(TEST_STRING, m_DataBuffer);
			}
		}

		[Benchmark]
		public void ShiftingEncoder_Write()
		{
			var encoder = new ShiftingAsciiToBinaryEncoder();

			string testString = "TESTSTRING";

			_ = encoder.WriteString(testString, new byte[testString.Length]);
		}

		[Benchmark]
		public void MaskedEncoder_Write()
		{
			var encoder = new MaskedAsciiToBinaryEncoder();

			string testString = "TESTSTRING";

			_ = encoder.WriteString(testString, new byte[testString.Length]);
		}

		[Benchmark]
		public void ShiftingEncoder_Read()
		{
			var encoder = new ShiftingAsciiToBinaryEncoder();

			_ = encoder.ReadString(m_DataBuffer, TEST_STRING.Length);
		}

		[Benchmark]
		public void MaskedEncoder_Read()
		{
			var encoder = new MaskedAsciiToBinaryEncoder();

			_ = encoder.ReadString(m_DataBuffer, TEST_STRING.Length);
		}
	}
}
