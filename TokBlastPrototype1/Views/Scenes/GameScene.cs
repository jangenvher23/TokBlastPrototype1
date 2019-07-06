using CocosSharp;
using static TokBlastPrototype1.Managers.SceneManagers;
using TokBlastPrototype1.Views.Layers;
using TokBlastPrototype1.Utilities;
namespace TokBlastPrototype1.Views.Scenes
{
    public class GameScene : BaseScene
    {
        public GameScene(CCGameView gameView) : base(gameView)
        {
            var background = new GameBackgroundLayer();
            this.AddLayer(background, 0, 0);

            var gameLayer = new GameLayer(this);
            gameLayer.Opacity = 0;
            this.AddLayer(gameLayer, 1, 1);

            var gameHudLayer = new GameHudLayer(this);
            gameHudLayer.Opacity = 0;
            this.AddLayer(gameHudLayer, 2, 2);

        }
        public override SceneType GetSceneType()
        {
            return SceneType.Game;
        }




    }
}
