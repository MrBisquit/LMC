using System;
using System.Collections.Generic;
using System.Text;

namespace LMC.Internal
{
    public static class Input
    {
        public enum Mode
        {
            UDLR
        };

        public static Mode CurrentMode { get; set { CurrentMode = value; PrepMemory(); } }

        public static List<KeyValuePair<ConsoleKey, int>> MemKeyPairs { get; set; } = new();

        public static void PrepMemory()
        {

        }

        public static void AcceptInput(ConsoleKey key)
        {

        }
    }
}