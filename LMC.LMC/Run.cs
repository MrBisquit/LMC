using System;
using System.Collections.Generic;
using System.Text;

namespace LMC.Internal
{
    public static class Run
    {
        public static bool IsRunning { get; internal set; } = false;

        public static void StartRun(string instr)
        {
            IsRunning = true;

            Memory.Clear();
            Screen.Clear();

            // Load program

            // Create default variables
            Input.CreateVars();
            Input.PrepInput();

            // Begin program
        }

        public static void StopRun()
        {

        }
    }
}