using System;
using System.Diagnostics;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;

namespace Match3
{
    public class MySceneBehavior : SceneBehavior
    {
		private ScoreboardPanel _scoreboardPanel;
		private MyScene _gamePlayScene;

        protected override void Update(System.TimeSpan gameTime)
        {
            Trace.Write("a");
			if (_gamePlayScene.CurrentState == MyScene.States.GamePlay)
			{

				_scoreboardPanel.Time -= gameTime;
				if (_scoreboardPanel.Time < TimeSpan.Zero)
				{
					_scoreboardPanel.Time = TimeSpan.Zero;
					_gamePlayScene.CurrentState = MyScene.States.TimeOut;
				}
                bool win = true;
                foreach(Board board in _gamePlayScene.EntityManager.FindAllByTag("board"))
                {
                    if (!board.IsComplete())
                    {
                        win = false;
                        break;
                    }
                }
                if (win)
                {
                    _gamePlayScene.CurrentState = MyScene.States.Win;
                }
			}
        }

        protected override void ResolveDependencies()
        {
			_gamePlayScene = this.Scene as MyScene;
			_scoreboardPanel = _gamePlayScene.EntityManager.Find<ScoreboardPanel>("scoreboardPanel");
        }
    }
}