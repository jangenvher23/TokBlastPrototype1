using CocosSharp;
using TokBlastPrototype1.Views.Layers;
using static TokBlastPrototype1.Managers.SceneManagers;
namespace TokBlastPrototype1.Views.Scenes
{
    public class SplashScene : BaseScene
    {
        public SplashScene(CCGameView gameView) : base(gameView)
        {
            var layer = new SplashLayer();
            this.AddLayer(layer);
        }

        public override SceneType GetSceneType()
        {
            return SceneType.Splash;
        }
    }
}
