using System;
using TokBlastPrototype1.Utilities;
using CocosSharp;
using TokBlastPrototype1.Managers;
namespace TokBlastPrototype1.Views.Layers
{
	public class MenuBackgroundLayer : BaseLayer
	{

		private CCSprite _bg;
		private CCSprite _cloud;
		private CCSprite _city;
		private CCSprite _blimp;
		private CCSprite _plane;



		public MenuBackgroundLayer() : base()
		{
			_bg = new CCSprite(ResourceManager.Instance.MenuBg);
			_bg.ContentSize = new CCSize(Screen.GameWidth, Screen.GameHeight);
			_bg.AnchorPoint = CCPoint.AnchorMiddle;
			this.AddChild(_bg);

			//_blimp = new CCSprite(ResourceManager.Instance.MenuBlimp);
			//_blimp.ContentSize = CustomSize.resizeSpriteByDeviceResolutionBox(1f, 4);
			//_bg.AddChild(_blimp, 0);

			//_cloud = new CCSprite(ResourceManager.Instance.MenuCloud);
			//_cloud.ContentSize = CustomSize.SpriteLogoSize();
			//_bg.AddChild(_cloud, 0);

			//_city = new CCSprite(ResourceManager.Instance.MenuCity);
			//_city.ContentSize = CustomSize.resizeSpriteByDeviceResolutionBox(1f, 3.5f);
			//_bg.AddChild(_city, 1);

			//_plane = new CCSprite(ResourceManager.Instance.MenuPlane);
			//_plane.ContentSize = CustomSize.resizeSpriteByDeviceResolutionBox(2.75f, 12); ;
			//_bg.AddChild(_plane, 2);



		}

		protected override void AddedToScene()
		{
			base.AddedToScene();
			var bounds = VisibleBoundsWorldspace;
            _bg.Position = bounds.Center;
            //_blimp.Position = new CCPoint(Screen.DeviceWidth / 2, _bg.ContentSize.Height - _blimp.ContentSize.Width / 2.75f);
            //_cloud.Position = new CCPoint(Screen.DeviceWidth / 2, _cloud.ContentSize.Height + 25);
            //_city.Position = new CCPoint(Screen.DeviceWidth / 2, _city.ContentSize.Height / 2);
            //_plane.Position = new CCPoint(Screen.DeviceWidth * 2.5f, Screen.DeviceHeight / 2);



            //_blimp.AnimationInLineRepeats(1.5f, 1.5f, _blimp.PositionX, _blimp.PositionX, _blimp.PositionY + 10, _blimp.PositionY - 10);
            //_cloud.AnimationInOneLineFade(15f, 15f, -_cloud.ContentSize.Width, _cloud.ContentSize.Width * 2.5f, _cloud.PositionY, _cloud.PositionY);
            //_plane.AnimationInOneLineFade(5f, 5f, Screen.DeviceWidth * 2.5f, -Screen.DeviceWidth, Screen.DeviceHeight / 2, Screen.DeviceHeight / 2);


        }
	}
}
