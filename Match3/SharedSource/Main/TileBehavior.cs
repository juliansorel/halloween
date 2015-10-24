using System.Diagnostics;
using WaveEngine.Common.Math;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Services;

namespace Match3
{
    public class TileBehavior : Behavior
    {
        [RequiredComponent]
        public Transform2D Transform;

        private bool _growing = true;
        private const int GrowingFrames = 100;
        private const float GrowthFactor = 1.2f;
        private int _growingIndex = 0;
        private Vector2 _originalScale;
        private Vector2 _maxScale;
        private float velocity = 9f;
        private System.TimeSpan _totalTime = new System.TimeSpan();
        private Tile _parent;

        public TileBehavior(Tile parent)
        {
            _parent = parent;
        }

        protected override void Update(System.TimeSpan gameTime)
        {
            _totalTime += gameTime;
            //float fps = 60 * (float)gameTime.TotalSeconds;
            //Transform.X += 1 * velocity * fps;
            //Transform.Y += 1 * velocity * fps;
            //Trace.WriteLine(_totalTime);
            if (_parent.Selected) {
                Transform.DrawOrder = 0;
                if (_growing)
                {
                    float increase = (_maxScale.X - _originalScale.X) * (float)_totalTime.TotalSeconds;

                    Transform.XScale = _originalScale.X + increase;
                    Transform.YScale = _originalScale.Y + increase;
                }
                else
                {
                    float decrease = (_maxScale.X - _originalScale.X) * (1 - (float)_totalTime.TotalSeconds);

                    Transform.XScale = _originalScale.X + decrease;
                    Transform.YScale = _originalScale.Y + decrease;
                }
                if (_totalTime > System.TimeSpan.FromSeconds(0.4))
                {

                    _growing = !_growing;
                    _totalTime = System.TimeSpan.FromSeconds(0);
                };
            }
            else
            {
                Transform.DrawOrder = 1;
                Transform.Scale = _originalScale;
            }

        }

        private bool IsMouseOver()
        {

            var mouseState = WaveServices.Input.MouseState;
            if (mouseState.X > Transform.X - (Transform.XScale * 100)/2 && mouseState.X < Transform.X + (Transform.XScale * 100)/2 
                && mouseState.Y > Transform.Y - (Transform.YScale * 100)/2 && mouseState.Y < Transform.Y + (Transform.YScale * 100)/2)
            {
                return true;
            }
            return false;
        }

        protected override void ResolveDependencies()
        {
            base.ResolveDependencies();
            _originalScale = Transform.Scale;
            _maxScale = new Vector2(_originalScale.X * GrowthFactor, _originalScale.Y * GrowthFactor);
        }
    }
}
