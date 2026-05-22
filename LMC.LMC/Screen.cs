using System;
using System.Collections.Generic;
using System.Text;

namespace LMC.Internal
{
    public static class Screen
    {
        public static int Width { get { return width; } set { width = value; Prep(); } }
        static int width;
        public static int Height { get { return height; } set { height = value; Prep(); } }
        static int height;

        public static void Prep()
        {
            Events.Screen.OnResized();
        }
    }
}
