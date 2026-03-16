using BenchmarkDotNet.Environments;
using RosettaCode.Problems.BitwiseIO.Trystan;

namespace RosettaCodeTest.Problems.BitwiseIO.Trystan;

[TestClass]
public class BitWriter_Test
{
    [TestMethod]
    public void Test_OpenWrite()
    {
        var bitWriter = new BitWriter(FilePath.filePath);
        bitWriter.OpenWrite();
        Assert.IsTrue(bitWriter.IsWriteOpen);

        bitWriter.CloseWrite();
    }

    [TestMethod]
    public void Test_CloseRead()
    {
        var bitWriter = new BitWriter(FilePath.filePath);
        bitWriter.OpenWrite();
        Assert.IsTrue(bitWriter.IsWriteOpen);
        bitWriter.CloseWrite();
        Assert.IsFalse(bitWriter.IsWriteOpen);
    }
}

