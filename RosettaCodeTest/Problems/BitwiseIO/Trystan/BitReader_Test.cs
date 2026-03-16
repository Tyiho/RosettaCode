using RosettaCode.Problems.BitwiseIO.Trystan;

namespace RosettaCodeTest.Problems.BitwiseIO.Trystan;

[TestClass]
public class BitReader_Test
{
    [TestMethod]
    public void Test_OpenRead()
    {
        var bitReader = new BitReader(FilePath.filePath);
        bitReader.OpenRead();
        Assert.IsTrue(bitReader.IsReadOpen);

        bitReader.CloseRead();
    }

    [TestMethod]
    public void Test_CloseRead()
    {
        var bitReader = new BitReader(FilePath.filePath);
        bitReader.OpenRead();
        bitReader.CloseRead();
        Assert.IsFalse(bitReader.IsReadOpen);
    }
}

