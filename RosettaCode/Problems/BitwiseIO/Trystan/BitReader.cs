using System;
using System.Collections.Generic;
using System.Text;

namespace RosettaCode.Problems.BitwiseIO.Trystan;

public class BitReader(string filePath)
{
    public bool IsReadOpen => _isReadOpen;

    private byte _buffer;
    private int _offset;
    private bool _isReadOpen = false;
    private FileStream _fileStream;

    public bool ReadBit()
    {
        if(!_isReadOpen) throw new Exception("Filestream read must be open.");
        string bitString = Convert.ToString(_buffer, 2).PadLeft(8, '0');
        Console.WriteLine(bitString);
        bool bit = bitString.ToCharArray()[_offset] == 1;
        _offset++;
        if (_offset >= 8)
        {
            int result = (int)_fileStream.ReadByte();
            if(result == -1) CloseRead();
            _buffer = (byte)result;
            _offset = 0;
        }

        return bit;
    }

    public void OpenRead()
    {
        _fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        if (!_fileStream.CanRead) return;
        _buffer = (byte)_fileStream.ReadByte();
        _offset = 0;
        _isReadOpen = true;
    }

    public void CloseRead()
    {
        _fileStream.Close();
        _isReadOpen = false;
    }
}