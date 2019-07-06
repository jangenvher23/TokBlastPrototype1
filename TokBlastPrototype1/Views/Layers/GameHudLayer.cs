using CocosSharp;
using TokBlastPrototype1.Managers;
using TokBlastPrototype1.Utilities;
using TokBlastPrototype1.Models.Enums;
using TokBlastPrototype1.Services;
using TokBlastPrototype1.Views.Scenes;

namespace TokBlastPrototype1.Views.Layers
{
	public class GameHudLayer : BaseLayer
	{
		private CCSprite _Footer;
		private CCSprite _btnHome;
		private CCSprite _btnSmash;
		private CCSprite _btnClock;
		private CCSprite _smashCounter;
		private CCSprite _placeHolder;

		private CCLabel _smashCount;

		private CCLabel _timer;

		float pos = 1;
		public GameHudLayer(CCScene scene) : base()
		{


			_placeHolder = new CCSprite();
			_placeHolder.ContentSize = new CCSize(App.ScreenWidth, App.ScreenHeight / 9.5f);
			this.AddChild(_placeHolder);

			_Footer = new CCSprite(ResourceManager.Instance.ScreenFooter);
			_Footer.ContentSize = CustomSize.resizeSpriteByDeviceResolutionBox(1, _placeHolder.ContentSize.Height / 3.5f);
			_placeHolder.AddChild(_Footer);

			_btnHome = new CCSprite(ResourceManager.Instance.BtnGameHome);
			_btnHome.ContentSize = CustomSize.resizeSpriteByDeviceResolutionBox(10, 16);

			_placeHolder.AddChild(_btnHome);

			_btnSmash = new CCSprite(ResourceManager.Instance.BtnSmash);
			_btnSmash.ContentSize = CustomSize.resizeSpriteByDeviceResolutionBox(5, 9.5f);
			_placeHolder.AddChild(_btnSmash);

			_smashCounter = new CCSprite(ResourceManager.Instance.SmashCountContainer);
			_smashCounter.ContentSize = CustomSize.resizeSpriteByDeviceResolutionBox(15.5f, 29.5f);
			_btnSmash.AddChild(_smashCounter);



			_btnClock = new CCSprite(ResourceManager.Instance.BtnClock);
			_btnClock.ContentSize = CustomSize.resizeSpriteByDeviceResolutionBox(5, 9.5f);
			_placeHolder.AddChild(_btnClock);

			_smashCount = new CCLabel($"{WordService.WordProperties[GameService.GameRound].id}", ResourceManager.Instance.FaceOffM54,
	   _smashCounter.ContentSize.Width, CCLabelFormat.SystemFont);
			_smashCount.Color = CCColor3B.Black;
			_smashCounter.AddChild(_smashCount);

			_timer = new CCLabel($"{WordService.WordProperties[GameService.GameRound].id}", ResourceManager.Instance.FaceOffM54,
			   _btnClock.ContentSize.Width / 2.5f, CCLabelFormat.SystemFont);
			_timer.Color = CCColor3B.Black;
			_btnClock.AddChild(_timer);

			if (AppSettings.ScreenResolution == ScreenResolution.HD)
			{
				pos = 0f;
			}
			else
			{
				pos = _btnSmash.ContentSize.Height / 3.5f;
			}

			SetListeners();
		}

		void SetListeners()
		{
			var listener = new CCEventListenerTouchOneByOne();
			listener.OnTouchCancelled = (touch, _event) => {
				var scale = new CCEaseElastic(new CCScaleTo(BUTTON_MOVE_TIME, 1), BUTTON_ELASTIC_TIME);
				if (_btnHome.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{

					_btnHome.RunAction(scale);

				}
				else if (_btnClock.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{
					_btnClock.RunAction(scale);
				}
				else if (_btnSmash.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{
					_btnSmash.RunAction(scale);
				}
			};
			listener.OnTouchBegan = (touch, _event) => {
				var scale = new CCEaseElastic(new CCScaleTo(BUTTON_MOVE_TIME, .90f), BUTTON_ELASTIC_TIME);
				if (_btnHome.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{

					_btnHome.RunAction(scale);

				}
				else if (_btnClock.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{
					_btnClock.RunAction(scale);
				}
				else if (_btnSmash.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{
					_btnSmash.RunAction(scale);
				}


				return true;
			};
			listener.OnTouchEnded = (touch, _event) => {
				var scale = new CCEaseElastic(new CCScaleTo(BUTTON_MOVE_TIME, 1), BUTTON_ELASTIC_TIME);
				if (_btnHome.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{

					_btnHome.RunAction(scale);
					AppSettings.GameModeSelected = GameMode.Variety;
					AppSettings.CurrentSceneType = SceneManagers.SceneType.Menu;
					SceneManagers.Instance.NavigateToScene(SceneManagers.SceneType.Loading);


				}
				else if (_btnClock.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{
					_btnClock.RunAction(scale);

				}
				else if (_btnSmash.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{
					_btnSmash.RunAction(scale);

				}

			};

			this.AddEventListener(listener);
		}

		protected override void AddedToScene()
		{
			base.AddedToScene();
			_placeHolder.Position = new CCPoint(Screen.DeviceWidth / 2, 0);
			_Footer.Position = new CCPoint(0, _Footer.ContentSize.Height / 2);
			_btnHome.Position = new CCPoint(-_placeHolder.ContentSize.Width / 2.25f, _Footer.ContentSize.Height + _Footer.ContentSize.Height / 2.75f);
			_btnClock.Position = new CCPoint(-_placeHolder.ContentSize.Width / 10, _Footer.ContentSize.Height + _Footer.ContentSize.Height / 1.5f);
			_timer.Position = new CCPoint(_btnClock.ContentSize.Width / 2, _btnClock.ContentSize.Height / 2);
			_btnSmash.Position = new CCPoint(_btnClock.PositionX + _btnSmash.ContentSize.Width + 10, _Footer.ContentSize.Height + _Footer.ContentSize.Height / 1.5f);
			_smashCounter.Position = new CCPoint(_btnSmash.PositionX + _btnSmash.ContentSize.Width / 3f, _btnSmash.PositionY + pos);
			_smashCount.Position = new CCPoint(_smashCounter.ContentSize.Width / 2, _smashCounter.ContentSize.Height / 2);
		}
	}
}
