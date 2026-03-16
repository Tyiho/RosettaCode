using System;
using System.Collections.Generic;
using System.Text;

namespace RosettaCode.Problems.BitwiseIO.Trystan;

public class BitWriter(string filePath)
{
    public bool IsWriteOpen => _isWriteOpen;

    private readonly bool[] _buffer = new bool[8];
    private int _bufferIndex = 0;
    private FileStream _fileStream;
    private bool _isWriteOpen = false;

    /// <summary>
    ///     Writes a bit to a buffer, and when the buffer is full (8 bits), it writes the byte to the file.
    ///     If the buffer is not full, it will attempt to Flush to the file.
    /// </summary>
    /// <param name="bit">
    ///     The bit to write.
    ///     True for 1, False for 0.
    ///     The bit will be written to the buffer, and when the buffer is full, the buffer will be written to the file as a byte and cleared.
    /// </param>
    public void WriteBit(bool bit)
    {
        if (!_isWriteOpen) throw new Exception("Filestream write must be open.");

        _buffer[_bufferIndex] = bit;
        _bufferIndex++;

        if (_bufferIndex >= 8)
        {
            Flush();
            _bufferIndex = 0;
        }
    }

    public void WriteBits(bool[] bits)
    {
        if(!_isWriteOpen) throw new Exception("Filestream write must be open.");
        foreach (bool bit in bits)
        {
            WriteBit(bit);
        }
    }

    public void Flush()
    {
        if (!_isWriteOpen) throw new Exception("Filestream write must be open.");


        for (int bi = _bufferIndex; bi < 8; bi++)
        {
            _buffer[bi] = false;
        }

        byte byteToWrite = 0;

        //please say I did this correctly
        for (int i = 7; i >= 0; i--)
        {
            byteToWrite += (byte)((_buffer[i] ? 1 : 0) << (7 - i));
        }

        Console.WriteLine(byteToWrite);
        try
        {
            _fileStream.WriteByte(byteToWrite);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while writing to the File: {ex.Message}");
        }

        //clear the buffer
        for (int i = 0; i < 8; i++)
        {
            _buffer[i] = false;
        }
    }

    public void ClearFile()
    {
        if(_isWriteOpen) throw new Exception("Cannot clear file while filestream is open.");
        _fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        _fileStream.Close();
        _isWriteOpen = false;
    }

    public void OpenWrite()
    {
        _fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write);
        _isWriteOpen = true;
    }

    public void CloseWrite()
    {
        _fileStream.Close();
        _isWriteOpen = false;
    }
}