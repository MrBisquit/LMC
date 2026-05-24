using LMC.Internal.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMC.Internal
{
    public static class Run
    {
        public static bool IsRunning { get; internal set; } = false;

        public static int TotalInstructions { get; private set; }
        public static int TotalJumps { get; private set; }
        public static int TotalVariables { get; private set; }

        public static void StartRun(string instr)
        {
            IsRunning = true;

            Memory.Clear();
            Screen.Clear();

            // Load program
            (Instructions.Instruction[], (string, int?)[]) instrs = Parser.Parse(instr.Split("\n"));
            TotalInstructions = instrs.Item1.Length;
            TotalVariables = instrs.Item2.Length + Instructions.GenDefData().Length;
            Events.misc.OnStatsUpdated();

            Dictionary<Instructions.Instruction, int> instrsMemMap = new();
            Dictionary<string, int> varsMemMap = new();

            for(int i = 0; i < instrs.Item1.Length; i++)
            {
                //Memory.Set(Memory.GetNextFree(), int.Parse(instrs.Item1[i].ToMem()));

                // Reserve the memory, write variables, then write the instructions
                instrsMemMap[instrs.Item1[i]] = Memory.GetNextFree();
                Memory.Set(Memory.GetNextFree(), 0);
            }

            for(int i = 0; i < instrs.Item2.Length; i++)
            {
                //varsMemMap[instrs.Item2[i].Item1] = Memory.GetNextFree();
                //Memory.Set(Memory.GetNextFree(), instrs.Item2[i].Item2 ?? 0);

                Variables.Create(instrs.Item2[i].Item1);
            }

            for (int i = 0; i < instrs.Item1.Length; i++)
            {
                Memory.Set(instrsMemMap[instrs.Item1[i]], int.Parse(instrs.Item1[i].ToMem()));
            }

            // Create default variables
            Input.CreateVars();
            Input.PrepInput();

            // Begin program
        }

        public static void StopRun()
        {

        }

        public static void HaltError(string msg, int? line)
        {
            IsRunning = false;
            Events.errors.OnError(new(msg, line));
        }
    }
}