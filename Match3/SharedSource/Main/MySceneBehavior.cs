using System.Diagnostics;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;

namespace Match3
{
    public class MySceneBehavior : SceneBehavior
    {
        protected override void Update(System.TimeSpan gameTime)
        {
            Trace.Write("a");

        }

        protected override void ResolveDependencies()
        {
        }
    }
}