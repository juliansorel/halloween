using System;
using System.Collections.Generic;
using System.Text;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.UI;

namespace Match3
{
    class ObjectivesPanel:BaseDecorator
    {
        private TextBlock text;
        private List<Objective> _objectives;

        public ObjectivesPanel(List<Objective> objectives, int width, int x, int y)
        {
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

        }

        public void UpdateObjectives(Match match)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Objective objective in _objectives)
            {
                objective.Update(match);
                sb.AppendFormat("{0} - {1}", objective.Name, objective.AmountLeft);
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

        private void CreateText()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Objective objective in _objectives)
            {
                sb.AppendFormat("{0} - {1}\n", objective.Name, objective.AmountLeft);
            }
            text.Text = sb.ToString();
        }
    }
}
