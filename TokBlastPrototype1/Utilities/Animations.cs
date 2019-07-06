
using CocosSharp;
namespace TokBlastPrototype1.Utilities
{
    public static class Animations
    {

        public static CCActionState AnimationInLineRepeats(this CCSprite sprite, float duration1, float duration2, float positionX1, float positionX2, float positionY1, float positionY2)
        {
            var Animate1 = new CCMoveTo(duration1, new CCPoint(positionX1, positionY1));
            var Animate2 = new CCMoveTo(duration2, new CCPoint(positionX2, positionY2));
            var TheAnimation = new CCFiniteTimeAction[2]; TheAnimation[0] = Animate1; TheAnimation[1] = Animate2;
            var RunAnimation = new CCRepeatForever(TheAnimation);
            return sprite.RunAction(RunAnimation);
        }

        public static CCActionState AnimationInOneLineFade(this CCSprite sprite, float duration1, float duration2, float positionX1, float positionX2, float positionY1, float positionY2)
        {
            var Animate1 = new CCMoveTo(duration1, new CCPoint(positionX1, positionY1));
            var FadeIn = new CCFadeIn(duration1 / 2);
            var FadeOut = new CCFadeOut(duration2 / 2);
            var Animate2 = new CCMoveTo(duration2, new CCPoint(positionX2, positionY2));

            var TheAnimation = new CCFiniteTimeAction[4]; TheAnimation[0] = FadeOut; TheAnimation[1] = Animate1; TheAnimation[2] = FadeIn; TheAnimation[3] = Animate2;
            var RunAnimation = new CCRepeatForever(TheAnimation);
            return sprite.RunAction(RunAnimation);
        }

        public static CCActionState ButtonAnimation(this CCSprite sprite, float duration1, float positionX1, float positionY1)
        {
            var Animate1 = new CCMoveTo(duration1, new CCPoint(positionX1, positionY1));
            //     var Animate2 = new CCMoveTo(duration2, new CCPoint(positionX2, positionY2));
            var TheAnimation = Animate1;// TheAnimation[0] = Animate1; TheAnimation[1] = Animate2;
            var RunAnimation = new CCRepeat(TheAnimation, 1);
            return sprite.RunAction(RunAnimation);
        }





    }
}
