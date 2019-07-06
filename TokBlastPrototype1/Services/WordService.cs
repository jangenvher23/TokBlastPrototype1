using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using TokBlastPrototype1.Models.Local;
using TokBlastPrototype1.Models.Enums;
using TokBlastPrototype1.Utilities;
using Newtonsoft.Json;
using System.Linq;
using TokBlastPrototype1.Views.Layers;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace TokBlastPrototype1.Services
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class WordService
    {

        static List<string> BanWords = new List<string>() { "this", "that", "the", "then", "of", "i", "for", "and", "i'll", "are", "you", "it's" };
        static string[] splitter = { " ", "/", "...", "-", ";", ":" };
        static char[] trimmer = new char[] { ' ', '!', ',', '.', '/', '?', '"', ':', ';' };

        public static List<WordProperties> WordProperties { get; private set; }
        public static List<WordProperties> WordPropertiesEasy { get; private set; }
        public static List<WordProperties> WordPropertiesModerate { get; private set; }
        public static List<WordProperties> WordPropertiesHard { get; private set; }
        public static List<WordProperties> SelectedWordProperties { get; private set; }
        public static string SampleWord { get; private set; }

        private static List<WordProperties> ListOfWords;
        private static List<WordProperties> DuplicateWords = new List<WordProperties>();
        private static string GetQuotesAndSayings()
        {

            var result = string.Empty;
            string fileName = "TokBlastPrototype1.Data.quotesandsayings.json";
            result = GetResourceStringFromFile(fileName);
            return result;

        }
        private static string GetResourceStringFromFile(string fileName)
        {
            var result = string.Empty;
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            using (Stream stream = assembly.GetManifestResourceStream(fileName))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        public static void GetQoutesAndSayings()
        {
            ListOfWords = JsonConvert.DeserializeObject<List<WordProperties>>(GetQuotesAndSayings());
            WordPropertiesModerate = new List<WordProperties>();
            WordPropertiesHard = new List<WordProperties>();
            WordPropertiesEasy = new List<WordProperties>();
            foreach (var item in ListOfWords)
            {
                if (item.number_of_words >= 21 && item.number_of_words <= 28 && item.id > 0)
                {
                    WordPropertiesHard.Add(item);
                }

                if (item.number_of_words >= 13 && item.number_of_words <= 20 && item.id > 0)
                {
                    WordPropertiesModerate.Add(item);
                }
                if (item.number_of_words >= 3 && item.number_of_words <= 12 && item.id > 0)
                {
                    WordPropertiesEasy.Add(item);
                }

            }


        }

        private static void InitiateWordSelectionVariety()
        {
            int minlet = 0, maxlet = 0;
            WordProperties = new List<WordProperties>();
            switch (AppSettings.GameDifficultySelected)
            {
                case Difficulties.Easy: minlet = 3; maxlet = 4; SelectedWordProperties = WordPropertiesEasy; break;
                case Difficulties.Moderate: minlet = 5; maxlet = 6; SelectedWordProperties = WordPropertiesModerate; break;
                case Difficulties.Difficult: minlet = 7; maxlet = 9; SelectedWordProperties = WordPropertiesHard; break;
                case Difficulties.Challenging: minlet = 10; maxlet = 12; SelectedWordProperties = WordPropertiesHard; break;
                case Difficulties.Default: minlet = 3; maxlet = 12; SelectedWordProperties = ListOfWords; break;
            }


            Random rand = new Random();
            int random = 0;

            while (WordProperties.Count != 5)
            {

                random = rand.Next(0, ListOfWords.Count);

                var id = ListOfWords.Where(item => item.id == ListOfWords[random].id).Select(item => item.id).FirstOrDefault();
                var Value = ListOfWords.Where(item => item.id == id).Select(item => item).FirstOrDefault();
                if (Value.longest_word_length >= minlet && Value.longest_word_length <= maxlet && !DuplicateWords.Contains(Value))
                {
                    WordProperties.Add(Value);
                    DuplicateWords.Add(Value);

                }


            }

            SampleWord = ListOfWords.Where(item => item.id == -3).Select(word => word.primary_trimmed).FirstOrDefault();
            //SampleWord = ListOfWords.Where(item => item ==ListOfWords[]).Select(word => word.primary_trimmed).FirstOrDefault();
        }

        public static string SelectedWordToPlay(string[] words)
        {
            var selected = string.Empty;
            var Sentence = words.ToList();
            int minlet = 0, maxlet = 0;
            switch (AppSettings.GameDifficultySelected)
            {
                case Difficulties.Easy: minlet = 3; maxlet = 4; break;
                case Difficulties.Moderate: minlet = 5; maxlet = 6; break;
                case Difficulties.Challenging: minlet = 7; maxlet = 9; break;
                case Difficulties.Difficult: minlet = 10; maxlet = 12; break;
                case Difficulties.Default: minlet = 3; maxlet = 12; break;
            }
            foreach (var word in Sentence)
            {

                // && word.CheckIfBan() == falseg 
                if (word.Length >= minlet && word.Length <= maxlet)
                {
                    selected = word;
                    GameLayer.LetterLength = word.Length;
                    GameLayer.GuessWord = word.ToCharArray();
                    break;
                }
            }


            return selected;
        }

        public static void GameStart()
        {

            switch (AppSettings.GameModeSelected)
            {
                case GameMode.Default: break;
                case GameMode.Variety: InitiateWordSelectionVariety(); break;
                case GameMode.Category: break;
                case GameMode.Multiplayer: break;
                case GameMode.SaveGame: break;
            }

        }

        static Random randforletters = new Random();
        public static char RandomLetters
        {
            get
            {
                int randit = randforletters.Next(0, 26);
                char letter = (char)('A' + randit);

                return letter;
            }
        }
    }
}
