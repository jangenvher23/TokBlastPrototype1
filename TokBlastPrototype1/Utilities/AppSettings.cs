using TokBlastPrototype1.Models.Enums;
using Xamarin.Forms;
using static TokBlastPrototype1.Managers.SceneManagers;
namespace TokBlastPrototype1.Utilities
{
    public class AppSettings
    {
        /*  public static bool IsOnline
          {
              get
              {
                  var connectionChecker = DependencyService.Get<IConnectionChecker>();
                  connectionChecker.CheckNetworkConnection();
                  return connectionChecker.IsConnected;
              }
          } */

        public static Difficulties GameDifficultySelected { get; set; }

        public static GameMode GameModeSelected { get; set; }

        public static PlayerType PlayerTypeSelected { get; set; }

        public static string CategorySelected { get; set; }
        public static ScreenResolution ScreenResolution { get; set; }

        public static SceneType CurrentSceneType { get; set; }

    }
}
