using System;
using System.Collections.Generic;
using System.Text;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.UI;

namespace Match3
{
    public class ObjectivesPanel:BaseDecorator
    {
        private TextBlock text;
		private TextBlock _timeText;
		private TimeSpan _time;
        private List<Objective> _objectives;

		public TimeSpan Time
		{
			get { return _time; }
			set
			{
				_time = value;
				_timeText.Text = string.Format("Time Left: {0:0}", _time.TotalSeconds);
			}
		}

		public List<int> Thresholds { get; set; }
		public List<double> TimeFactors { get; set; }

		public ObjectivesPanel(List<Objective> objectives, int width, int x, int y)
        {
			_time = TimeSpan.FromSeconds(60);
            _objectives = objectives;
            this.entity = new Entity()
                               .AddComponent(new Transform2D() { X = x, Y = y })
                               ;

            this.text = new TextBlock("text")
            {
                Width = width,
                Height = 42,
                Text = string.Empty,
            };
            CreateText();

            this.entity.AddChild(this.text.Entity);

			_timeText = new TextBlock()
			{
				Width = width,
				Height = 100,
				Text = string.Format("Time Left: {0:0}", _time.TotalSeconds),
				Margin = new Thickness(0, 100, 0, 0),
				
			};
			this.entity.AddChild(_timeText.Entity);

		}

        public void UpdateObjectives(Match match)
        {
            StringBuilder sb = new StringBuilder("\n");
            foreach (Objective objective in _objectives)
            {
                objective.Update(match);
                sb.AppendFormat("{0} - {1}\n", objective.Name, objective.AmountLeft);
            }
            text.Text = sb.ToString();
        }

        public bool AreObjectivesMet()
        {
            foreach (Objective o in _objectives)
            {
                if (!o.IsMet())
                {
                    return false;
                }
            }
            return true;
        }

		public void UpdateTime(TimeSpan gameTime, TimeSpan totalTime)
		{
			double factor = 1.0;
			foreach(int threshold in Thresholds)
			{
				if (totalTime.TotalSeconds >= threshold)
				{
					factor = TimeFactors[Thresholds.IndexOf(threshold)];
				}
			}
			Time -= TimeSpan.FromTicks(gameTime.Ticks * (long)factor);
		}

        private void CreateText()
        {
            StringBuilder sb = new StringBuilder("\n");
            foreach (Objective objective in _objectives)
            {
                sb.AppendFormat("{0} - {1}\n", objective.Name, objective.AmountLeft);
            }
            text.Text = sb.ToString();
        }
    }
}
