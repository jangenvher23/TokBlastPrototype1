using System;
using CocosSharp;
using Xamarin.Forms;
using TokBlastPrototype1.Managers;
using TokBlastPrototype1.Utilities;
using Xamarin.Forms.Xaml;

namespace TokBlastPrototype1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class GamePage : ContentPage
    {
        public static CocosSharpView _cocosSharpView;
        public static CocosSharpView CocosSharpView
        {
            get { return _cocosSharpView; }
        }

        public GamePage()
        {
            _cocosSharpView = new CocosSharpView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            // Set the game world dimensions
            _cocosSharpView.DesignResolution = new Size(App.ScreenWidth, App.ScreenHeight);
            _cocosSharpView.ResolutionPolicy = CocosSharpView.ViewResolutionPolicy.NoBorder;

            // Set the method to call once the view has been initialised
            _cocosSharpView.ViewCreated = LoadGame;

            var grid = new Grid();
            grid.Children.Add(_cocosSharpView);

            Content = grid;
        }

        protected override void OnDisappearing()
        {
            if (_cocosSharpView != null)
                _cocosSharpView.Paused = true;

            base.OnDisappearing();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_cocosSharpView != null)
                _cocosSharpView.Paused = false;
        }

        private void LoadGame(object sender, EventArgs e)
        {
            CCGameView gameView = sender as CCGameView;
            if (gameView != null)
            {
                Screen.GameView = gameView;
                Screen.GameWidth = gameView.DesignResolution.Width;
                Screen.GameHeight = gameView.DesignResolution.Height;

                GameManager.Instance.Ready(this);

                ResourceManager.Instance.Ready(gameView);
                ResourceManager.Instance.SetupContentPaths();

                //      SoundManager.Instance.Ready(gameView);
                //       SoundManager.Instance.PreloadSoundContents();

                SceneManagers.Instance.Ready(gameView);
                SceneManagers.Instance.LaunchGame();
            }
        }
    }
}
