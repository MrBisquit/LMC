using System;
using System.Collections.Generic;
using System.Text;

namespace LMC.Internal
{
    public static class Input
    {
        public enum Mode
        {
            None,
            UDLR
        };

        public static Mode CurrentMode { get { return currentMode; } set { currentMode = value; PrepInput(); } }
        static Mode currentMode;

        public static List<KeyValuePair<ConsoleKey, int>> MemKeyPairs { get; set; } = new();

        public static void PrepInput()
        {
            Events.Input.OnModeChanged(new(currentMode));
        }

        public static void AcceptInput(ConsoleKey key)
        {

        }
    }
}