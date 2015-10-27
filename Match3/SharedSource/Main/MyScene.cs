#region Using Statements
using System;
using System.Collections.Generic;
using WaveEngine.Common;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;
using WaveEngine.Components.Cameras;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Components.Graphics3D;
using WaveEngine.Components.Transitions;
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
		public enum States
		{

			GamePlay,
			TimeOut,
		}

		private States _currentState;
		private MessagePanel _messagePanel;
        //private int boardX = 400;
        //private int boardY = 100;

        //private int boardWidth = 500;
        //private int boardHeight = 500;

        private int tileSide = 100; // This should probably be taken from assets.
        private string[] tileSprites = {WaveContent.Tiles_spritesheet_TextureName.black,
                WaveContent.Tiles_spritesheet_TextureName.blue,
                WaveContent.Tiles_spritesheet_TextureName.green,
                WaveContent.Tiles_spritesheet_TextureName.red,
                WaveContent.Tiles_spritesheet_TextureName.yellow};
		

        public ScoreboardPanel scoreboardPanel;

		public States CurrentState
		{
			get { return _currentState; }
			set
			{
				_currentState = value;
				UpdateState(_currentState);
			}
		}

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

			_messagePanel = new MessagePanel(MessagePanel.MessageType.Hide)
			{
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};
			EntityManager.Add(_messagePanel);

            foreach (BoardConfiguration boardConfig in config.Levels[0].Boards)
            {
                Board board = new Board(boardConfig.X, boardConfig.Y, boardConfig.Width, boardConfig.Height,
                    boardConfig.Columns, boardConfig.Rows, tileSide);
                board.Entity.Name = boardConfig.Name;
                string[] selectedSprites = new string[boardConfig.Tiles];
                for (int i = 0; i < boardConfig.Tiles; i++)
                {
                    selectedSprites[i] = tileSprites[i];
                }
                List<Entity> tiles = board.GenerateRandomBoard(WaveContent.Tiles_spritesheet, selectedSprites);

                EntityManager.Add(board);

                EntityManager.Add(tiles);
            }

            this.AddSceneBehavior(new MySceneBehavior(), SceneBehavior.Order.PostUpdate);
			CurrentState = States.GamePlay;
		}

		protected override void Start()
        {
            EntityManager.Remove("particle2D");
            base.Start();
        }

		private void UpdateState(States state)
		{
			_messagePanel.IsVisible = false;
			switch (state)
			{
				case States.GamePlay:

					break;
				case States.TimeOut:
					_messagePanel.Type = MessagePanel.MessageType.Timeout;
                    foreach(Board board in EntityManager.FindAllByTag("board"))
                    {
                        board.Entity.IsActive = false;
                    }
					WaveServices.TimerFactory.CreateTimer("timer", TimeSpan.FromSeconds(2.5f), () =>
					{
						WaveServices.ScreenContextManager.To(
					new ScreenContext(new MainMenuScene()), new SpinningSquaresTransition(TimeSpan.FromSeconds(1.5f)));
					}, false);
					break;
			}
		}
    }
}
