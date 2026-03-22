using System.IO.Abstractions.TestingHelpers;
using RosettaCode.Problems.BitwiseIO.Trystan;

namespace RosettaCodeTest.Problems.BitwiseIO.Trystan;

[TestClass]
public class BitReader_Test
{
    [TestMethod]
    public void Test_OpenRead()
    {
        FilePath.FileSystem = new MockFileSystem();
        FilePath.FileSystem.Directory.CreateDirectory(FilePath.directoryPath);
        FilePath.FileSystem.File.WriteAllBytes(FilePath.filePath + "d", new byte[] { 00000000});

        var bitReader = new BitReader(FilePath.filePath + "d");
        bitReader.OpenRead();
        Assert.IsTrue(bitReader.IsReadOpen);

        bitReader.CloseRead();
    }

    [TestMethod]
    public void Test_CloseRead()
    {
        FilePath.FileSystem = new MockFileSystem();
        FilePath.FileSystem.Directory.CreateDirectory(FilePath.directoryPath);
        FilePath.FileSystem.File.WriteAllBytes(FilePath.filePath + "f", new byte[] { 00000000 });


        var bitReader = new BitReader(FilePath.filePath + "f");
        bitReader.OpenRead();
        bitReader.CloseRead();
        Assert.IsFalse(bitReader.IsReadOpen);
    }
}

