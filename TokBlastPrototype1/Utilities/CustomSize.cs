using CocosSharp;
namespace TokBlastPrototype1.Utilities
{
    public static class CustomSize
    {
        /// <summary>
        /// Returns the A size that is suited for a logo
        /// </summary>
        public static CCSize SpriteLogoSize(float width = 150, float height = -150, float DivideBy = 2)
        {
            CCSize size = new CCSize((Screen.GameWidth / DivideBy) + width, (Screen.GameHeight / DivideBy) + height);
            return size;
        }

        /// <summary>
        /// Resizes a sprite, 
        /// </summary>
        public static CCSize resizeSprite(this CCSprite sprite, float width = 0, float height = 0, float DivideForWidth = 2, float DivideForHeight = 2, bool insideSprite = false, CCSprite parentSprite = null)
        {
            CCSize size = new CCSize();
            if (insideSprite)
            {
                size = new CCSize(parentSprite.ContentSize.Width / DivideForWidth + width, parentSprite.ContentSize.Height / DivideForHeight + height);
            }
            else
            {
                size = new CCSize(sprite.ContentSize.Width / DivideForWidth + width, sprite.ContentSize.Height / DivideForHeight + height);
            }

            return size;
        }

        public static CCSize resizeSpriteByDeviceResolution(float DivideForWidth = 2, float DivideForHeight = 2)
        {

            var size = new CCSize(Screen.GameWidth - Screen.GameWidth / DivideForWidth, Screen.GameHeight / DivideForHeight);

            return size;
        }
        public static CCSize resizeSpriteBy(float DivideForWidth = 2, float DivideForHeight = 2, float width = 0, float height = 0)
        {

            var size = new CCSize(Screen.GameWidth - (Screen.GameWidth / DivideForWidth + width), Screen.GameHeight - (Screen.GameHeight / DivideForHeight - height));

            return size;
        }

        public static CCSize resizeSpriteByDeviceResolutionBox(float DivideForWidth = 2, float DivideForHeight = 2)
        {

            var size = new CCSize(Screen.GameWidth / DivideForWidth, Screen.GameHeight / DivideForHeight);

            return size;
        }



    }
}
