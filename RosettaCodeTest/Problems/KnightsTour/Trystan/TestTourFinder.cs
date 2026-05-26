using System;
using System.Collections.Generic;
using System.Text;
using RosettaCode.Problems.KnightsTour.Trystan.Game;
using RosettaCode.Problems.KnightsTour.Trystan.Game.Pieces;
using RosettaCode.Problems.KnightsTour.Trystan.Tour;

namespace RosettaCodeTest.Problems.KnightsTour.Trystan;

[TestClass]
public class TestTourFinder
{
    [TestMethod]
    public void TestGetTourAsString_Result_StringEquals()
    {
        // Arrange
        var map = new TourMap(new ChessBoard(8, 8), new Knight(new Position(0, 0), "Knight"));
        var tourFinder = new TourFinder(map);
        var tour = new Queue<Position>();
        tour.Enqueue(new Position(0, 0));
        tour.Enqueue(new Position(1, 2));
        tour.Enqueue(new Position(2, 4));

        // Act
        var result = tourFinder.GetTourAsString(tour);

        Console.WriteLine(result);

        // Assert
        Assert.AreEqual("START A1 -> B3 -> C5 -> END", result);
        ;
    }

    [TestMethod]
    public void TestFindTour_Result_IsNotNull()
    {
        // Arrange
        var map = new TourMap(new ChessBoard(8, 8), new Knight(new Position(0, 0), "Knight"));
        var tourFinder = new TourFinder(map);
        // Act
        var result = tourFinder.FindTour();
        if(result is not null) Console.WriteLine(tourFinder.GetTourAsString(result!));
        else Console.WriteLine("failed to find tour");

        // Assert
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public void TestFindTour_WithHolyChessBoard_Result_IsNotNull()
    {
        int[][] holyPositions =
        [
            [0, 0, 0, 0],
            [0, 0, 1, 0],
            [0, 0, 0, 0],
            [1, 0, 0, 1],
        ];

        var chessboard = new HolyChessBoard(holyPositions);

        var map = new TourMap(chessboard, new Knight(new Position(0,0), "Knight"));
        var tourFinder1 = new TourFinder(map);

        var result1 = tourFinder1.FindTour();
        if (result1 is not null) Console.WriteLine(tourFinder1.GetTourAsString(result1!));
        else Console.WriteLine("failed to find tour");

        // Assert
        Assert.IsNotNull(result1);
    }

    [TestMethod]
    public void TestFindTour_WithImpossibleBoard_Result_IsNull()
    {
        var chessboard = new ChessBoard(4, 4);
        var map = new TourMap(chessboard, new Knight(new Position(0, 0), "Knight"));
        var tourFinder1 = new TourFinder(map);
        var result1 = tourFinder1.FindTour();
        if (result1 is not null) Console.WriteLine(tourFinder1.GetTourAsString(result1!));
        else Console.WriteLine("failed to find tour");
        // Assert
        Assert.IsNull(result1);
    }
}