using CocosSharp;
using TokBlastPrototype1.Managers;
using TokBlastPrototype1.Utilities;
using TokBlastPrototype1.Models.Enums;
using TokBlastPrototype1.Extensions;
using TokBlastPrototype1.Services;
using Xamarin.Forms.Xaml;

namespace TokBlastPrototype1.Views.Layers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class LoadingLayer : BaseLayer
    {
        private CCLabel _loadingLabel;
        private CCSprite _loadingBarColor;
        private CCSprite _loadingBarBase;
        private CCSprite _background;

        public LoadingLayer() : base()
        {
            _background = new CCSprite(ResourceManager.Instance.SplashBG);
            _background.ContentSize = new CCSize(Screen.GameWidth, Screen.GameHeight);
            _background.AnchorPoint = CCPoint.AnchorMiddle;
            this.AddChild(_background);

            string loadingText = "LOADING...";
            int loadingTextFontSize = 60;
            if (AppSettings.PlayerTypeSelected == PlayerType.Multiplayer)
            {
                loadingText = "FINDING MATCH...";
                loadingTextFontSize = 50;
            }
            _loadingLabel = new CCLabel(loadingText, ResourceManager.Instance.FaceOffM54, loadingTextFontSize, CCLabelFormat.SystemFont);
            _loadingLabel.AnchorPoint = CCPoint.AnchorMiddleBottom;
            _loadingLabel.VerticalAlignment = CCVerticalTextAlignment.Center;
            _loadingLabel.HorizontalAlignment = CCTextAlignment.Center;
            _loadingLabel.SetDisplayedColorFromHex(App.PrimaryTextColor);
            this.AddChild(_loadingLabel);
            _loadingBarBase = new CCSprite(ResourceManager.Instance.SplashLoadPanel);
            _loadingBarBase.AnchorPoint = CCPoint.AnchorMiddle;
            _loadingBarBase.ContentSize = new CCSize(Screen.GameWidth - Screen.GameWidth / 8, Screen.GameHeight / 40);
            this.AddChild(_loadingBarBase);

            _loadingBarColor = new CCSprite(ResourceManager.Instance.SplashLoadBar);
            _loadingBarColor.AnchorPoint = CCPoint.AnchorMiddleLeft;
            _loadingBarColor.ContentSize = new CCSize(Screen.GameWidth - Screen.GameWidth / 7.25f, Screen.GameHeight / 55f);
            _loadingBarColor.ScaleX = 0.0f;
            this.AddChild(_loadingBarColor);
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            var bounds = VisibleBoundsWorldspace;

            _background.Position = bounds.Center;
            _loadingLabel.Position = new CCPoint(bounds.Center.X, bounds.Center.Y);
            _loadingBarBase.Position = new CCPoint(bounds.Center.X, (_loadingLabel.Position.Y - (_loadingLabel.ContentSize.Height / 2)) - 20);
            _loadingBarColor.Position = new CCPoint(
                _loadingBarBase.Position.X - (_loadingBarColor.ContentSize.Width / 2),
                (_loadingLabel.Position.Y - (_loadingLabel.ContentSize.Height / 2)) - 20);
            var scaleUpAction = new CCEaseElastic(new CCScaleTo(0.3f, 1f), 0.15f);
            var scaleDownAction = new CCEaseElastic(new CCScaleTo(0.3f, 0.85f), 0.15f);
            _loadingLabel.AddAction(new CCRepeatForever(scaleUpAction, scaleDownAction));

            var loadingTime = 0.5f;
            if (AppSettings.PlayerTypeSelected == PlayerType.Multiplayer)
            {
                loadingTime = 2;
            }
            CCScaleTo scaleTo = new CCScaleTo(loadingTime, 1.0f, 1.0f);
            _loadingBarColor.RunAction(scaleTo);

            // SoundManager.Instance.PlayLoadingScreenSound();

            OnUpdate();
        }

        private void OnUpdate()
        {

            Schedule(i =>
            {
                if (_loadingBarColor.NumberOfRunningActions <= 0)
                {
                    if (AppSettings.CurrentSceneType == SceneManagers.SceneType.Game)
                    {
                        WordService.GameStart();
                    }
                    ScheduleOnce(j =>
                    {
                        if (AppSettings.PlayerTypeSelected == PlayerType.Multiplayer)
                        {
                            // TODO: Call find match API here
                        }

                        ResourceManager.Instance.LoadGameResources();

                        if (AppSettings.CurrentSceneType == SceneManagers.SceneType.Game)
                        {
                            SceneManagers.Instance.NavigateToScene(SceneManagers.SceneType.Game);
                        }
                        else
                        {
                            SceneManagers.Instance.NavigateToScene(SceneManagers.SceneType.Menu);
                        }
                    }, 0.5f);
                }
            });
        }
    }
}
