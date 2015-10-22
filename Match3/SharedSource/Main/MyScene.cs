#region Using Statements
using System;
using System.Collections.Generic;
using WaveEngine.Common;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;
using WaveEngine.Components.Cameras;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Components.Graphics3D;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Resources;
using WaveEngine.Framework.Services;
using WaveEngine.Framework.UI;
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
        private int tileSide = 100; // This should probably be taken from assets.
        private string[] tileSprites = {WaveContent.Tiles_spritesheet_TextureName.black,
                WaveContent.Tiles_spritesheet_TextureName.blue,
                WaveContent.Tiles_spritesheet_TextureName.green,
                WaveContent.Tiles_spritesheet_TextureName.red,
                WaveContent.Tiles_spritesheet_TextureName.yellow};


        public ScoreboardPanel scoreboardPanel;

        protected override void CreateScene()
        {
            this.Load(WaveContent.Scenes.MyScene);

            Configuration config = new Configuration();
            config.ReadConfiguration();

            scoreboardPanel = new ScoreboardPanel()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, WaveServices.ViewportManager.TopEdge, 0,0),
            };
            EntityManager.Add(scoreboardPanel);

            Board board = new Board(boardX, boardY, boardWidth, boardHeight, config.Columns, config.Rows, tileSide);

            string[] selectedSprites = new string[config.Tiles];
            for (int i = 0; i< config.Tiles; i++)
            {
                selectedSprites[i] = tileSprites[i];
            }
            List<Entity> tiles = board.GenerateRandomBoard(WaveContent.Tiles_spritesheet, selectedSprites);

            EntityManager.Add(board);
            EntityManager.Add(tiles);

            this.AddSceneBehavior(new MySceneBehavior(), SceneBehavior.Order.PostUpdate);
        }

        protected override void Start()
        {
            EntityManager.Remove("particle2D");
            base.Start();
        }
    }
}
