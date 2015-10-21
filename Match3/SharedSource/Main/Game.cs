#region Using Statements
using System;
using WaveEngine.Common;
using WaveEngine.Common.Graphics;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;
#endregion

namespace Match3
{
    public class Game : WaveEngine.Framework.Game
    {
        public override void Initialize(IApplication application)
        {
            base.Initialize(application);
            ViewportManager vm = WaveServices.ViewportManager;
            vm.Activate(1280, 720, ViewportManager.StretchMode.Uniform);
            ScreenContext screenContext = new ScreenContext(new MainMenuScene());	
			WaveServices.ScreenContextManager.To(screenContext);
        }

    }
}
