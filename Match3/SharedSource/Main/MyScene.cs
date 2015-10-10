#region Using Statements
using System;
using System.Collections.Generic;
using WaveEngine.Common;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;
using WaveEngine.Components.Cameras;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Components.Graphics3D;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Resources;
using WaveEngine.Framework.Services;
#endregion

namespace Match3
{
    public class MyScene : Scene
    {
        private int boardX = 400;
        private int boardY = 100;

        private int boardWidth = 500;
        private int boardHeight = 500;

        private int boardColumns = 10;
        private int boardRows = 10;


        protected override void CreateScene()
        {
            this.Load(WaveContent.Scenes.MyScene);
            Board board = new Board(boardX, boardY, boardWidth, boardHeight, boardColumns, boardRows);
            string[] tileSprites = {WaveContent.Tiles_spritesheet_TextureName.black,
                WaveContent.Tiles_spritesheet_TextureName.blue,
                WaveContent.Tiles_spritesheet_TextureName.green,
                WaveContent.Tiles_spritesheet_TextureName.red,
                WaveContent.Tiles_spritesheet_TextureName.yellow};
            List<Entity> tiles = board.GenerateRandomBoard(WaveContent.Tiles_spritesheet, tileSprites);
            
            EntityManager.Add(tiles);
        }
    }
}
