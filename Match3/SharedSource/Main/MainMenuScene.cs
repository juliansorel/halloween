using Match3.Behaviors;
using System;
using System.Collections.Generic;
using System.Text;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;
using WaveEngine.Components.Transitions;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Services;

namespace Match3
{
    class MainMenuScene : Scene
    {
        public Entity DefaultCamera2D
        {
            get { return this.EntityManager.Find("defaultCamera2D"); }
        }

        protected override void CreateScene()
        {
            this.Load(@"Content/Scenes/MainMenuScene.wscene");
            this.DefaultCamera2D.FindComponent<Camera2D>().CenterScreen();
            this.CreateUI();
        }

        private void CreateUI()
        {
            Button startButton = new Button()
            {
                Text = "Play",
                IsBorder = false,
                Width = 100,
                Height = 75,
                BackgroundImage = WaveContent.Assets.black_png,
                PressedBackgroundImage = WaveContent.Assets.blue_png,
                Margin = new WaveEngine.Framework.UI.Thickness(
                    WaveServices.ViewportManager.RightEdge/2 - 50, WaveServices.ViewportManager.BottomEdge/2 - 37, 0, 0),
            };
            startButton.Entity.FindChild("ImageEntity").FindComponent<Transform2D>().Origin = Vector2.Center;
            startButton.Click += (s, o) => {
                WaveServices.ScreenContextManager.To(
                    new ScreenContext(new MyScene()), new SpinningSquaresTransition(TimeSpan.FromSeconds(1f)));
            };

            EntityManager.Add(startButton);
        }
    }
}
