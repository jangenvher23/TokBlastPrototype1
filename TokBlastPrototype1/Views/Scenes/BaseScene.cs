using CocosSharp;
using static TokBlastPrototype1.Managers.SceneManagers;
namespace TokBlastPrototype1.Views.Scenes
{

    public class BaseScene : CCScene
    {
        public BaseScene(CCGameView gameView) : base(gameView)
        {


        }

        public virtual SceneType GetSceneType()
        {
            return SceneType.Default;
        }
    }

}
