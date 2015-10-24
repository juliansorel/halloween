using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;

namespace Match3
{
    class BoardBehavior : Behavior
    {
        private Board _parent;
        private bool _pressed;

        public BoardBehavior(Board parent)
        {
            _parent = parent;
        }

        protected override void Update(System.TimeSpan gameTime)
        {
            var mouseState = WaveServices.Input.MouseState;


            if (mouseState.X > _parent.X && mouseState.X < _parent.X + _parent.Width &&
                mouseState.Y > _parent.Y && mouseState.Y < _parent.Y + _parent.Height)
            {
                if(_pressed && mouseState.LeftButton == WaveEngine.Common.Input.ButtonState.Release)
                {
                    _pressed = false;
                    int tileColumn = (int)Math.Floor((mouseState.X - _parent.X) / (100 * _parent.GetTileScale().X));
                    int tileRow = (int)Math.Floor((mouseState.Y - _parent.Y) / (100 * _parent.GetTileScale().Y));
                    _parent.SelectTile(tileColumn, tileRow);
                }
                else if (mouseState.LeftButton == WaveEngine.Common.Input.ButtonState.Pressed)
                {
                    _pressed = true;
                }

                
                //Trace.WriteLine(string.Format("{0}, {1}", tileColumn, tileRow));
                
                
            }

        }

        protected override void ResolveDependencies()
        {
            
        }

        internal void RemoveEntity(Entity entity)
        {
            if (EntityManager != null)
            {
                EntityManager.Remove(entity);
            }
        }

        internal void AddEntity(Entity entity)
        {
            if (EntityManager != null)
            {
                EntityManager.Add(entity);
            }
        }
    }
}
