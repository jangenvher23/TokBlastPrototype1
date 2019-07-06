using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TokBlastPrototype1.Managers
{
    public class GameManager
    {
        private static readonly GameManager instance = new GameManager();
        public static GameManager Instance
        {
            get { return instance; }
        }

        public ContentPage Page { get; private set; }

        public void Ready(ContentPage page)
        {
            Page = page;
        }
    }
}
