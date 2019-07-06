using CocosSharp;
using TokBlastPrototype1.Utilities;
using TokBlastPrototype1.Managers;
using TokBlastPrototype1.Views.Layers;
using TokBlastPrototype1.Models.Local;
namespace TokBlastPrototype1.Services
{
    public class UIService
    {

        public static CCSprite CreateModeBanner(string Name, string fontFamily, float bannerWidth = 1, float bannerHieght = 1, float fontSize = 1)
        {
            var sprite = new CCSprite(ResourceManager.Instance.ModeBanner);
            sprite.ContentSize = CustomSize.resizeSpriteByDeviceResolutionBox(bannerWidth, bannerHieght);
            var label = new CCLabel(Name, fontFamily, Screen.DeviceWidth / fontSize, CCLabelFormat.SystemFont);
            label.Position = new CCPoint(sprite.ContentSize.Width / 2, sprite.ContentSize.Height / 1.75f);

            sprite.AddChild(label);
            return sprite;
        }

        public static CCSprite CreateNamedButton(string Name, string fontFamily, float fontSize = 4, float ButtonScaleWidth = 1, float ButtonScaleHeight = 1)
        {

            var sprite = new CCSprite(ResourceManager.Instance.BtnBlank);



            sprite.ContentSize = CustomSize.resizeSpriteByDeviceResolutionBox(ButtonScaleWidth, ButtonScaleHeight);
            var label = new CCLabel(Name, fontFamily, Screen.DeviceWidth / fontSize, CCLabelFormat.SystemFont);
            label.Position = new CCPoint(sprite.ContentSize.Width / 2, sprite.ContentSize.Height / 2f);

            sprite.AddChild(label);
            return sprite;
        }

        public static void CreateGameBoardBy(WordProperties wordProperties)
        {
            if (wordProperties.number_of_words >= 21 && wordProperties.number_of_words <= 28)
            {
                GameLayer.ColoumnSetter = 4;
                GameLayer.RowSetter = 7;
            }
            else if (wordProperties.number_of_words >= 13 && wordProperties.number_of_words <= 20)
            {
                GameLayer.ColoumnSetter = 4;
                GameLayer.RowSetter = 5;
            }
            else if (wordProperties.number_of_words >= 3 && wordProperties.number_of_words <= 12)
            {
                GameLayer.ColoumnSetter = 3;
                GameLayer.RowSetter = 4;
            }
        }

    }
}
