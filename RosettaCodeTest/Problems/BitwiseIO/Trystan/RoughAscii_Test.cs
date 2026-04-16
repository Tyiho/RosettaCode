using RosettaCode.Problems.BitwiseIO.Trystan;
using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Text;

namespace RosettaCodeTest.Problems.BitwiseIO.Trystan;

[TestClass]
public class RoughAscii_Test
{
    [TestMethod]
    public void CompressCharsAscii_Result_HasLength()
    {
        FilePath.FileSystem = new MockFileSystem();
        FilePath.FileSystem.Directory.CreateDirectory(FilePath.directoryPath);
        FilePath.FileSystem.File.WriteAllBytes(FilePath.filePath+"a", new byte[] { 00000000 });

        var roughAscii = new RoughAscii(FilePath.filePath + "a");
        char[] input = ['A', 'B', 'C'];
        string[] bitStrings = roughAscii.CompressCharsAscii(input);

        Assert.HasCount(3,bitStrings);
    }

    [TestMethod]
    public void CompressCharsAscii_Result_ContainsExpectedBitStrings()
    {
        FilePath.FileSystem = new MockFileSystem();
        FilePath.FileSystem.Directory.CreateDirectory(FilePath.directoryPath);
        FilePath.FileSystem.File.WriteAllBytes(FilePath.filePath+"b", new byte[] { 00000000 });

        var roughAscii = new RoughAscii(FilePath.filePath + "b");
        char[] input = ['A', 'B', 'C'];
        string[] bitStrings = roughAscii.CompressCharsAscii(input);
        Assert.IsTrue(bitStrings.Contains("1000001")); // 'A' in 7-bit ASCII
        Assert.IsTrue(bitStrings.Contains("1000010")); // 'B' in 7-bit ASCII
        Assert.IsTrue(bitStrings.Contains("1000011")); // 'C' in 7-bit ASCII
    }
     
    [TestMethod]
    public void DecompressStringAscii_Result_MatchesOriginalInput()
    {
        FilePath.FileSystem = new MockFileSystem();
        FilePath.FileSystem.Directory.CreateDirectory(FilePath.directoryPath);
        FilePath.FileSystem.File.WriteAllBytes(FilePath.filePath+"c", new byte[] { 00000000 });

        var roughAscii = new RoughAscii(FilePath.filePath + "c");
        char[] input = [ 'A', 'B', 'C' ];
        roughAscii.CompressCharsAscii(input);

        Console.WriteLine("Break, now decompressing");

        char[] output = roughAscii.DecompressStringAscii();
        for (int i = 0; i < 3; i++)
        {
            Assert.AreEqual(input[i], output[i]);
        }
    }
}