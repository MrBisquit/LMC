using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace LMC
{
    public static class Maps
    {
        public static ConsoleKey KeyToConsole(Key key)
        {
            switch(key)
            {
                case Key.Up:    return ConsoleKey.UpArrow;
                case Key.Down:  return ConsoleKey.DownArrow;
                case Key.Left:  return ConsoleKey.LeftArrow;
                case Key.Right: return ConsoleKey.RightArrow;

                /// @todo Complete this

                default:
                    return ConsoleKey.Escape;
            }
        }
    }
}
