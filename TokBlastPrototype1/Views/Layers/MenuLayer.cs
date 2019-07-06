using CocosSharp;
using TokBlastPrototype1.Models.Enums;
using TokBlastPrototype1.Utilities;
using TokBlastPrototype1.Managers;
using TokBlastPrototype1.Views.Scenes;
using TokBlastPrototype1.Services;

namespace TokBlastPrototype1.Views.Layers
{
    public class MenuLayer : BaseLayer
    {
        private CCSprite _gameLogo;
        private CCSprite _btnPlayGame;
        private CCSprite _btnShop;
        private CCSprite _btnSettings;
        private CCSprite _btnProfile;
        private CCSprite _btnHowToPlay;
        private CCLabel _sizes;
        public MenuLayer() : base()
        {
            AppSettings.CategorySelected = string.Empty;
            AppSettings.GameDifficultySelected = Difficulties.Default;
            AppSettings.GameModeSelected = GameMode.Default;
            AppSettings.PlayerTypeSelected = PlayerType.Default;
            AppSettings.CurrentSceneType = SceneManagers.SceneType.Menu;
            _gameLogo = new CCSprite(ResourceManager.Instance.SplashLogo);
            _gameLogo.AnchorPoint = CCPoint.AnchorMiddle;
            _gameLogo.ContentSize = new CCSize(Screen.GameWidth - Screen.GameWidth / 2.5f, Screen.GameHeight / 3.5f);
            this.AddChild(_gameLogo);

            _btnPlayGame = new CCSprite(ResourceManager.Instance.BtnPlay);
            _btnPlayGame.ContentSize = CustomSize.resizeSpriteByDeviceResolutionBox(3.6f, 6.3f);
            //_btnPlayGame.ContentSize = new CCSize(Screen.GameWidth - Screen.GameWidth * .73f, Screen.GameHeight - Screen.DeviceHeight * .83f);

            this.AddChild(_btnPlayGame);

            _btnSettings = new CCSprite(ResourceManager.Instance.BtnSettings);
            _btnSettings.ContentSize = CustomSize.resizeSpriteByDeviceResolutionBox(5.3f, 9f);
            this.AddChild(_btnSettings);

            _btnShop = new CCSprite(ResourceManager.Instance.BtnShop);
            _btnShop.ContentSize = CustomSize.resizeSpriteByDeviceResolutionBox(5.3f, 9f);
            this.AddChild(_btnShop);

            _btnProfile = new CCSprite(ResourceManager.Instance.BtnProfile);
            _btnProfile.ContentSize = CustomSize.resizeSpriteByDeviceResolutionBox(5.3f, 9f);
            this.AddChild(_btnProfile);

            _btnHowToPlay = new CCSprite(ResourceManager.Instance.BtnHowToPlay);
            _btnHowToPlay.ContentSize = CustomSize.resizeSpriteByDeviceResolutionBox(5.3f, 9f);
            this.AddChild(_btnHowToPlay);

            _sizes = new CCLabel($"Hiegth: {Screen.GameHeight} Width: {Screen.GameWidth}", "arial", 22f);
            _sizes.Position = new CCPoint(Screen.DeviceWidth / 2, Screen.DeviceHeight / 2);
            //   this.AddChild(_sizes);

            #region ommited
            /*
             *  var texturePlay = new CCTexture2D(ResourceManager.Instance.BtnPlay);
            var textureShop = new CCTexture2D(ResourceManager.Instance.BtnShop);
            var texttureSettings = new CCTexture2D(ResourceManager.Instance.BtnSettings);
            _btnPlayGame = new CCSprite(texturePlay);
            _btnPlayGame.SpriteFrame = new CCSpriteFrame(_btnPlayGame.resizeSprite(150, -150, 1.75f),
               texturePlay,
               new CCRect(0, 0, _btnPlayGame.ContentSize.Width * 2f, _btnPlayGame.ContentSize.Height * 1.015f)
                );
            _btnPlayGame.ContentSize = _btnPlayGame.resizeSprite(150, -150, 1.75f);

            this.AddChild(_btnPlayGame);

            _btnShop = new CCSprite(textureShop);
            _btnShop.SpriteFrame = new CCSpriteFrame(_btnShop.resizeSprite(0, 0, 1.2f),
               textureShop,
               new CCRect(0, 0, _btnShop.ContentSize.Width * 2f, _btnShop.ContentSize.Height * 1.015f)
                );
            _btnShop.ContentSize = _btnShop.resizeSprite(0, 0, 1.2f);
            this.AddChild(_btnShop);

            _btnSettings = new CCSprite(texttureSettings);
           _btnSettings.SpriteFrame =new CCSpriteFrame(_btnSettings.resizeSprite(0, 0, 1.2f),
             texttureSettings,
               new CCRect(0, 0, _btnSettings.ContentSize.Width * 2f, _btnSettings.ContentSize.Height * 1.015f)
                );
            _btnSettings.ContentSize = _btnSettings.resizeSprite(0, 0, 1.2f);
            this.AddChild(_btnSettings);
            */
            #endregion
            SetListeners();
        }



        void SetListeners()
        {

            var listener = new CCEventListenerTouchOneByOne();
            listener.OnTouchBegan = (touch, _event) => {
                var scale = new CCEaseElastic(new CCScaleTo(BUTTON_MOVE_TIME, .90f), BUTTON_ELASTIC_TIME);
                if (_btnPlayGame.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {

                    _btnPlayGame.RunAction(scale);

                }
                else if (_btnSettings.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {
                    _btnSettings.RunAction(scale);
                }
                else if (_btnShop.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {
                    _btnShop.RunAction(scale);
                }

                return true;
            };
            listener.OnTouchEnded = (touch, _event) => {
                var scale = new CCEaseElastic(new CCScaleTo(BUTTON_MOVE_TIME, 1), BUTTON_ELASTIC_TIME);
                if (_btnPlayGame.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {

                    _btnPlayGame.RunAction(scale);
                    AppSettings.CurrentSceneType = SceneManagers.SceneType.GameMode;
                    SceneManagers.Instance.NavigateToScene(SceneManagers.SceneType.GameMode);

                }
                else if (_btnSettings.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {
                    _btnSettings.RunAction(scale);
                }
                else if (_btnShop.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {
                    _btnShop.RunAction(scale);
                }
            };

            this.AddEventListener(listener);
        }
        protected override void AddedToScene()
        {
            base.AddedToScene();

            _gameLogo.Position = new CCPoint(Screen.DeviceWidth / 2 + (Screen.DeviceHeight * .035f), Screen.DeviceHeight * .70f);
            _btnPlayGame.Position = new CCPoint(Screen.DeviceWidth / 2, _gameLogo.PositionY - _gameLogo.ContentSize.Height / 1.15f);
            //
            
            _btnSettings.Position = new CCPoint(_btnPlayGame.PositionX - _btnPlayGame.PositionX /4.75f, _btnPlayGame.PositionY - _btnPlayGame.PositionY / 3.2f);
            _btnShop.Position = new CCPoint(_btnSettings.PositionX - _btnSettings.PositionX * .50f, _btnPlayGame.PositionY - _btnPlayGame.PositionY / 3.2f);
            _btnProfile.Position = new CCPoint(_btnSettings.PositionX + _btnSettings.PositionX * .50f, _btnPlayGame.PositionY - _btnPlayGame.PositionY / 3.2f);
            _btnHowToPlay.Position = new CCPoint(_btnSettings.PositionX + _btnSettings.PositionX * 1.0f, _btnPlayGame.PositionY - _btnPlayGame.PositionY / 3.2f);
            //

        }
    }
}
