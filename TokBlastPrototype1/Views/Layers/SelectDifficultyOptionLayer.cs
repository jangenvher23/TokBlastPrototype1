using CocosSharp;
using TokBlastPrototype1.Services;
using TokBlastPrototype1.Utilities;
using TokBlastPrototype1.Models.Enums;
using TokBlastPrototype1.Managers;
using TokBlastPrototype1.Views.Scenes;

namespace TokBlastPrototype1.Views.Layers
{
	public class SelectDifficultyOptionLayer : BaseLayer
	{
		private CCSprite _modeBanner;
		private CCSprite _btnEasy;
		private CCSprite _btnModerate;
		private CCSprite _btnChallenging;
		private CCSprite _btnDifficult;
		public SelectDifficultyOptionLayer() : base()
		{
			AppSettings.GameDifficultySelected = Difficulties.Default;

			_modeBanner = UIService.CreateModeBanner("Difficulty", ResourceManager.Instance.FaceOffM54, 1f, 5.5f, 7.5f);
			this.AddChild(_modeBanner);
			_btnEasy = UIService.CreateNamedButton("Easy", ResourceManager.Instance.FaceOffM54, 6f, 1.75f, 8);
			this.AddChild(_btnEasy);
			_btnModerate = UIService.CreateNamedButton("Moderate", ResourceManager.Instance.FaceOffM54, 7.50f, 1.75f, 8);
			this.AddChild(_btnModerate);
			_btnChallenging = UIService.CreateNamedButton("Challenging", ResourceManager.Instance.FaceOffM54, 9.25f, 1.75f, 8);
			this.AddChild(_btnChallenging);
			_btnDifficult = UIService.CreateNamedButton("Difficult", ResourceManager.Instance.FaceOffM54, 9f, 1.75f, 8);
			this.AddChild(_btnDifficult);

			SetListeners();
		}

		protected override void AddedToScene()
		{
			base.AddedToScene();
			var additionalSpace = -15;
			var addSpacefirst = -additionalSpace;
			_modeBanner.Position = new CCPoint(Screen.DeviceWidth / 2, Screen.DeviceHeight + _modeBanner.ContentSize.Height / 2);
			_btnEasy.Position = new CCPoint(Screen.GameWidth / 2,
				Screen.GameHeight - (_modeBanner.ContentSize.Height + _btnEasy.ContentSize.Height / 1.75f + addSpacefirst));
			_btnModerate.Position = new CCPoint(Screen.GameWidth / 2, _btnEasy.PositionY - _btnModerate.ContentSize.Height + additionalSpace);
			_btnChallenging.Position = new CCPoint(Screen.GameWidth / 2, _btnModerate.PositionY - _btnChallenging.ContentSize.Height + additionalSpace);
			_btnDifficult.Position = new CCPoint(Screen.GameWidth / 2, _btnChallenging.PositionY - _btnDifficult.ContentSize.Height + additionalSpace);
		}

		void SetListeners()
		{

			var listener = new CCEventListenerTouchOneByOne();
			listener.OnTouchCancelled = (touch, _event) => {
				var scale = new CCEaseElastic(new CCScaleTo(BUTTON_MOVE_TIME, 1), BUTTON_ELASTIC_TIME);
				if (_btnEasy.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{

					_btnEasy.RunAction(scale);


				}
				else if (_btnModerate.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{
					_btnModerate.RunAction(scale);
				}
				else if (_btnChallenging.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{
					_btnChallenging.RunAction(scale);
				}
				else if (_btnDifficult.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{

					_btnDifficult.RunAction(scale);
				}
			};

			listener.OnTouchBegan = (touch, _event) => {
				var scale = new CCEaseElastic(new CCScaleTo(BUTTON_MOVE_TIME, .90f), BUTTON_ELASTIC_TIME);
				if (_btnEasy.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{

					_btnEasy.RunAction(scale);


				}
				else if (_btnModerate.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{
					_btnModerate.RunAction(scale);
				}
				else if (_btnChallenging.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{
					_btnChallenging.RunAction(scale);
				}
				else if (_btnDifficult.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{

					_btnDifficult.RunAction(scale);
				}

				return true;
			};

			listener.OnTouchEnded = (touch, _event) => {
				var scale = new CCEaseElastic(new CCScaleTo(BUTTON_MOVE_TIME, 1), BUTTON_ELASTIC_TIME);
				if (_btnEasy.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{

					_btnEasy.RunAction(scale);
					AppSettings.GameDifficultySelected = Difficulties.Easy;
					SceneManagers.Instance.NavigateToScene(SceneManagers.SceneType.Loading);
				}
				else if (_btnModerate.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{
					_btnModerate.RunAction(scale);
					AppSettings.GameDifficultySelected = Difficulties.Moderate;

					SceneManagers.Instance.NavigateToScene(SceneManagers.SceneType.Loading);

				}
				else if (_btnChallenging.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{

					_btnChallenging.RunAction(scale);
					AppSettings.GameDifficultySelected = Difficulties.Challenging;

					SceneManagers.Instance.NavigateToScene(SceneManagers.SceneType.Loading);
				}
				else if (_btnDifficult.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
				{

					_btnDifficult.RunAction(scale);
					AppSettings.GameDifficultySelected = Difficulties.Difficult;

					SceneManagers.Instance.NavigateToScene(SceneManagers.SceneType.Loading);
				}
			};

			this.AddEventListener(listener);
		}

		public override SelectionScene.SelectionType GetSelectionType()
		{
			return SelectionScene.SelectionType.SelectDifficultyOption;
		}
		public override void OnEnter()
		{
			base.OnEnter();

			var bounds = VisibleBoundsWorldspace;
			var finalPosition = new CCPoint(Screen.DeviceWidth / 2, Screen.DeviceHeight - _modeBanner.ContentSize.Height / 2);
			var downAction = new CCEaseElastic(new CCMoveTo(BANNER_TRANSITION_TIME, finalPosition), BANNER_ELASTIC_TIME);
			//     BackgroundLayer.BannerSprite = UIService.CreateModeBanner("Select\nDifficulty", ResourceManager.Instance.FaceOffM54, 1f, 5.5f, 8.5f);
			_modeBanner.RunActionAsync(downAction);
		}
	}
}
