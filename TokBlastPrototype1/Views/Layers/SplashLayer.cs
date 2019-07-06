using CocosSharp;
using TokBlastPrototype1.Managers;
using TokBlastPrototype1.Utilities;
using TokBlastPrototype1.Services;
namespace TokBlastPrototype1.Views.Layers
{
    public class SplashLayer : BaseLayer
    {
       
        private CCSprite _gameLogo;
        private CCSprite _loadingBarColor;
        private CCSprite _loadingBarBase;
        private CCSprite _background;
        private CCSprite _placeholder;
        public SplashLayer() : base()
        {
            _background = new CCSprite(ResourceManager.Instance.SplashBG);
            _background.ContentSize = new CCSize(Screen.GameWidth, Screen.GameHeight);
            _background.AnchorPoint = CCPoint.AnchorMiddle;
            this.AddChild(_background);

            _placeholder = new CCSprite();
            _placeholder.ContentSize = new CCSize(Screen.GameWidth, Screen.GameHeight / 2.5f);
            this.AddChild(_placeholder);
            _gameLogo = new CCSprite(ResourceManager.Instance.SplashLogo);
            _gameLogo.AnchorPoint = CCPoint.AnchorMiddle;
            _gameLogo.ContentSize = new CCSize(Screen.GameWidth - Screen.GameWidth / 3f, Screen.GameHeight / 3.2f);
            _placeholder.AddChild(_gameLogo);

            _loadingBarBase = new CCSprite(ResourceManager.Instance.SplashLoadPanel);
            _loadingBarBase.AnchorPoint = CCPoint.AnchorMiddle;
            _loadingBarBase.ContentSize = new CCSize(Screen.GameWidth - Screen.GameWidth / 3f - (Screen.GameWidth * .126f), Screen.GameHeight / 40);
            _placeholder.AddChild(_loadingBarBase);

            _loadingBarColor = new CCSprite(ResourceManager.Instance.SplashLoadBar);
            _loadingBarColor.AnchorPoint = CCPoint.AnchorMiddleLeft;
            _loadingBarColor.ContentSize = new CCSize(Screen.GameWidth - Screen.GameWidth / 3f - (Screen.GameWidth * .136f), Screen.GameHeight / 58f);
            _loadingBarColor.ScaleX = 0.0f;
            _placeholder.AddChild(_loadingBarColor);
        }
        protected override void AddedToScene()
        {
            base.AddedToScene();

            var bounds = VisibleBoundsWorldspace;

            _background.Position = bounds.Center;
            _gameLogo.Position = new CCPoint(bounds.Center.X + (Screen.GameWidth * .05f), bounds.Center.Y + (_gameLogo.ContentSize.Height / 4));
            _loadingBarBase.Position = new CCPoint(_gameLogo.Position.X - (Screen.GameWidth * .06f), (_gameLogo.Position.Y - (_gameLogo.ContentSize.Height / 2)) - 45);
            _loadingBarColor.Position = new CCPoint(
                _loadingBarBase.Position.X - (_loadingBarColor.ContentSize.Width / 2),
                (_gameLogo.Position.Y - (_gameLogo.ContentSize.Height / 2)) - 45);

            CCScaleTo scaleTo = new CCScaleTo(2, 1.0f, 1.0f);
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

                    ScheduleOnce(j => { SceneManagers.Instance.NavigateToScene(SceneManagers.SceneType.Menu); }, 0.5f);
                }
            });
        }

    }
}
