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

				_scoreboardPanel.Time += gameTime;
                bool win = true;
                foreach(Board board in _gamePlayScene.EntityManager.FindAllByTag("board"))
                {
					board.ObjectivesPanel.UpdateTime(gameTime, _scoreboardPanel.Time);
					if (board.ObjectivesPanel.Time < TimeSpan.Zero)
					{
						board.ObjectivesPanel.Time = TimeSpan.Zero;
						_gamePlayScene.CurrentState = MyScene.States.TimeOut;
					}
                    if (!board.IsComplete())
                    {
                        win = false;
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