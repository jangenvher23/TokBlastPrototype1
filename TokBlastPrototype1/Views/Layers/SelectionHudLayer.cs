using CocosSharp;
using TokBlastPrototype1.Utilities;
using TokBlastPrototype1.Services;
using TokBlastPrototype1.Managers;
using TokBlastPrototype1.Views.Scenes;
namespace TokBlastPrototype1.Views.Layers
{
	public class SelectionHudLayer : BaseLayer
	{
		private CCSprite _btnBack;
		private CCSprite _btnHome;
		public SelectionHudLayer() : base()
		{

			_btnBack = new CCSprite(ResourceManager.Instance.BtnBack);

			_btnBack.ContentSize = CustomSize.resizeSpriteByDeviceResolutionBox(9.5f, 14.5f);
			this.AddChild(_btnBack);

			_btnHome = new CCSprite(ResourceManager.Instance.BtnHome);

			_btnHome.ContentSize = CustomSize.resizeSpriteByDeviceResolutionBox(9.5f, 14.5f);
			this.AddChild(_btnHome);
			SetListeners();
		}

		void SetListeners()
		{

			var listener = new CCEventListenerTouchOneByOne();
			listener.OnTouchCancelled = (touch, _event) => {
				var scale = new CCEaseElastic(new CCScaleTo(BUTTON_MOVE_TIME, 1), BUTTON_ELASTIC_TIME);
				if (_btnBack.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{

					_btnBack.RunAction(scale);


				}
				else if (_btnHome.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{
					_btnHome.RunAction(scale);
				}



			};
			listener.OnTouchBegan = (touch, _event) => {
				var scale = new CCEaseElastic(new CCScaleTo(BUTTON_MOVE_TIME, .90f), BUTTON_ELASTIC_TIME);
				if (_btnBack.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{

					_btnBack.RunAction(scale);

				}
				else if (_btnHome.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{
					_btnHome.RunAction(scale);
				}

				return true;
			};
			listener.OnTouchEnded = (touch, _event) => {
				var scale = new CCEaseElastic(new CCScaleTo(BUTTON_MOVE_TIME, 1), BUTTON_ELASTIC_TIME);
				if (_btnBack.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{

					_btnBack.RunAction(scale);
					((SelectionScene)Parent).NavigateBack();



				}
				else if (_btnHome.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{
					_btnHome.RunAction(scale);
					SceneManagers.Instance.NavigateToScene(SceneManagers.SceneType.Menu);
				}

			};

			this.AddEventListener(listener);
		}
		protected override void AddedToScene()
		{
			base.AddedToScene();
			_btnBack.Position = new CCPoint(
			   Screen.GameWidth / 10f
				, Screen.GameHeight - Screen.GameHeight / 14.5f);
			_btnHome.Position = new CCPoint(
				  Screen.GameWidth - Screen.GameWidth / 10f,
						  Screen.GameHeight - Screen.GameHeight / 14.5f
				);

		}
	}
}
