using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace RosettaCode.Problems.KnightsTour.Trystan.Game;

public struct Position
{
    public int X;
    public int Y;

    public readonly override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public readonly override bool Equals([NotNullWhen(true)] object? obj)
    {
        if(obj == null) return false;
        if(obj.GetHashCode() != this.GetHashCode()) return false;
        if(obj is Position pos)
        {
            return this.X == pos.X && this.Y == pos.Y;
        }
        return false;
    }

    public Position(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }


    private string GetXString()
    {
        StringBuilder sb = new StringBuilder();
        int offset = (int)'A';
        var xC = X;
        do
        {
            sb.Append((char)(xC % 26 + offset));
            xC /= 26;
        } while(xC > 0);

        return sb.ToString();
    }

    public override string ToString()
    {
            return $"{GetXString()}{Y+1}";
    }

    public static implicit operator Position((int x, int y) tuple) => new Position(tuple.x, tuple.y);
}