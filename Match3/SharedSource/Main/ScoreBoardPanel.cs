using System;
using System.Collections.Generic;
using System.Text;
using WaveEngine.Common.Graphics;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.UI;

namespace Match3
{
    public class ScoreboardPanel : BaseDecorator
    {
        private TextBlock _scoreText, timeText;
        private int _scores;
		private TimeSpan time;

        public HorizontalAlignment HorizontalAlignment
        {
            get { return this.entity.FindComponent<PanelControl>().HorizontalAlignment; }
            set { this.entity.FindComponent<PanelControl>().HorizontalAlignment = value; }
        }

        public VerticalAlignment VerticalAlignment
        {
            get { return this.entity.FindComponent<PanelControl>().VerticalAlignment; }
            set { this.entity.FindComponent<PanelControl>().VerticalAlignment = value; }
        }

        public Thickness Margin
        {
            get { return this.entity.FindComponent<PanelControl>().Margin; }
            set { this.entity.FindComponent<PanelControl>().Margin = value; }
        }

		public TimeSpan Time
		{
			get { return time; }
			set
			{
				time = value;
				this.timeText.Text = this.time.ToString(@"mm\:ss\:ff");

				//this.timeIn.IsVisible = (time > TimeSpan.Zero) ? true : false;
				//this.timeOut.IsVisible = (time > TimeSpan.Zero) ? false : true;
			}
		}

        public int Scores
        {
            get { return _scores; }
            set { _scores = value;
                _scoreText.Text = string.Format("{0:0000}", _scores);
            }
        }

        public ScoreboardPanel()
        {
            this.entity = new Entity("scoreboardPanel").AddComponent(new Transform2D())
                .AddComponent(new PanelControl(300, 100))
                .AddComponent(new PanelControlRenderer());
			Configuration config = new Configuration();
			config.ReadConfiguration();
			this.time = TimeSpan.FromSeconds(config.Levels[0].TimeSec);

			this.timeText = new TextBlock("timeText")
			{
				Width = 132,
				Height = 42,
				Text = this.time.ToString(@"mm\:ss\:ff"),
				Margin = new Thickness(34, 19, 0, 0),
				Foreground = Color.LightGreen,
			};
			this.entity.AddChild(this.timeText.Entity);

            _scoreText = new TextBlock("scoreText")
            {
                Width = 132,
                Height = 42,
                Text = string.Format("{0:0000}", _scores),
                Margin = new WaveEngine.Framework.UI.Thickness(207, 19, 0,0)
            };
            this.entity.AddChild(_scoreText.Entity);

        }
    }
}
