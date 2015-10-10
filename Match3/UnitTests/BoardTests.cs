using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Match3;
using WaveEngine.Common.Math;
using System.Collections.Generic;
using WaveEngine.Framework;

namespace UnitTests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void GetScale_Returns1_On10x10Board()
        {
            // setup
            int width = 1000;
            int height = 1000;
            int columns = 10;
            int rows = 10;
            Vector2 expectedVecorScale = new Vector2(1.0f, 1.0f);
            Board sut = new Board(0, 0, width, height, columns, rows);
            // act
            Vector2 actualScale = sut.GetTileScale(100, 100);
            // verify
            Assert.AreEqual(expectedVecorScale, actualScale, "On 1000 x 1000 10 x 10 board scale should be 1.0x1.0");
        }

        [TestMethod]
        public void GetScale_Returns05_On500WidthBoard()
        {
            // setup
            int width = 500;
            int height = 500;
            int columns = 10;
            int rows = 10;
            Vector2 expectedVecorScale = new Vector2(0.5f, 0.5f);
            Board sut = new Board(0, 0, width, height, columns, rows);
            // act
            Vector2 actualScale = sut.GetTileScale(100, 100);
            // verify
            Assert.AreEqual(expectedVecorScale, actualScale, "On 500 x 500 10 x 10 board scale should be 0.5x0.5");
        }

        [TestMethod]
        public void GetTilePosition_Returns00_On00Tile()
        {
            // setup
            int boardX = 0;
            int boardY = 0;
            int width = 1000;
            int height = 1000;
            int columns = 10;
            int rows = 10;
            Point expectedPosition = new Point(0, 0);
            Board sut = new Board(boardX, boardY, width, height, columns, rows);
            // act
            Point actualPosition = sut.GetTilePosition(0, 0);
            // verify
            Assert.AreEqual(expectedPosition, actualPosition);
        }

        [TestMethod]
        public void GetTilePosition_ReturnsBoardOffset_On00TileWithBoardOffset()
        {
            // setup
            int boardX = 1;
            int boardY = 2;
            int width = 1000;
            int height = 1000;
            int columns = 10;
            int rows = 10;
            Point expectedPosition = new Point(1, 2);
            Board sut = new Board(boardX, boardY, width, height, columns, rows);
            // act
            Point actualPosition = sut.GetTilePosition(0, 0);
            // verify
            Assert.AreEqual(expectedPosition, actualPosition);
        }

        [TestMethod]
        public void GetTilePosition_Returnsx100y200_On12Tile()
        {
            // setup
            int boardX = 0;
            int boardY = 0;
            int width = 1000;
            int height = 1000;
            int columns = 10;
            int rows = 10;
            Point expectedPosition = new Point(100, 200);
            Board sut = new Board(boardX, boardY, width, height, columns, rows);
            // act
            Point actualPosition = sut.GetTilePosition(1, 2);
            // verify
            Assert.AreEqual(expectedPosition, actualPosition);
        }

        [TestMethod]
        public void GetTilePosition_Returnsx50y100_On12TileAndSmallBoard()
        {
            // setup
            int boardX = 0;
            int boardY = 0;
            int width = 500;
            int height = 500;
            int columns = 10;
            int rows = 10;
            Point expectedPosition = new Point(50, 100);
            Board sut = new Board(boardX, boardY, width, height, columns, rows);
            // act
            Point actualPosition = sut.GetTilePosition(1, 2);
            // verify
            Assert.AreEqual(expectedPosition, actualPosition);
        }

        [TestMethod]
        public void GenerateBoard_ReturnsListOf100Entities_On10x10Board()
        {
            // setup
            int boardX = 0;
            int boardY = 0;
            int width = 500;
            int height = 500;
            int columns = 10;
            int rows = 10;
            string[] tiles = { "red", "blue", "black", "yellow", "green" };

            int expectedNoEntities = 100;
            Board sut = new Board(boardX, boardY, width, height, columns, rows);
            // act
            List<Entity> actualEntities = sut.GenerateRandomBoard("spritesheet", tiles);
            // verify
            Assert.AreEqual(expectedNoEntities, actualEntities.Count);
        }
    }
}
