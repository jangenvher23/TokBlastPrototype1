using System;
using TokBlastPrototype1.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TokBlastPrototype1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        public const string AdAppIdAndroid = "ca-app-pub-8659477468618951~2757166332";
        public const string AdAppIdiOS = "ca-app-pub-8659477468618951~1575946608";

        public const string AdBannerAndroidId = "ca-app-pub-8659477468618951/8102413235";
        public const string AdBanneriOSId = "ca-app-pub-8659477468618951/2294842341";
        public const string InterstitialAndroidId = "ca-app-pub-8659477468618951/7706240689";
        public const string InterstitialiOSId = "ca-app-pub-8659477468618951/7575089182";
        public const string InterstitialAndroidRewarded = "ca-app-pub-8659477468618951/5087614225";
        public const string InterstitialiOSRewarded = "ca-app-pub-8659477468618951/3115701781";

        public static string BundleIdAndroid = "com.tokket.TokBlitz";
        public static string BundleIdiOS = "com.tokket.TokBlitz";
        public static int ScreenHeight { get; set; }
        public static int ScreenWidth { get; set; }

        public App()
        {
            InitializeComponent();
            WordService.GetQoutesAndSayings();
            var page = new GamePage();
            MainPage = page;
        }

        protected override void OnStart()
        {

            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static string BaseColor { get { return "#447D95"; } }
        public static string PrimaryColor { get { return "#504A30"; } }
        public static string PrimaryTextColor { get { return "#FFFFFF"; } }
        public static string SecondaryTextColor { get { return "#D7B966"; } }
        public static string PressedColor { get { return "#C8C8C8"; } }
        public static string DisableColor { get { return "#838383"; } }
        public static string CorrectTextColor { get { return "#2EC60B"; } }
        public static string WrongTextColor { get { return "#E5130C"; } }
    }
}
