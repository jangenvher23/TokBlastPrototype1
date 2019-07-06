using System;
using System.Collections.Generic;
using System.Text;
using CocosSharp;
namespace TokBlastPrototype1.Utilities
{
    public class Screen
    {    //===================================================================
        // Constants
        //===================================================================

        //===================================================================
        // Fields
        //===================================================================

        //===================================================================
        // Properties
        //===================================================================

        // The view port of the graphics device
        public static CCGameView GameView
        {
            get;
            set;
        }

        // The width that the game is designed for
        public static float GameWidth
        {
            get;
            set;
        }

        // The height that the game is designed for
        public static float GameHeight
        {
            get;
            set;
        }

        // The rectangle bounds the game is designed for
        public static CCRect GameBounds
        {
            get { return new CCRect(0, 0, (int)GameWidth, (int)GameHeight); }
        }

        // The width of the current device
        public static float DeviceWidth
        {
            get { return App.ScreenWidth; }
        }

        // The height of the current device
        public static float DeviceHeight
        {
            get { return App.ScreenHeight; }
        }

        // The rectangle bounds of the device
        public static CCRect DeviceBounds
        {
            get { return new CCRect(0, 0, DeviceWidth, DeviceHeight); }
        }

        // The aspect ratio of the device screen
        public static CCPoint Center
        {
            get { return new CCPoint(DeviceBounds.Center.X, DeviceBounds.Center.Y); }
        }

        //===================================================================
        // Constructors
        //===================================================================

        //===================================================================
        // Base Class/Interface Methods
        //===================================================================  

        //===================================================================
        // Methods
        //===================================================================

    }
}
