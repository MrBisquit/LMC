using System;
using System.Collections.Generic;
using System.Text;

namespace LMC.LMC
{
    public static class Input
    {
        public enum Mode
        {
            UDLR
        };

        public static Mode CurrentMode { get; set { CurrentMode = value; PrepMemory(); } }

        public static KeyValuePair<ConsoleKey, int> MemKeyPair { get; set; } 

        public static void PrepMemory()
        {

        }

        public static void AcceptInput(ConsoleKey key)
        {

        }
    }
}