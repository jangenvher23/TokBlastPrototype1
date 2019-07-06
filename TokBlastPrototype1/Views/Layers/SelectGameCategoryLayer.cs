
using TokBlastPrototype1.Views.Scenes;
using TokBlastPrototype1.Managers;
using System.Collections.Generic;

namespace TokBlastPrototype1.Views.Layers
{
    public class SelectGameCategoryLayer : BaseLayer
    {
        List<string> categoryNames = new List<string>()  {"Ability","Adversity","Aging","Attitude","Courage","Commitment",
                    "Creativity","Education","Faith","Family","Friendship","God","Government","Health","Humor","Kindness",
                    "Leadership","Love","Money","Opportunity","Patience","Perseverance","Peace","Politics","Quality",
                    "Service","Success","Time","Wisdom","Work"};
        public SelectGameCategoryLayer() : base()
        {


            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 3; i++)
                {

                }
            }
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();


        }

        public override SelectionScene.SelectionType GetSelectionType()
        {
            return SelectionScene.SelectionType.SelectCategory;
        }
    }
}
