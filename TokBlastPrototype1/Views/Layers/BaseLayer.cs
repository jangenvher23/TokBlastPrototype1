using CocosSharp;
using static TokBlastPrototype1.Managers.SceneManagers;
using static TokBlastPrototype1.Views.Scenes.SelectionScene;
namespace TokBlastPrototype1.Views.Layers
{
    public class BaseLayer : CCLayerColor
    {
        protected const float BUTTON_MOVE_DISTANCE = 10f;
        protected const float BUTTON_MOVE_TIME = 0.3f;
        protected const float BUTTON_ELASTIC_TIME = 0.5f;
        protected const float BANNER_TRANSITION_TIME = 0.3f;
        protected const float BANNER_ELASTIC_TIME = 0.5f;

        public BaseLayer() : base(CCColor4B.Black)
        {
            // Set background color 


        }

        public virtual SelectionType GetSelectionType()
        {
            return SelectionType.Default;
        }
    }
}
