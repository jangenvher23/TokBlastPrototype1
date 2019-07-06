using CocosSharp;
using static TokBlastPrototype1.Managers.SceneManagers;
using TokBlastPrototype1.Views.Layers;
using TokBlastPrototype1.Managers;
namespace TokBlastPrototype1.Views.Scenes
{
    public class SelectionScene : BaseScene
    {
        public enum SelectionType
        {
            SelectGameOption,
            SelectGame,
            SelectDifficultyOption,
            SelectPlayer,
            SelectCategory,
            SelectSaved,
            SelectMode,
            Default
        }
        private BaseLayer _currentLayer;

        public SelectionScene(CCGameView gameView) : base(gameView)
        {
            var backgroundLayer = new BackgroundLayer();
            this.AddLayer(backgroundLayer, 0);

            var layer = new SelectGameTypeLayer();
            layer.Opacity = 0;
            this.AddLayer(layer, 1);
            _currentLayer = layer;

            var hudLayer = new SelectionHudLayer();
            hudLayer.Opacity = 0;
            this.AddLayer(hudLayer, 2);
        }

        public override SceneType GetSceneType()
        {
            return SceneType.GameMode;
        }

        public void NavigateToLayer(SelectionType selectionType)
        {
            BaseLayer layer = null;

            switch (selectionType)
            {
                case SelectionType.SelectGameOption:
                    layer = new SelectGameTypeLayer();
                    break;
                case SelectionType.SelectGame:
                    //    layer = new SelectGameLayer();
                    break;
                case SelectionType.SelectPlayer:
                    //    layer = new SelectPlayerLayer();
                    break;
                case SelectionType.SelectCategory:
                    //      layer = new SelectCategoryLayer();
                    break;
                case SelectionType.SelectSaved:
                    //     layer = new SelectSavedLayer();
                    break;
                case SelectionType.SelectMode:
                    //     layer = new SelectModeLayer();
                    break;
                case SelectionType.SelectDifficultyOption:
                    layer = new SelectDifficultyOptionLayer();
                    break;
            }

            if (layer != null)
            {
                this.RemoveChild(_currentLayer, false);
                layer.Opacity = 0;
                this.AddLayer(layer, 1);
                _currentLayer = layer;
            }
        }

        public void NavigateBack()
        {
            SelectionType selectionType = SelectionType.Default;

            switch (_currentLayer.GetSelectionType())
            {
                case SelectionType.SelectGameOption:
                    SceneManagers.Instance.NavigateToScene(SceneManagers.SceneType.Menu);
                    break;
                case SelectionType.SelectGame:
                    selectionType = SelectionType.SelectGameOption;
                    break;
                case SelectionType.SelectPlayer:
                    selectionType = SelectionType.SelectMode;
                    break;
                case SelectionType.SelectCategory:
                    selectionType = SelectionType.SelectMode;
                    break;
                case SelectionType.SelectSaved:
                    selectionType = SelectionType.SelectGame;
                    break;
                case SelectionType.SelectMode:
                    selectionType = SelectionType.SelectGame;
                    break;
                case SelectionType.SelectDifficultyOption:
                    selectionType = SelectionType.SelectGameOption;
                    break;

            }

            if (selectionType != SelectionType.Default)
            {
                NavigateToLayer(selectionType);
            }
        }

    }

}
