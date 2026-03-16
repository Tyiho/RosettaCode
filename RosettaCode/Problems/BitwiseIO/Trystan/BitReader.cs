using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Text;

namespace RosettaCode.Problems.BitwiseIO.Trystan;

public class BitReader(string filePath) : IDisposable
{
    public bool IsReadOpen => _isReadOpen;

    private byte _buffer;
    private int _offset;
    private bool _isReadOpen = false;
    private FileSystemStream _fileStream;
    //private FileStream _fileStream;

    IFileSystem FileSystem { get; } = new FileSystem(); 

    public bool ReadBit()
    {
        if(!_isReadOpen) throw new Exception("Filestream read must be open.");
        string bitString = Convert.ToString(_buffer, 2).PadLeft(8, '0');
        bool bit = bitString.ToCharArray()[_offset] == '1';
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
        _fileStream = FilePath.FileSystem.FileStream.New(FilePath.filePath, FileMode.Open, FileAccess.Read);
        if (!_fileStream.CanRead) return;
        var temp = _fileStream.ReadByte();
        if (temp == -1)
        {
            CloseRead();
            return;
        }
        _buffer = (byte)temp;
        _offset = 0;
        _isReadOpen = true;
    }

    public void CloseRead()
    {
        if (_isReadOpen)
        {
            _fileStream.Close();
            _isReadOpen = false;
        }
    }

    public void Dispose()
    {
        CloseRead();
    }
}