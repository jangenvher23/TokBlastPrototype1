using CocosSharp;
using TokBlastPrototype1.Utilities;
using TokBlastPrototype1.Services;
using TokBlastPrototype1.Managers;
using TokBlastPrototype1.Views.Scenes;
using static TokBlastPrototype1.Managers.SceneManagers;

namespace TokBlastPrototype1.Views.Layers
{
	public class BackgroundLayer : BaseLayer
	{


		private CCSprite _bg;

		public BackgroundLayer() : base()
		{

			_bg = new CCSprite(ResourceManager.Instance.MenuBg);
			_bg.ContentSize = new CCSize(Screen.GameWidth, Screen.GameHeight);
			_bg.AnchorPoint = CCPoint.AnchorMiddle;
			this.AddChild(_bg);

		}

		protected override void AddedToScene()
		{
			base.AddedToScene();
			var bound = VisibleBoundsWorldspace;
			_bg.Position = bound.Center;


		}
		/*
        public override void OnEnter()
        {
            base.OnEnter();

            var bounds = VisibleBoundsWorldspace;
            var finalPosition = new CCPoint(bounds.Center.X, Screen.DeviceHeight - _modeBanner.ContentSize.Height/2);
            var downAction = new CCEaseElastic(new CCMoveTo(BANNER_TRANSITION_TIME, finalPosition), BANNER_ELASTIC_TIME);
            //     BackgroundLayer.BannerSprite = UIService.CreateModeBanner("Select\nDifficulty", ResourceManager.Instance.FaceOffM54, 1f, 5.5f, 8.5f);
            _modeBanner.RunActions(downAction);
        }
        */
	}
}
