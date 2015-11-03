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
            Win,
		}

		private States _currentState;
		private MessagePanel _messagePanel;
        private int _currentLevel = 0;

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

            CreateBoard();

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
                case States.Win:
                    //_messagePanel.Type = MessagePanel.MessageType.Win;
                    foreach (Board board in EntityManager.FindAllByTag("board"))
                    {
                        board.Entity.IsActive = false;
                    }
                    NextLevel();
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

        private void CreateBoard()
        {
            Configuration config = new Configuration();
            config.ReadConfiguration();
            foreach (BoardConfiguration boardConfig in config.Levels[_currentLevel].Boards)
            {
                Board board = new Board(boardConfig, tileSide);
                board.Entity.Name = boardConfig.Name;
                board.ChanceSpecial1 = boardConfig.Special1Chance;

                string[] selectedSprites = new string[boardConfig.Tiles];
                for (int i = 0; i < boardConfig.Tiles; i++)
                {
                    selectedSprites[i] = tileSprites[i];
                }
                List<Entity> tiles = board.GenerateRandomBoard(WaveContent.Tiles_spritesheet, selectedSprites);

                EntityManager.Add(board);

                EntityManager.Add(tiles);
            }
			scoreboardPanel.Time = TimeSpan.Zero;
			
    }

        private void NextLevel()
        {
            Configuration config = new Configuration();
            config.ReadConfiguration();
            _currentLevel++;
            if (_currentLevel >= config.Levels.Count)
            {
                _messagePanel.Type = MessagePanel.MessageType.Win;
                WaveServices.TimerFactory.CreateTimer("timer", TimeSpan.FromSeconds(2.5f), () =>
                {
                    WaveServices.ScreenContextManager.To(
                new ScreenContext(new MainMenuScene()), new SpinningSquaresTransition(TimeSpan.FromSeconds(1.5f)));
                }, false);
                return;
            }
            List<string> names = new List<string>();
            List<Entity> tiles = new List<Entity>();
            foreach(Board board in EntityManager.FindAllByTag("board"))
            {
                names.Add(board.Name);
               
                foreach(var t in board.Tiles)
                {
                    tiles.Add(t.Entity);
                }
            }
            foreach(Entity tile in tiles)
            {
                EntityManager.Remove(tile);
            }
            foreach (string name in names)
            {
                EntityManager.Remove(name);
            }
            CreateBoard();
            _currentState = States.GamePlay;
        }
    }
}
