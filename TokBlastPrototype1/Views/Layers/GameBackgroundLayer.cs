using CocosSharp;
using TokBlastPrototype1.Utilities;
using TokBlastPrototype1.Services;
using TokBlastPrototype1.Managers;
using TokBlastPrototype1.Views.Scenes;
using static TokBlastPrototype1.Managers.SceneManagers;
namespace TokBlastPrototype1.Views.Layers
{
	public class GameBackgroundLayer : BaseLayer
	{

		private CCSprite _bg;
		private BaseScene _currentScene;

		public GameBackgroundLayer() : base()
		{

			_bg = new CCSprite(ResourceManager.Instance.GameBg);
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

	}
}
