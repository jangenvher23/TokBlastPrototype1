using CocosSharp;
using Xamarin.Forms;
using static TokBlastPrototype1.Managers.SceneManagers;
using TokBlastPrototype1.Views.Layers;
namespace TokBlastPrototype1.Views.Scenes
{
    public class MenuScene : BaseScene
    {
        public MenuScene(CCGameView gameView) : base(gameView)
        {
            var backgroundLayer = new MenuBackgroundLayer();
            this.AddLayer(backgroundLayer, 0);

            var layer = new MenuLayer();
            layer.Opacity = 0;
            this.AddLayer(layer, 1);

        }
        public override SceneType GetSceneType()
        {
            return SceneType.Menu;
        }
    }
}
