using System;
using System.Collections.Generic;
using System.Text;
using CocosSharp;
using TokBlastPrototype1.Utilities;
using TokBlastPrototype1.Models.Enums;
namespace TokBlastPrototype1.Managers
{
    public class ResourceManager
    {
        private static readonly ResourceManager instance = new ResourceManager();
        public static ResourceManager Instance
        {
            get { return instance; }
        }


        public CCGameView GameView { get; private set; }



        private const string FontsContentPath = "Fonts";
        private const string SoundsContentPath = "Sounds";
        private const string ImageHdContentPath = "Images/Hd";
        private const string ImageLdContentPath = "Images/Ld";
        private const string AnimationContentPath = "Animation";
        private const string ParticlesContentPath = "Particles";

        // Menu Resource
        public string MenuBg { get; private set; }
        public string GameBg { get; private set; }
        public string MenuBlimp { get; private set; }
        public string MenuCloud { get; private set; }
        public string MenuCity { get; private set; }
        public string MenuPlane { get; private set; }
        public string MenuLogo { get; private set; }
        public string BtnPlay { get; private set; }
        public string BtnShop { get; private set; }
        public string BtnSettings { get; private set; }
        public string BtnProfile { get; private set; }
        public string BtnHowToPlay { get; private set; }

        // Splash Resource
        public string SplashLoadPanel { get; private set; }
        public string SplashLoadBar { get; private set; }
        public string SplashBG { get; private set; }
        public string SplashLogo { get; set; }

        //Fonts and Music Resource
        public string FaceOffM54 { get; private set; }
        public string ProximaNovaBold { get; private set; }
        public string ProximaNovaExtaBold { get; private set; }

        // Game Mode Resource
        public string BtnBlank { get; private set; }
        public string ModeBanner { get; private set; }
        public string BtnBack { get; private set; }

        public string BtnHome { get; private set; }

        // Game Resource
        public string SmashCountContainer { get; private set; }
        public string BtnClock { get; private set; }
        public string AnswerLetterContainer { get; private set; }

        public string ScreenFooter { get; private set; }

        public string LetterBankContainer { get; private set; }

        public string ProfilPicContainer { get; private set; }

        public string RoundContainer { get; private set; }

        public string GuessContainer { get; private set; }

        public string BtnSmash { get; private set; }
        public string BtnGameHome { get; private set; }
        public string WordContainer { get; private set; }
        public string GameBanner { get; set; }

        public void LoadFontResources()
        {
            FaceOffM54 = $"{FontsContentPath}/FaceOffM54.ttf";
            ProximaNovaBold = $"{FontsContentPath}/ProximaNova-Bold.ttf";
            ProximaNovaExtaBold = $"{FontsContentPath}/ProximaNovaCond-Xbold.otf";
        }

        public void LoadGameResources()
        {
            SmashCountContainer = $"circmon.png";
            BtnClock = $"clock.png";
            AnswerLetterContainer = $"fns.png";
            ScreenFooter = $"footer.png";
            LetterBankContainer = $"frame.png";
            ProfilPicContainer = $"part1.png";
            RoundContainer = $"part2.png";
            GuessContainer = $"part3.png";
            BtnSmash = $"smash.png";
            WordContainer = $"wordframe.png";
            BtnGameHome = $"home.png";

        }
        public void LoadMenuResources()
        {

            MenuBg = $"bg.png";
            GameBg = $"InGame.png";
            GameBanner = $"panelhead.png";
            MenuBlimp = $"blimp.png";
            MenuCloud = $"clouds.png";
            MenuCity = $"building.png";
            MenuPlane = $"plane.png";
            MenuLogo = $"loading_logo.png";
            BtnPlay = $"play.png";
            BtnSettings = $"settings1.png";
            BtnProfile = $"profile1.png";
            BtnHowToPlay = $"howtoplay1.png";
            BtnShop = $"shop1.png";
        }

        public void LoadGameModeResources()
        {
            BtnBlank = $"BlackButton.png";
            BtnBack = $"return.png";
            BtnHome = $"home.png";
            ModeBanner = $"panel_topBlank.png";
        }
        public void LoadSplashResources()
        {
            SplashLoadPanel = $"loading_panel.png";
            SplashLoadBar = $"loading_bar.png";
            SplashBG = $"loading_bg.png";
            SplashLogo = $"loading_logo.png";
        }

        // Setup the content search paths for fonts, music, sounds and textures 
        public void SetupContentPaths()
        {
            var contentSearchPaths = new List<string>() { FontsContentPath, SoundsContentPath };
            CCSizeI viewSize = GameView.ViewSize;
            CCSizeI designResolution = GameView.DesignResolution;

            // Determine whether to use the high or low def versions of our images
            // Make sure the default texel to content size ratio is set correctly
            // Of course you're free to have a finer set of image resolutions e.g (ld, hd, super-hd)
            if (designResolution.Width < viewSize.Width)
            {
                contentSearchPaths.Add(ImageHdContentPath);
                CCSprite.DefaultTexelToContentSizeRatio = 2.0f;
                AppSettings.ScreenResolution = ScreenResolution.HD;
            }
            else
            {
                //  contentSearchPaths.Add(ImageLdContentPath);
                contentSearchPaths.Add(ImageHdContentPath);
                CCSprite.DefaultTexelToContentSizeRatio = 2.0f;
                AppSettings.ScreenResolution = ScreenResolution.LD;
            }

            // contentSearchPaths.Add(ImageHdContentPath);
            GameView.ContentManager.SearchPaths = contentSearchPaths;
        }

        public void Ready(CCGameView gameView)
        {
            GameView = gameView;
        }

    }
}
