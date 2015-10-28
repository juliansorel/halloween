using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Match3;
using WaveEngine.Common.Math;
using System.Collections.Generic;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;

namespace UnitTests
{
    //[TestClass]
    //public class BoardTests
    //{
    //    [TestMethod]
    //    public void GetScale_Returns1_On10x10Board()
    //    {
    //        // setup
    //        int width = 1000;
    //        int height = 1000;
    //        int columns = 10;
    //        int rows = 10;
    //        int tileSide = 100;

    //        Vector2 expectedVecorScale = new Vector2(1.0f, 1.0f);
    //        Board sut = new Board(0, 0, width, height, columns, rows, tileSide);
    //        // act
    //        Vector2 actualScale = sut.GetTileScale(100, 100);
    //        // verify
    //        Assert.AreEqual(expectedVecorScale, actualScale, "On 1000 x 1000 10 x 10 board scale should be 1.0x1.0");
    //    }

    //    [TestMethod]
    //    public void GetScale_Returns05_On500WidthBoard()
    //    {
    //        // setup
    //        int width = 500;
    //        int height = 500;
    //        int columns = 10;
    //        int rows = 10;
    //        int tileSide = 100;

    //        Vector2 expectedVecorScale = new Vector2(0.5f, 0.5f);
    //        Board sut = new Board(0, 0, width, height, columns, rows, tileSide);
    //        // act
    //        Vector2 actualScale = sut.GetTileScale(100, 100);
    //        // verify
    //        Assert.AreEqual(expectedVecorScale, actualScale, "On 500 x 500 10 x 10 board scale should be 0.5x0.5");
    //    }

    //    [TestMethod]
    //    public void GetTilePosition_Returns00_On00Tile()
    //    {
    //        // setup
    //        int boardX = 0;
    //        int boardY = 0;
    //        int width = 1000;
    //        int height = 1000;
    //        int columns = 10;
    //        int rows = 10;
    //        int tileSide = 100;
    //        Point expectedPosition = new Point(50, 50);
    //        Board sut = new Board(boardX, boardY, width, height, columns, rows, tileSide);
    //        // act
    //        Point actualPosition = sut.GetTilePosition(0, 0);
    //        // verify
    //        Assert.AreEqual(expectedPosition, actualPosition);
    //    }

    //    [TestMethod]
    //    public void GetTilePosition_ReturnsBoardOffset_On00TileWithBoardOffset()
    //    {
    //        // setup
    //        int boardX = 1;
    //        int boardY = 2;
    //        int width = 1000;
    //        int height = 1000;
    //        int columns = 10;
    //        int rows = 10;
    //        int tileSide = 100;

    //        Point expectedPosition = new Point(51, 52);
    //        Board sut = new Board(boardX, boardY, width, height, columns, rows, tileSide);
    //        // act
    //        Point actualPosition = sut.GetTilePosition(0, 0);
    //        // verify
    //        Assert.AreEqual(expectedPosition, actualPosition);
    //    }

    //    [TestMethod]
    //    public void GetTilePosition_Returnsx100y200_On12Tile()
    //    {
    //        // setup
    //        int boardX = 0;
    //        int boardY = 0;
    //        int width = 1000;
    //        int height = 1000;
    //        int columns = 10;
    //        int rows = 10;
    //        int tileSide = 100;

    //        Point expectedPosition = new Point(150, 250);
    //        Board sut = new Board(boardX, boardY, width, height, columns, rows, tileSide);
    //        // act
    //        Point actualPosition = sut.GetTilePosition(1, 2);
    //        // verify
    //        Assert.AreEqual(expectedPosition, actualPosition);
    //    }

    //    [TestMethod]
    //    public void GetTilePosition_Returnsx50y100_On12TileAndSmallBoard()
    //    {
    //        // setup
    //        int boardX = 0;
    //        int boardY = 0;
    //        int width = 500;
    //        int height = 500;
    //        int columns = 10;
    //        int rows = 10;
    //        int tileSide = 100;

    //        Point expectedPosition = new Point(75, 125);
    //        Board sut = new Board(boardX, boardY, width, height, columns, rows, tileSide);
    //        // act
    //        Point actualPosition = sut.GetTilePosition(1, 2);
    //        // verify
    //        Assert.AreEqual(expectedPosition, actualPosition);
    //    }

    //    [TestMethod]
    //    public void GenerateBoard_ReturnsListOf100Entities_On10x10Board()
    //    {
    //        // setup
    //        int boardX = 0;
    //        int boardY = 0;
    //        int width = 500;
    //        int height = 500;
    //        int columns = 10;
    //        int rows = 10;
    //        int tileSide = 100;

    //        string[] tiles = { "red", "blue", "black", "yellow", "green" };

    //        int expectedNoEntities = 100;
    //        Board sut = new Board(boardX, boardY, width, height, columns, rows, tileSide);
    //        // act
    //        List<Entity> actualEntities = sut.GenerateRandomBoard("spritesheet", tiles);
    //        // verify
    //        Assert.AreEqual(expectedNoEntities, actualEntities.Count);
    //    }

    //    [TestMethod]
    //    public void SwapTiles_ChangesPosition_OnTwoTiles()
    //    {
    //        // setup
    //        Board sut = new Board(0, 0, 1000, 1000, 10, 10, 100);
    //        sut.GenerateRandomBoard("spritesheet", new string[]{ "black", "red", "blue"});
    //        Tile tile1 = sut.Tiles[1,2];
    //        Tile tile2 = sut.Tiles[5,3];
    //        Vector2 tile1OriginalPosition = tile1.Entity.FindComponent<Transform2D>().Position;
    //        Vector2 tile2OriginalPosition = tile2.Entity.FindComponent<Transform2D>().Position;

    //        //act
    //        sut.SwapTiles(1, 2, 5,3 );
    //        Vector2 tile1NewPosition = tile1.Entity.FindComponent<Transform2D>().Position;
    //        Vector2 tile2NewPosition = tile2.Entity.FindComponent<Transform2D>().Position;

    //        //verify
    //        Assert.AreEqual(tile2OriginalPosition, tile1NewPosition);
    //        Assert.AreEqual(tile1OriginalPosition, tile2NewPosition);
    //    }

    //    [TestMethod]
    //    public void SwapTiles_ChangesTiles_OnTwoTiles()
    //    {
    //        // setup
    //        Board sut = new Board(0, 0, 1000, 1000, 10, 10, 100);
    //        sut.GenerateRandomBoard("spritesheet", new string[] { "black", "red", "blue" });
    //        Tile tile1 = sut.Tiles[1, 2];
    //        Tile tile2 = sut.Tiles[5, 3];

    //        //act
    //        sut.SwapTiles(1, 2, 5, 3);
    //        Tile newTile1 = sut.Tiles[1, 2];
    //        Tile newTile2 = sut.Tiles[5, 3];

    //        //verify
    //        Assert.AreEqual(tile1, newTile2);
    //        Assert.AreEqual(tile2, newTile1);
    //    }

    //    [TestMethod]
    //    public void SelectTile_DeselectsOriginalTile_OnSecondTile()
    //    {
    //        // setup
    //        Board sut = new Board(0, 0, 1000, 1000, 10, 10, 100);
    //        sut.GenerateRandomBoard("spritesheet", new string[] { "black", "red", "blue" });
    //        Tile tile1 = sut.Tiles[1, 2];

    //        //act
    //        sut.SelectTile(1, 2);
    //        sut.SelectTile(5, 3);

    //        //verify
    //        Assert.IsFalse(tile1.Selected);
    //    }

    //    [TestMethod]
    //    public void SelectTile_SelectsSecondTile_OnNotAdjacentTiles()
    //    {
    //        // setup
    //        Board sut = new Board(0, 0, 1000, 1000, 10, 10, 100);
    //        sut.GenerateRandomBoard("spritesheet", new string[] { "black", "red", "blue" });
    //        Tile tile1 = sut.Tiles[1, 2];
    //        Tile tile2 = sut.Tiles[1, 4];

    //        //act
    //        sut.SelectTile(1, 2);
    //        sut.SelectTile(1, 4);

    //        //verify
    //        Assert.IsFalse(tile1.Selected, "First tile should not be selected.");
    //        Assert.IsTrue(tile2.Selected, "Second tile should be selected.");
    //    }

    //    [TestMethod]
    //    public void FindMatches_FindsReturnsAMatch_OnHorizontal3MatchPresent()
    //    {
    //        // setup
    //        Board sut = new Board(0, 0, 1000, 1000, 10, 10, 100);
    //        sut.GenerateRandomBoard("spritesheet", new string[] { "black", "red", "blue" });
    //        EnsureNoMatchesOnBoard(sut.Tiles);
    //        sut.Tiles[0, 0].Index = 0;
    //        sut.Tiles[1, 0].Index = 0;
    //        sut.Tiles[2, 0].Index = 0;
    //        // act
    //        List<Match> actualMatches = sut.FindMatches();
    //        // verify
    //        Assert.IsTrue(actualMatches.Count > 0, "Should find at least one match.");
            
    //    }

    //    [TestMethod]
    //    public void FindMatches_FindsReturnsAMatch_OnVertical3MatchPresent()
    //    {
    //        // setup
    //        Board sut = new Board(0, 0, 1000, 1000, 10, 10, 100);
    //        sut.GenerateRandomBoard("spritesheet", new string[] { "black", "red", "blue" });
    //        EnsureNoMatchesOnBoard(sut.Tiles);
    //        sut.Tiles[0, 0].Index = 0;
    //        sut.Tiles[0, 1].Index = 0;
    //        sut.Tiles[0, 2].Index = 0;
    //        // act
    //        List<Match> actualMatches = sut.FindMatches();
    //        // verify
    //        Assert.IsTrue(actualMatches.Count > 0, "Should find at least one match.");

    //    }

    //    [TestMethod]
    //    public void FindMatches_FindsReturnsOneMatch_OnHorizontal4MatchPresent()
    //    {
    //        // setup
    //        Board sut = new Board(0, 0, 1000, 1000, 10, 10, 100);
    //        sut.GenerateRandomBoard("spritesheet", new string[] { "black", "red", "blue" });
    //        EnsureNoMatchesOnBoard(sut.Tiles);
    //        sut.Tiles[0, 0].Index = 0;
    //        sut.Tiles[1, 0].Index = 0;
    //        sut.Tiles[2, 0].Index = 0;
    //        sut.Tiles[3, 0].Index = 0;
    //        // act
    //        List<Match> actualMatches = sut.FindMatches();
    //        // verify
    //        Assert.AreEqual(1, actualMatches.Count, "Should find one match.");

    //    }

    //    [TestMethod]
    //    public void FindMatches_FindsReturnsOneMatch_OnVertical4MatchPresent()
    //    {
    //        // setup
    //        Board sut = new Board(0, 0, 1000, 1000, 10, 10, 100);
    //        sut.GenerateRandomBoard("spritesheet", new string[] { "black", "red", "blue" });
    //        EnsureNoMatchesOnBoard(sut.Tiles);
    //        sut.Tiles[0, 0].Index = 0;
    //        sut.Tiles[0, 1].Index = 0;
    //        sut.Tiles[0, 2].Index = 0;
    //        sut.Tiles[0, 3].Index = 0;
    //        // act
    //        List<Match> actualMatches = sut.FindMatches();
    //        // verify
    //        Assert.AreEqual(1, actualMatches.Count, "Should find one match.");

    //    }

    //    [TestMethod]
    //    public void FindMatches_FindsReturnsOneMatch_OnHorizontal5MatchPresent()
    //    {
    //        // setup
    //        Board sut = new Board(0, 0, 1000, 1000, 10, 10, 100);
    //        sut.GenerateRandomBoard("spritesheet", new string[] { "black", "red", "blue" });
    //        EnsureNoMatchesOnBoard(sut.Tiles);
    //        sut.Tiles[0, 0].Index = 0;
    //        sut.Tiles[1, 0].Index = 0;
    //        sut.Tiles[2, 0].Index = 0;
    //        sut.Tiles[3, 0].Index = 0;
    //        sut.Tiles[4, 0].Index = 0;
    //        // act
    //        List<Match> actualMatches = sut.FindMatches();
    //        // verify
    //        Assert.AreEqual(1, actualMatches.Count, "Should find one match.");

    //    }

    //    [TestMethod]
    //    public void FindMatches_FindsReturnsOneMatch_OnVertical5MatchPresent()
    //    {
    //        // setup
    //        Board sut = new Board(0, 0, 1000, 1000, 10, 10, 100);
    //        sut.GenerateRandomBoard("spritesheet", new string[] { "black", "red", "blue" });
    //        EnsureNoMatchesOnBoard(sut.Tiles);
    //        sut.Tiles[0, 0].Index = 0;
    //        sut.Tiles[0, 1].Index = 0;
    //        sut.Tiles[0, 2].Index = 0;
    //        sut.Tiles[0, 3].Index = 0;
    //        sut.Tiles[0, 4].Index = 0;
    //        // act
    //        List<Match> actualMatches = sut.FindMatches();
    //        // verify
    //        Assert.AreEqual(1, actualMatches.Count, "Should find one match.");

    //    }

    //    [TestMethod]
    //    public void GenerateBoard_CreatesBoardWithNoMatches_On5Tiles()
    //    {
    //        // setup
    //        int boardX = 0;
    //        int boardY = 0;
    //        int width = 500;
    //        int height = 500;
    //        int columns = 10;
    //        int rows = 10;
    //        int tileSide = 100;

    //        string[] tiles = { "red", "blue", "black", "yellow", "green" };
    //        Board sut = new Board(boardX, boardY, width, height, columns, rows, tileSide);
    //        // act
    //        sut.GenerateRandomBoard("spritesheet", tiles);
    //        // verify
    //        List<Match> actualMatches = sut.FindMatches();
    //        Assert.AreEqual(0, actualMatches.Count);
    //    }

    //    [TestMethod]
    //    public void SelectTile_DoesNotMoveTiles_OnNoMatchMade()
    //    {
    //        // setup
    //        Board sut = new Board(0, 0, 1000, 1000, 10, 10, 100);
    //        sut.GenerateRandomBoard("spritesheet", new string[] { "black", "red", "blue" });
    //        EnsureNoMatchesOnBoard(sut.Tiles);
    //        sut.Tiles[0, 0].Change(0); sut.Tiles[1, 0].Change(1); sut.Tiles[2, 0].Change(2);
    //        sut.Tiles[0, 1].Change(0); sut.Tiles[1, 1].Change(2); 
    //        sut.Tiles[0, 2].Change(1); sut.Tiles[1, 2].Change(1);

    //        Tile tile1 = sut.Tiles[0, 0];
    //        Tile tile2 = sut.Tiles[1, 0];

    //        //act
    //        sut.SelectTile(0, 0);
    //        sut.SelectTile(1, 0);


    //        //verify
    //        Tile newTile1 = sut.Tiles[0, 0];
    //        Tile newTile2 = sut.Tiles[1, 0];

    //        Assert.AreEqual(tile1, newTile1, "Tiles should not be swapped.");
    //        Assert.AreEqual(tile2, newTile2, "Tiles should not be swapped.");
    //    }

    //    [TestMethod]
    //    public void SelectTile_RemovesTilesMatched_OnMatchMade()
    //    {
    //        // setup
    //        Board sut = new Board(0, 0, 1000, 1000, 10, 10, 100);
    //        sut.GenerateRandomBoard("spritesheet", new string[] { "black", "red", "blue" });
    //        sut.Tiles[0, 0].Change(0); sut.Tiles[1, 0].Change(1); sut.Tiles[2, 0].Change(2);
    //        sut.Tiles[0, 1].Change(1); sut.Tiles[1, 1].Change(2);
    //        sut.Tiles[0, 2].Change(1); sut.Tiles[1, 2].Change(1);

    //        Tile matchedTile = sut.Tiles[0, 0];

    //        //act
    //        sut.SelectTile(0, 0);
    //        sut.SelectTile(1, 0);


    //        //verify
    //        Tile newTile = sut.Tiles[0, 0];
    //        Assert.AreNotEqual(newTile, matchedTile, "After matching matched tiles should be removed from the board.");
    //    }

    //    [TestMethod]
    //    public void SelectTile_RemovesAllTilesMatched_OnMatchMade()
    //    {
    //        // setup
    //        Board sut = new Board(0, 0, 1000, 1000, 10, 10, 100);
    //        sut.GenerateRandomBoard("spritesheet", new string[] { "black", "red", "blue" });
    //        sut.Tiles[0, 0].Change(0); sut.Tiles[1, 0].Change(1); sut.Tiles[2, 0].Change(2);
    //        sut.Tiles[0, 1].Change(1); sut.Tiles[1, 1].Change(2);
    //        sut.Tiles[0, 2].Change(1); sut.Tiles[1, 2].Change(1);


    //        //act
    //        sut.SelectTile(0, 0);
    //        sut.SelectTile(1, 0);


    //        //verify
    //        IList<Match> matchesLeft = sut.FindMatches();
    //        Assert.AreEqual(0, matchesLeft.Count, "After matching tiles there should be no more matches left on the board.");
    //    }

    //    [TestMethod]
    //    public void DropTiles_MovesTileAboveDown_OnMissingTile()
    //    {
    //        // setup
    //        Board sut = new Board(0, 0, 1000, 1000, 10, 10, 100);
    //        sut.GenerateRandomBoard("spritesheet", new string[] { "black", "red", "blue" });
    //        Tile tileAbove = sut.Tiles[0, 0];
    //        sut.Tiles[0, 1] = null;
    //        // act
    //        sut.DropTiles();
    //        // verify
    //        Tile newTile = sut.Tiles[0, 1];
    //        Assert.AreEqual(newTile, tileAbove);
    //    }

    //    [TestMethod]
    //    public void DropTiles_FillsEmptyTilesWithNewOnes_OnMissingTile()
    //    {
    //        // setup
    //        Board sut = new Board(0, 0, 1000, 1000, 10, 10, 100);
    //        sut.GenerateRandomBoard("spritesheet", new string[] { "black", "red", "blue" });
    //        sut.Tiles[0, 1] = null;
    //        // act
    //        sut.DropTiles();
    //        // verify
    //        Assert.IsNotNull(sut.Tiles[0, 0]);
    //    }

    //    public void EnsureNoMatchesOnBoard(Tile[,] board)
    //    {
    //        int index = 1;
    //        foreach (var tile in board)
    //        {
    //            tile.Index = index;
    //            index++;
    //        }
    //    }
    }

