using TokBlastPrototype1.Views.Layers;
using TokBlastPrototype1.Views.Scenes;
using static TokBlastPrototype1.Managers.SceneManagers;
using CocosSharp;
using TokBlastPrototype1.Managers;

namespace TokBlastPrototype1.Views.Scenes
{
    public class LoadingScene : BaseScene
    {
        public LoadingScene(CCGameView gameView) : base(gameView)
        {
            var layer = new LoadingLayer();
            this.AddLayer(layer);

            //  SoundManager.Instance.StopBackgroundMusic();
        }

        public override SceneManagers.SceneType GetSceneType()
        {
            return SceneManagers.SceneType.Loading;
        }

        /*  public override void OnEnter()
          {
              base.OnEnter();

              ((MenuScene)SceneManager.Instance.MenuScene).HideAds();
          }   */
    }
}
