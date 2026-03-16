

namespace RosettaCode.Problems.BitwiseIO.Trystan;

public static class FilePath
{
    public static readonly string filePath = "C:\\Users\\Tryst\\Downloads\\rough_ascii.bit";
}

public class RoughAscii
{
    /// <summary>
    /// Stores the file path to the file to be read/written.
    /// This is a hardcoded path and should be changed to the appropriate path on the user's machine.
    /// </summary>
    private string filePath = FilePath.filePath;
    private BitReader _bitReader;
    private BitWriter _bitWriter; 
    public RoughAscii()
    {
       _bitReader = new BitReader(filePath);
       _bitWriter = new BitWriter(filePath);
    }

    public string[] CompressCharsAscii(char[] input)
    {
        string[] bitStrings = new string[input.Length];

        _bitWriter.ClearFile();
        _bitWriter.OpenWrite();
        foreach (char c in input)
        {
            byte asciiValue = (byte)c;

            bool[] bits = new bool[7];


            string bitString = Convert.ToString(asciiValue, 2).PadLeft(7, '0');
            for (int i = 0; i < 7; i++)
            {
                bool bit = bitString.ToCharArray()[i] == 1;
                bits[i] = bit;
            }

            Console.WriteLine(bitString);
            bitStrings[Array.IndexOf(input, c)] = bitString;

            _bitWriter.WriteBits(bits);
        }
        _bitWriter.Flush();
        _bitWriter.CloseWrite();

        return bitStrings;
    }

    public char[] DecompressStringAscii()
    {
        _bitReader.OpenRead();
        List<char> results = new List<char>();
        while (_bitReader.IsReadOpen)
        {
            try
            {
                bool[] bits = new bool[7];
                for (int i = 0; i < 7; i++)
                {
                    bits[i] = _bitReader.ReadBit();
                }
                byte asciiValue = 0;
                for (int i = 6; i >= 0; i--)
                {
                    asciiValue |= (byte)((bits[i] ? 1 : 0) << (6 - i));
                }

                results.Add((char)asciiValue);
                Console.WriteLine((char)asciiValue);

            }
            catch (Exception)
            {
                _bitReader.CloseRead();
            }
        }
        return results.ToArray();
    }
}