using CocosSharp;
using TokBlastPrototype1.Views.Scenes;
namespace TokBlastPrototype1.Managers
{
    public class SceneManagers
    {
        public static readonly SceneManagers instance = new SceneManagers();
        public static SceneManagers Instance
        {
            get { return instance; }
        }
        public CCGameView GameView { get; private set; }


        public enum SceneType
        {
            Splash,
            Menu,
            GameMode,
            Loading,
            Game,
            Default
        }
        // Scenes
        private BaseScene _menuScene;
        public BaseScene MenuScene
        {
            get { return _menuScene; }
            set { _menuScene = value; }
        }

        private BaseScene _splashScene;
        private BaseScene _selectionScene;
        private BaseScene _loadingScene;
        private BaseScene _gameScene;

        // Variables
        private BaseScene _currentScene;
        public BaseScene CurrentScene
        {
            get { return _currentScene; }
            set { _currentScene = value; }
        }


        // Launches the game and creates the splash scene
        public void LaunchGame()
        {

            if (_splashScene == null)
            {

                ResourceManager.Instance.LoadSplashResources();
                _splashScene = new SplashScene(GameView);
            }

            GameView.RunWithScene(_splashScene);
            CurrentScene = _splashScene;

        }

        // Navigate to a new scene from the current scene
        public void NavigateToScene(SceneType sceneType)
        {
            BaseScene scene = null;
            ResourceManager.Instance.LoadFontResources();
            switch (sceneType)
            {
                case SceneType.Splash:
                    _splashScene = new SplashScene(GameView);
                    scene = _splashScene;
                    break;
                case SceneType.Menu:
                    ResourceManager.Instance.LoadMenuResources();
                    _menuScene = new MenuScene(GameView);
                    scene = _menuScene;
                    break;
                case SceneType.GameMode:
                    ResourceManager.Instance.LoadGameModeResources();

                    _selectionScene = new SelectionScene(GameView);
                    scene = _selectionScene;
                    break;
                case SceneType.Loading:
                    _loadingScene = new LoadingScene(GameView);
                    scene = _loadingScene;
                    break;
                case SceneType.Game:
                    _gameScene = new GameScene(GameView);
                    scene = _gameScene;
                    break;
            }

            if (scene != null)
            {
                GameView.Director.ReplaceScene(scene);
                CurrentScene = scene;
            }
        }

        public void Ready(CCGameView gameView)
        {
            GameView = gameView;
        }
    }
}
