using BenchmarkDotNet.Environments;
using RosettaCode.Problems.BitwiseIO.Trystan;
using System.IO.Abstractions.TestingHelpers;

namespace RosettaCodeTest.Problems.BitwiseIO.Trystan;

[TestClass]
public class BitWriter_Test
{
    [TestMethod]
    public void Test_OpenWrite()
    {
        FilePath.FileSystem = new MockFileSystem();
        FilePath.FileSystem.Directory.CreateDirectory(FilePath.directoryPath);
        FilePath.FileSystem.File.WriteAllBytes(FilePath.filePath, new byte[] { 00000000 });

        var bitWriter = new BitWriter(FilePath.filePath);
        bitWriter.OpenWrite();
        Assert.IsTrue(bitWriter.IsWriteOpen);

        bitWriter.CloseWrite();
    }

    [TestMethod]
    public void Test_CloseRead()
    {
        FilePath.FileSystem = new MockFileSystem();
        FilePath.FileSystem.Directory.CreateDirectory(FilePath.directoryPath);
        FilePath.FileSystem.File.WriteAllBytes(FilePath.filePath, new byte[] { 00000000 });

        var bitWriter = new BitWriter(FilePath.filePath);
        bitWriter.OpenWrite();
        Assert.IsTrue(bitWriter.IsWriteOpen);
        bitWriter.CloseWrite();
        Assert.IsFalse(bitWriter.IsWriteOpen);
    }
}

