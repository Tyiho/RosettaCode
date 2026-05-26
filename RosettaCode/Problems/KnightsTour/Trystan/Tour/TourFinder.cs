using System;
using System.Collections.Generic;
using System.Text;
using RosettaCode.Problems.KnightsTour.Trystan.Game;

namespace RosettaCode.Problems.KnightsTour.Trystan.Tour;
public class TourFinder
{
    public TourMap Map { get; }

    public TourFinder(TourMap map)
    {
        this.Map = map;
    }

    public string GetTourAsString(Queue<Position> tour)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("START ");

        foreach (var pos in tour)
        {
            sb.Append(pos.ToString() + " -> ");
        }
        sb.Append("END");
        return sb.ToString();
    }

    public Queue<Position>? FindTour()
    {
        var result = Map.SearchForTour();
        if (result.Item1) return result.Item2;
        return null;
    }
}
