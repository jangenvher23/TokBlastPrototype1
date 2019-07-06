using CocosSharp;
using TokBlastPrototype1.Utilities;
using TokBlastPrototype1.Services;
using TokBlastPrototype1.Managers;
using System.Collections.Generic;
using TokBlastPrototype1.Models.Enums;
using TokBlastPrototype1.Views.Scenes;

namespace TokBlastPrototype1.Views.Layers
{
    public class SelectGameTypeLayer : BaseLayer
    {
        private CCSprite _modeBanner;
        private CCSprite _btnVariety;
        private CCSprite _btnCategory;
        private CCSprite _btnMultiplayer;
        private CCSprite _btnSavedGames;
        public SelectGameTypeLayer() : base()
        {


            //   List<string> NamesBtn = new List<string>() { "Variety", "Category", "Multiplayer","Saved Games" };
            AppSettings.GameModeSelected = GameMode.Default;
            AppSettings.CategorySelected = string.Empty;
            AppSettings.GameDifficultySelected = Difficulties.Default;
            AppSettings.PlayerTypeSelected = PlayerType.Default;
            AppSettings.CurrentSceneType = SceneManagers.SceneType.GameMode;

            _modeBanner = UIService.CreateModeBanner("Game Mode", ResourceManager.Instance.FaceOffM54, 1f, 5.5f, 7.5f);
            this.AddChild(_modeBanner);
            _btnVariety = UIService.CreateNamedButton("Variety", ResourceManager.Instance.FaceOffM54, 7.5f, 1.75f, 8);
            this.AddChild(_btnVariety);
            _btnCategory = UIService.CreateNamedButton("Category", ResourceManager.Instance.FaceOffM54, 7.5f, 1.75f, 8);
            this.AddChild(_btnCategory);
            _btnMultiplayer = UIService.CreateNamedButton("Multiplayer", ResourceManager.Instance.FaceOffM54, 9.75f, 1.75f, 8);
            this.AddChild(_btnMultiplayer);
            _btnSavedGames = UIService.CreateNamedButton("Saved Games", ResourceManager.Instance.FaceOffM54, 9.75f, 1.75f, 8);
            this.AddChild(_btnSavedGames);

            SetListeners();
        }

        void SetListeners()
        {

            var listener = new CCEventListenerTouchOneByOne();
            listener.OnTouchCancelled = (touch, _event) => {
                var scale = new CCEaseElastic(new CCScaleTo(BUTTON_MOVE_TIME, 1), BUTTON_ELASTIC_TIME);
                if (_btnVariety.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {

                    _btnVariety.RunAction(scale);

                }
                else if (_btnCategory.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {
                    _btnCategory.RunAction(scale);
                }
                else if (_btnMultiplayer.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {
                    _btnMultiplayer.RunAction(scale);
                }
            };
            listener.OnTouchBegan = (touch, _event) => {
                var scale = new CCEaseElastic(new CCScaleTo(BUTTON_MOVE_TIME, .90f), BUTTON_ELASTIC_TIME);
                if (_btnVariety.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {

                    _btnVariety.RunAction(scale);

                }
                else if (_btnCategory.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {
                    _btnCategory.RunAction(scale);
                }
                else if (_btnMultiplayer.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {
                    _btnMultiplayer.RunAction(scale);
                }
                else if (_btnSavedGames.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {

                    _btnSavedGames.RunAction(scale);
                }

                return true;
            };
            listener.OnTouchEnded = (touch, _event) => {
                var scale = new CCEaseElastic(new CCScaleTo(BUTTON_MOVE_TIME, 1), BUTTON_ELASTIC_TIME);
                if (_btnVariety.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {

                    _btnVariety.RunAction(scale);
                    AppSettings.GameModeSelected = GameMode.Variety;
                    AppSettings.CurrentSceneType = SceneManagers.SceneType.Game;
                    ((SelectionScene)Parent).NavigateToLayer(SelectionScene.SelectionType.SelectDifficultyOption);


                }
                else if (_btnCategory.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {
                    _btnCategory.RunAction(scale);

                }
                else if (_btnMultiplayer.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {
                    _btnMultiplayer.RunAction(scale);

                }
                else if (_btnSavedGames.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {

                    _btnSavedGames.RunAction(scale);

                }
            };

            this.AddEventListener(listener);
        }

        public override SelectionScene.SelectionType GetSelectionType()
        {
            return SelectionScene.SelectionType.SelectGameOption;
        }
        protected override void AddedToScene()
        {
            base.AddedToScene();
            var additionalSpace = -15;
            var addSpacefirst = -additionalSpace;
            _modeBanner.Position = new CCPoint(Screen.DeviceWidth / 2, Screen.DeviceHeight + _modeBanner.ContentSize.Height / 2);
            _btnVariety.Position = new CCPoint(Screen.GameWidth / 2,
                Screen.GameHeight - (_modeBanner.ContentSize.Height + _btnVariety.ContentSize.Height / 1.75f + addSpacefirst));
            _btnCategory.Position = new CCPoint(Screen.GameWidth / 2, _btnVariety.PositionY - _btnCategory.ContentSize.Height + additionalSpace);
            _btnMultiplayer.Position = new CCPoint(Screen.GameWidth / 2, _btnCategory.PositionY - _btnMultiplayer.ContentSize.Height + additionalSpace);
            _btnSavedGames.Position = new CCPoint(Screen.GameWidth / 2, _btnMultiplayer.PositionY - _btnSavedGames.ContentSize.Height + additionalSpace);
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
