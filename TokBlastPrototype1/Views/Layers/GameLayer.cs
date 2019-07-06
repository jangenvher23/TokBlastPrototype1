using CocosSharp;
using TokBlastPrototype1.Managers;
using TokBlastPrototype1.Utilities;
using TokBlastPrototype1.Services;
using TokBlastPrototype1.Models.Enums;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;
using TokBlastPrototype1.Views.Scenes;

namespace TokBlastPrototype1.Views.Layers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class GameLayer : BaseLayer
    {
        #region Init UI variables
        private CCSprite ProfileBoard;
        private CCSprite GameBoard = new CCSprite();
        private CCSprite LetterBoard = new CCSprite();
        private CCSprite BankBoard = new CCSprite();
        private CCSprite[,] AnswerBank;
        //Letter Board
        private CCSprite[] _boxLetter;
        private CCLabel[] _textsLetter;
        //Game Board
        private CCSprite[,] box;
        CCLabel[,] texts;

        private CCLabel[,] _bankText;

        #endregion

        #region Init Logic Variables

        //GameBoard

        private string[] WordBeingPlayed;

        private List<CCSprite> ListedSpritesBank = new List<CCSprite>();
        private List<CCSprite> ListedSpriteLetter = new List<CCSprite>();
        private List<CCSprite> ListedGameBoards = new List<CCSprite>();
        private string[] _answerContent;
        private string[,] _bankContent;
        #endregion

        #region   Properties
        public static int RowSetter { get; set; }
        public static int ColoumnSetter { get; set; }
        public static int WordLength { get; set; }

        public static int LetterLength { get; set; }
        public static char[] GuessWord { get; set; }

        public static int GameRound { get; set; }
        public static int RandomWordsCountCol { get; set; }
        public static int RandomWordsCountRow { get; set; }

        private float Longest_Word_Length { get; set; }

        private static int GuessCount { get; set; }

        private static int GuessLetter { get; set; }

        #endregion
        #region   Fields
        private GameScene _scene;
        private GameHudLayer _gameHudLayer;
        #endregion

        public GameLayer(CCScene scene) : base()
        {

            GuessCount = 1;
            GuessLetter = 1;
            GameRound = 0;
            InitGameBase();
            InitGameProfiles();
            GameStartAtRound(GameService.GameRound);
            GameInit();
            StartRound();

        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            var boardpos = (Screen.DeviceHeight / 2.50f) + (Screen.DeviceHeight / 5f);
            var letpos = (Screen.DeviceHeight / 2.50f) + (Screen.DeviceHeight / 5f) + (Screen.DeviceHeight / 9f);
            var bankpos = (Screen.DeviceHeight / 2.50f) + (Screen.DeviceHeight / 5f) + (Screen.DeviceHeight / 9.25f) + (Screen.DeviceHeight / 5.5f);
            ProfileBoard.Position = new CCPoint(Screen.DeviceWidth / 2, Screen.DeviceHeight - Screen.DeviceHeight / 9.5f);
            GameBoard.Position = new CCPoint(0, Screen.DeviceHeight - (boardpos + 10));

            LetterBoard.Position = new CCPoint(0, Screen.DeviceHeight - letpos);
            BankBoard.Position = new CCPoint(0, Screen.DeviceHeight - (bankpos - 17));
        }

        #region UI Creation
        private void InitGameProfiles()
        {
            ProfileBoard = new CCSprite();
            ProfileBoard.ContentSize = new CCSize(Screen.DeviceWidth, Screen.DeviceHeight / 5.25f);

            var _gameBanner = new CCSprite(ResourceManager.Instance.GameBanner);
            _gameBanner.ContentSize = new CCSize(Screen.DeviceWidth, Screen.DeviceHeight / 5.25f);
            _gameBanner.Position = new CCPoint(ProfileBoard.PositionX, ProfileBoard.PositionY);
            ProfileBoard.AddChild(_gameBanner, 0);

            #region Player 1

            var Player1 = new CCSprite();
            Player1.ContentSize = new CCSize(App.ScreenWidth / 4.5f, ProfileBoard.ContentSize.Height);
            Player1.Position = new CCPoint(-ProfileBoard.ContentSize.Width / 1.75f, ProfileBoard.PositionY + Player1.ContentSize.Height / 7.75f);

            ProfileBoard.AddChild(Player1, 1);
            //Profile
            var part1_1 = new CCSprite(ResourceManager.Instance.ProfilPicContainer);
            part1_1.ContentSize = new CCSize(Player1.ContentSize.Width, Player1.ContentSize.Height / 2.15f);
            part1_1.Position = new CCPoint(part1_1.ContentSize.Width, ProfileBoard.PositionY);
            Player1.AddChild(part1_1, 1);

            var _p1Profile = InitProfilePic1();
            _p1Profile.ContentSize = new CCSize(Player1.ContentSize.Width - 9.5f, Player1.ContentSize.Height / 2.2f);
            _p1Profile.Position = new CCPoint(part1_1.ContentSize.Width, ProfileBoard.PositionY);
            Player1.AddChild(_p1Profile, 0);

            var label1Points = new CCLabel("0", ResourceManager.Instance.FaceOffM54, part1_1.ContentSize.Width / 3.5f, CCLabelFormat.SystemFont);
            label1Points.Position = new CCPoint(part1_1.ContentSize.Width - part1_1.ContentSize.Width / 4.25f, part1_1.ContentSize.Height - part1_1.ContentSize.Height / 1.15f);
            label1Points.Color = CCColor3B.Yellow;
            part1_1.AddChild(label1Points);
            //Round Counter
            var part2_1 = new CCSprite(ResourceManager.Instance.RoundContainer);
            part2_1.ContentSize = new CCSize(Player1.ContentSize.Width, Player1.ContentSize.Height / 6.5f);
            part2_1.Position = new CCPoint(part1_1.PositionX, part1_1.PositionY - part1_1.ContentSize.Height / 1.55f);
            Player1.AddChild(part2_1);

            var label1Round = new CCLabel("Round: ", ResourceManager.Instance.ProximaNovaExtaBold, part2_1.ContentSize.Height, CCLabelFormat.SystemFont);
            label1Round.Color = CCColor3B.Black;
            label1Round.Position = new CCPoint(part2_1.ContentSize.Width / 3.6f, part2_1.ContentSize.Height / 2);
            part2_1.AddChild(label1Round);

            //Guess Counter
            var part3_1 = new CCSprite(ResourceManager.Instance.GuessContainer);
            part3_1.ContentSize = new CCSize(Player1.ContentSize.Width, Player1.ContentSize.Height / 6.5f);
            part3_1.Position = new CCPoint(part2_1.PositionX, part2_1.PositionY - part2_1.ContentSize.Height / 1.005f);
            Player1.AddChild(part3_1);

            var label1Guess = new CCLabel("Guess: ", ResourceManager.Instance.ProximaNovaExtaBold, part3_1.ContentSize.Height, CCLabelFormat.SystemFont);
            label1Guess.Color = CCColor3B.Black;
            label1Guess.Position = new CCPoint(part3_1.ContentSize.Width / 3.6f, part3_1.ContentSize.Height / 2);
            part3_1.AddChild(label1Guess);
            #endregion

            #region Player 2

            var Player2 = new CCSprite();
            Player2.ContentSize = new CCSize(App.ScreenWidth / 4.5f, ProfileBoard.ContentSize.Height);
            Player2.Position = new CCPoint(ProfileBoard.ContentSize.Width / 7.75f, ProfileBoard.PositionY + Player2.ContentSize.Height / 7.75f);
            ProfileBoard.AddChild(Player2, 1);


            //Profile
            var part1_2 = new CCSprite(ResourceManager.Instance.ProfilPicContainer);
            part1_2.ContentSize = new CCSize(Player2.ContentSize.Width, Player2.ContentSize.Height / 2.15f);
            part1_2.Position = new CCPoint(part1_2.ContentSize.Width, ProfileBoard.PositionY);
            Player2.AddChild(part1_2, 1);


            var _p2Profile = InitProfilePic2();
            _p2Profile.ContentSize = new CCSize(Player2.ContentSize.Width - 9.5f, Player2.ContentSize.Height / 2.2f);
            _p2Profile.Position = new CCPoint(part1_2.ContentSize.Width, ProfileBoard.PositionY);
            Player2.AddChild(_p2Profile, 0);


            var label2Points = new CCLabel("0", ResourceManager.Instance.FaceOffM54, part1_2.ContentSize.Width / 3.5f, CCLabelFormat.SystemFont);
            label2Points.Position = new CCPoint(part1_2.ContentSize.Width - part1_2.ContentSize.Width / 4.25f, part1_2.ContentSize.Height - part1_2.ContentSize.Height / 1.15f);
            label2Points.Color = CCColor3B.Yellow;
            part1_2.AddChild(label2Points);
            //Round Counter
            var part2_2 = new CCSprite(ResourceManager.Instance.RoundContainer);
            part2_2.ContentSize = new CCSize(Player2.ContentSize.Width, Player2.ContentSize.Height / 6.5f);
            part2_2.Position = new CCPoint(part1_2.PositionX, part1_2.PositionY - part1_2.ContentSize.Height / 1.55f);
            Player2.AddChild(part2_2);

            var label2Round = new CCLabel("Round: ", ResourceManager.Instance.ProximaNovaExtaBold, part2_2.ContentSize.Height, CCLabelFormat.SystemFont);
            label2Round.Color = CCColor3B.Black;
            label2Round.Position = new CCPoint(part2_2.ContentSize.Width / 3.6f, part2_2.ContentSize.Height / 2);
            part2_2.AddChild(label2Round);

            //Guess Counter
            var part3_2 = new CCSprite(ResourceManager.Instance.GuessContainer);
            part3_2.ContentSize = new CCSize(Player2.ContentSize.Width, Player2.ContentSize.Height / 6.5f);
            part3_2.Position = new CCPoint(part2_2.PositionX, part2_2.PositionY - part2_2.ContentSize.Height / 1.005f);
            Player2.AddChild(part3_2);

            var label2Guess = new CCLabel("Guess: ", ResourceManager.Instance.ProximaNovaExtaBold, part3_2.ContentSize.Height, CCLabelFormat.SystemFont);
            label2Guess.Color = CCColor3B.Black;
            label2Guess.Position = new CCPoint(part3_2.ContentSize.Width / 3.6f, part3_2.ContentSize.Height / 2);
            part3_2.AddChild(label2Guess);
            #endregion
            this.AddChild(ProfileBoard);
        }

        public CCSprite InitProfilePic1()
        {
            /*var webClient = new HttpClient();
            var streamImage = webClient.GetByteArrayAsync("https://upload.wikimedia.org/wikipedia/commons/0/0e/Lakeyboy_Silhouette.PNG").Result;
            CCTexture2D texture = new CCTexture2D(streamImage);
            var sprite = new CCSprite(texture);
            */
            var sprite = new CCSprite();
            return sprite;
        }
        public CCSprite InitProfilePic2()
        {
            /*  var webClient = new HttpClient();
              var streamImage = webClient.GetByteArrayAsync("https://upload.wikimedia.org/wikipedia/commons/0/0e/Lakeyboy_Silhouette.PNG").Result;
              CCTexture2D texture = new CCTexture2D(streamImage);
               var sprite = new CCSprite(texture);
              */
            var sprite = new CCSprite();
            return sprite;
        }

        private void InitGameBoard()
        {
            #region Original
            GameBoard.ContentSize = new CCSize(Screen.DeviceWidth, Screen.DeviceHeight / 2.50f);
            #region Grid Logic

            box = new CCSprite[RowSetter, ColoumnSetter];
            texts = new CCLabel[RowSetter, ColoumnSetter];
            float DivideHeightBy2 = GameBoard.ContentSize.Height / 2, DividedWidthBy2 = GameBoard.ContentSize.Width / 2;
            float DivideHeightBy4 = GameBoard.ContentSize.Height / 4, DividedWidthBy4 = GameBoard.ContentSize.Width / 4;
            float fixedWidth = GameBoard.ContentSize.Width, fixedHeight = GameBoard.ContentSize.Height;
            float WidthDividend = 3.10f, HeightDividend = 6, AdjusterValueWidth = 1.05f, AdjusterValueHeight = 1.3f, AdjustPositionHeigth = 2.25f, AdjustPositionWidth = 2;
            float BoxWidth = 8.5f, BoxHeight = 8.5f;
            // float ExtendY = 0, ExtendX = 0;

            if (RowSetter >= 5 && RowSetter <= 7)
            {
                int heightControl = RowSetter - 5;
                for (int i = 0; i < heightControl; i++)
                {
                    HeightDividend += 1.5f;

                    if (RowSetter == 5)
                        AdjusterValueHeight -= .15f;
                    else
                        AdjusterValueHeight -= .0055f;


                }
            }
            if (ColoumnSetter > 3)
            {
                int widthControl = ColoumnSetter - 2;
                for (int i = 0; i < widthControl; i++)
                {
                    WidthDividend += .5f;
                    if (i > 0)
                    {
                        AdjusterValueWidth -= .01f;
                    }

                }
            }


            float _Width = fixedWidth / WidthDividend;
            int numWordSet = 0;
            for (int row = 0; row < RowSetter; row++)
            {

                for (int col = 0; col < ColoumnSetter; col++)
                {

                    box[row, col] = new CCSprite(ResourceManager.Instance.WordContainer);
                    texts[row, col] = new CCLabel($"{WordBeingPlayed[numWordSet].ToUpper()}", ResourceManager.Instance.FaceOffM54, box[row, col].BoundingBoxTransformedToWorld.Size.Width / 3.5f);
                    texts[row, col].Color = CCColor3B.Black;
                    box[row, col].ContentSize = new CCSize(fixedWidth / WidthDividend, fixedHeight / HeightDividend);
                    if (row == 0)
                    {
                        if (col == 0)
                        {
                            box[row, col].Position = new CCPoint((fixedWidth / WidthDividend) / 2, fixedHeight - (fixedHeight / HeightDividend) / AdjustPositionHeigth);
                        }
                        else
                        {
                            box[row, col].Position = new CCPoint(box[row, col - 1].PositionX + box[row, col - 1].ContentSize.Width * AdjusterValueWidth, fixedHeight - (fixedHeight / HeightDividend) / AdjustPositionHeigth);
                        }
                    }
                    else
                    {



                        if (col == 0)
                        {
                            box[row, col].Position = new CCPoint((fixedWidth / WidthDividend) / 2, box[row - 1, col].PositionY - box[row - 1, col].ContentSize.Height * AdjusterValueHeight);
                        }
                        else
                        {
                            box[row, col].Position = new CCPoint(box[row, col - 1].PositionX + box[row, col - 1].ContentSize.Width * AdjusterValueWidth, box[row - 1, col].PositionY - box[row - 1, col].ContentSize.Height * AdjusterValueHeight);
                        }


                    }

                    texts[row, col].Position = new CCPoint(box[row, col].ContentSize.Width / 2, box[row, col].ContentSize.Height / 2);
                    if (numWordSet < WordLength)
                    {
                        GameBoard.AddChild(box[row, col]);
                        box[row, col].AddChild(texts[row, col]);

                    }
                    numWordSet++;

                }

            }

            #endregion

            this.AddChild(GameBoard, GameRound, 1001 + GameRound);
            #endregion
        }

        private void InitAnswerBoard()
        {

            LetterBoard.ContentSize = new CCSize(Screen.DeviceWidth, Screen.DeviceHeight / 9.25f);
            _boxLetter = new CCSprite[LetterLength];
            _textsLetter = new CCLabel[LetterLength];
            float spacer = 0, span = 0;
            int tag = 0;
            for (int i = 0; i < LetterLength; i++)
            {

                _boxLetter[i] = new CCSprite(ResourceManager.Instance.AnswerLetterContainer);
                _boxLetter[i].ContentSize = new CCSize(LetterBoard.ContentSize.Width / LetterLength, LetterBoard.ContentSize.Height - LetterBoard.ContentSize.Height / 2.5f);
                if (LetterLength < 5)
                { span = _boxLetter[i].BoundingBoxTransformedToWorld.Size.Width / 10; }




                if (i == 0)
                {
                    spacer += _boxLetter[i].ContentSize.Width / 2;
                    _textsLetter[i] = new CCLabel($"{GuessWord[i].ToString().ToUpper()}", ResourceManager.Instance.FaceOffM54,
                   _boxLetter[i].BoundingBoxTransformedToWorld.Size.Width - span, CCLabelFormat.SystemFont);
                }
                else
                {
                    spacer += _boxLetter[i].ContentSize.Width;
                    _textsLetter[i] = new CCLabel(string.Empty, ResourceManager.Instance.FaceOffM54,
                        _boxLetter[i].BoundingBoxTransformedToWorld.Size.Width - span, CCLabelFormat.SystemFont);
                }
                _boxLetter[i].Position = new CCPoint(spacer, LetterBoard.ContentSize.Height / 2);
                LetterBoard.AddChild(_boxLetter[i], 0, tag);
                _textsLetter[i].Position = new CCPoint(_boxLetter[i].ContentSize.Width / 2, _boxLetter[i].ContentSize.Height / 2);
                _boxLetter[i].AddChild(_textsLetter[i]);
                ListedSpriteLetter.Add(_boxLetter[i]);
                tag++;
            }

            this.AddChild(LetterBoard);
        }

        private void InitAnswerBank()
        {

            BankBoard.ContentSize = new CCSize(Screen.DeviceWidth, Screen.DeviceHeight / 6.25f);
            AnswerBank = new CCSprite[RandomWordsCountRow, RandomWordsCountCol];
            _bankText = new CCLabel[RandomWordsCountRow, RandomWordsCountCol];
            float spacer = 0;
            float dividerW = 10f;
            float dividerL = 2.25f;
            if (AppSettings.GameDifficultySelected == Difficulties.Easy || AppSettings.GameDifficultySelected == Difficulties.Challenging)
            {
                dividerW = 8f;
                if (AppSettings.GameDifficultySelected == Difficulties.Easy)
                    dividerL = 2f;
            }

            int tag = 0;

            for (int row = 0; row < RandomWordsCountRow; row++)
            {
                for (int col = 0; col < RandomWordsCountCol; col++)
                {
                    AnswerBank[row, col] = new CCSprite(ResourceManager.Instance.LetterBankContainer);
                    _bankText[row, col] = new CCLabel($"{WordService.RandomLetters}", ResourceManager.Instance.FaceOffM54, AnswerBank[row, col].BoundingBoxTransformedToWorld.Size.Width, CCLabelFormat.SystemFont);
                    _bankText[row, col].Color = CCColor3B.White;
                    AnswerBank[row, col].ContentSize = new CCSize(BankBoard.ContentSize.Width / dividerW, BankBoard.ContentSize.Height / dividerL);

                    if (AppSettings.GameDifficultySelected == Difficulties.Difficult || AppSettings.GameDifficultySelected == Difficulties.Challenging)
                    {
                        if (row == 1 && col == 0)
                            spacer = 0;

                        if ((row == 0 && col == 0) || (row == 1 && col == 0))
                        {
                            spacer += AnswerBank[row, col].ContentSize.Width / 1.3f;
                        }
                        else
                        {
                            if (AppSettings.GameDifficultySelected == Difficulties.Difficult)
                                spacer += AnswerBank[row, col].ContentSize.Width + AnswerBank[row, col].ContentSize.Width / 15;
                            else
                                spacer += AnswerBank[row, col].ContentSize.Width + AnswerBank[row, col].ContentSize.Width / 3.5f;


                        }

                        switch (row)
                        {
                            case 0:
                                AnswerBank[row, col].Position = new CCPoint(spacer, BankBoard.ContentSize.Height / 1.2f);
                                break;
                            case 1:
                                AnswerBank[row, col].Position = new CCPoint(spacer, AnswerBank[row - 1, col].PositionY - AnswerBank[row, col].ContentSize.Width - 12f);
                                break;
                        }
                        _bankText[row, col].Position = new CCPoint(AnswerBank[row, col].ContentSize.Width / 2, AnswerBank[row, col].ContentSize.Height / 2);
                        AnswerBank[row, col].AddChild(_bankText[row, col], 0, tag);
                        ListedSpritesBank.Add(AnswerBank[row, col]);
                        BankBoard.AddChild(AnswerBank[row, col]);

                    }
                    else
                    {
                        if (col == 0)
                            spacer = 0;

                        if (col == 0)
                        {
                            spacer += AnswerBank[row, col].ContentSize.Width / 1.3f;
                        }
                        else
                        {
                            if (AppSettings.GameDifficultySelected == Difficulties.Moderate)
                                spacer += AnswerBank[row, col].ContentSize.Width + AnswerBank[row, col].ContentSize.Width / 15;
                            else
                                spacer += AnswerBank[row, col].ContentSize.Width + AnswerBank[row, col].ContentSize.Width / 3.5f;


                        }




                        AnswerBank[row, col].Position = new CCPoint(spacer, BankBoard.ContentSize.Height / 2f);
                        _bankText[row, col].Position = new CCPoint(AnswerBank[row, col].ContentSize.Width / 2, AnswerBank[row, col].ContentSize.Height / 2);
                        AnswerBank[row, col].AddChild(_bankText[row, col], 0, tag);
                        ListedSpritesBank.Add(AnswerBank[row, col]);
                        BankBoard.AddChild(AnswerBank[row, col]);

                    }
                    tag++;

                }
            }


            this.AddChild(BankBoard);
        }
        #endregion

        #region Gameplay and Logic  


        private void InitGameBase()
        {

            ListedSpriteLetter = new List<CCSprite>();
            ListedSpritesBank = new List<CCSprite>();
        }
        private void GameInit()
        {

            InitGameBoard();
            InitAnswerBoard();
            InitAnswerBank();
            TouchListeners();

        }
        private void GameStartAtRound(int round)
        {
            WordBeingPlayed = WordService.WordProperties[round].primary_text.Split(' ');
            //  WordBeingPlayed = WordService.SampleWord.Split(' ');
            WordLength = WordService.WordProperties[round].number_of_words;
            Longest_Word_Length = (float)WordService.WordProperties[round].longest_word_length;
            UIService.CreateGameBoardBy(WordService.WordProperties[round]);
            WordService.SelectedWordToPlay(WordBeingPlayed);
            switch (AppSettings.GameDifficultySelected)
            {
                case Difficulties.Easy: RandomWordsCountRow = 1; RandomWordsCountCol = 6; break;
                case Difficulties.Moderate: RandomWordsCountRow = 1; RandomWordsCountCol = 9; break;
                case Difficulties.Challenging: RandomWordsCountRow = 2; RandomWordsCountCol = 6; break;
                case Difficulties.Difficult: RandomWordsCountRow = 2; RandomWordsCountCol = 9; break;
            }
        }

        private void TouchListeners()
        {

            var scaleDown = new CCEaseElastic(new CCScaleTo(BUTTON_MOVE_TIME, .90f), BUTTON_ELASTIC_TIME);
            var scaleUp = new CCEaseElastic(new CCScaleTo(BUTTON_MOVE_TIME, 1), BUTTON_ELASTIC_TIME);
            var listeners = new CCEventListenerTouchOneByOne();

            listeners.OnTouchBegan = (touch, _event) => {
                if (BankBoard.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {
                    foreach (var sprites in ListedSpritesBank)
                    {
                        if (sprites.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                        {
                            sprites.AddAction(scaleDown);

                        }
                    }
                }
                else if (LetterBoard.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {
                    foreach (var sprites in ListedSpriteLetter)
                    {
                        if (sprites.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                        {
                            sprites.AddAction(scaleDown);
                        }
                    }
                }
                return true;
            };
            listeners.OnTouchCancelled = (touch, _event) => {
                if (BankBoard.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {
                    foreach (var sprites in ListedSpritesBank)
                    {
                        if (sprites.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                        {
                            sprites.AddAction(scaleUp);
                        }
                    }
                }
                else if (LetterBoard.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {
                    foreach (var sprites in ListedSpriteLetter)
                    {
                        if (sprites.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                        {
                            sprites.AddAction(scaleUp);
                        }
                    }
                }
            };
            listeners.OnTouchEnded = (touch, _event) => {
                int tagBank = 0, tagLetter = 0;

                if (BankBoard.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {

                    foreach (var sprites in ListedSpritesBank)
                    {
                        if (sprites.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location) && sprites.Opacity != 0)
                        {

                            var label = sprites.GetChildByTag(tagBank) as CCLabel;
                            _textsLetter[GuessLetter].Text = label.Text;
                            _textsLetter[GuessLetter].Tag = tagBank;
                            sprites.Opacity = 0;
                            label.Opacity = 0;
                            foreach (var letterSprite in ListedSpriteLetter)
                            {
                                var letterLabel = letterSprite.Children.First() as CCLabel;
                                if (letterLabel.Text == string.Empty)
                                {
                                    GuessLetter = letterSprite.Tag;
                                    tagLetter = 0;
                                    break;
                                }
                                else
                                {
                                    tagLetter++;
                                }


                            }
                            if (tagLetter == LetterLength)
                            {
                                //   GameService.GameRound += 1;
                                WordService.GameStart();
                                SceneManagers.Instance.NavigateToScene(SceneManagers.SceneType.Game);

                            }




                        }
                        tagBank++;
                    }
                }
                else if (LetterBoard.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {
                    foreach (var sprites in ListedSpriteLetter)
                    {
                        if (sprites.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                        {
                            var label = sprites.Children.First() as CCLabel;

                            foreach (var bankSprites in ListedSpritesBank)
                            {
                                var bankLabel = bankSprites.Children.First() as CCLabel;
                                if (label.Tag == bankLabel.Tag)
                                {
                                    bankSprites.Opacity = 255;
                                    bankLabel.Opacity = 255;
                                    label.Text = string.Empty;
                                    GuessLetter = sprites.Tag;

                                }
                                else if (label.Text == string.Empty)
                                {
                                    GuessLetter = sprites.Tag;
                                }
                            }
                            //  GuessLetter = sprites.Tag;
                            sprites.AddAction(scaleUp);

                        }
                    }
                }
            };
            listeners.OnTouchMoved = (touch, _event) => {
                if (BankBoard.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {
                    foreach (var sprites in ListedSpritesBank)
                    {
                        if (sprites.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                        {
                            sprites.AddAction(scaleUp);
                        }
                    }
                }
                else if (LetterBoard.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {
                    foreach (var sprites in ListedSpriteLetter)
                    {
                        if (sprites.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                        {
                            sprites.AddAction(scaleUp);
                        }
                    }
                }
            };

            this.AddEventListener(listeners);
        }

        private void StartNewRound()
        {
            //  GameRound++;
            GuessLetter = 1;
            /*   if (GameBoard?.Parent != null)
                   GameBoard.RemoveFromParent();
               if (LetterBoard?.Parent != null)
                   LetterBoard.RemoveFromParent();
               if (BankBoard?.Parent != null)
                   BankBoard.RemoveFromParent();

               GameBoard.RemoveAllChildren();
               LetterBoard.RemoveAllChildren();
               BankBoard.RemoveAllChildren();
               GameBoard.Children.Clear();
               LetterBoard.Children.Clear();
               BankBoard.Children.Clear(); 
                  */
            // InitGameBase();
            InitGameBase();
            GameStartAtRound(GameRound);
            InitGameBoard();
            InitAnswerBoard();
            InitAnswerBank();
            TouchListeners();
            AddedToScene();
        }
        private void ClearAll()
        {
            /*  foreach (var sprites in ListedSpritesBank) {
                  sprites.RemoveAllChildren();
              }
              foreach (var sprites in ListedSpriteLetter) {
                  sprites.RemoveAllChildren();
              }

              this.RemoveChild(GameBoard);
              this.RemoveChild(LetterBoard);
              this.RemoveChild(BankBoard);
                 */

        }

        private void StartRound()
        {

        }
        #endregion
    }
}
