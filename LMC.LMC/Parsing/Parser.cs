using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LMC.Internal.Parsing
{
    public static class Parser
    {
        public static (Instructions.Instruction?, (string, int?)?)? ParseLine(string input, int l)
        {
            List<string> instrs = new()
            {
                "ADD",
                "SUB",
                "LDA",
                "STA",
                "HLT",
                "INP",
                "OUT",
                "BRA",
                "BRZ",
                "BRP",

                "DAT"
            };

            string line = input.Trim().Replace("\t", " ");
            string[] split = line.Split(' ');
            string[] parts = new string[3];
            int part = 0;

            for(int i = 0; i < split.Length; i++)
            {
                if (part == 0)
                {
                    if(split[i] == "") continue;

                    if(instrs.Contains(split[i]))
                    {
                        parts[1] = split[i];
                        part = 2;
                    } else
                    {
                        parts[0] = split[i];
                    }
                } else if (part == 2)
                {
                    if (split[i] == ";") break;
                    if (split[i] == "") continue;

                    parts[2] = split[i];
                }
            }

            if (parts[1] == "DAT")
            {
                if (string.IsNullOrEmpty(parts[0]))
                {
                    Run.HaltError($"Failed to parse line {l + 1}: No name was provided", l);
                    return (null, null);
                } else
                {
                    if (string.IsNullOrEmpty(parts[2]))
                        return (null, (parts[0], null));
                    else
                    {
                        try
                        {
                            return (null, (parts[0], int.Parse(parts[2])));
                        } catch(Exception ex)
                        {
                            Run.HaltError($"Failed to parse line {l + 1}: Encountered and exception: " + ex.Message, l);
                            return (null, null);
                        }
                    }
                }
            } else
            {
                switch(parts[1])
                {
                    case "INP":
                        return (new Instructions.Instructs.INP(), null);
                    case "OUT":
                        return (new Instructions.Instructs.OUT(), null);
                    case "LDA":
                        return (new Instructions.Instructs.LDA(parts[2]), null);
                    case "STA":
                        return (new Instructions.Instructs.STA(parts[2]), null);
                    case "ADD":
                        return (new Instructions.Instructs.ADD(parts[2]), null);
                    case "SUB":
                        return (new Instructions.Instructs.SUB(parts[2]), null);
                    case "BRP":
                        return (new Instructions.Instructs.BRP(parts[2]), null);
                    case "BRZ":
                        return (new Instructions.Instructs.BRZ(parts[2]), null);
                    case "BRA":
                        return (new Instructions.Instructs.BRA(parts[2]), null);
                    case "HLT":
                        return (new Instructions.Instructs.HLT(), null);
                    default:
                        break;
                }
            }

            return null;
        }

        public static (Instructions.Instruction[], (string, int?)[]) Parse(string[] input)
        {
            List<Instructions.Instruction> insts = new();
            List<(string, int?)> vars = new();

            for(int i = 0; i < input.Length; i++)
            {
                (Instructions.Instruction?, (string, int?)?)? parsed = ParseLine(input[i], i);

                //if (parsed == null) return ([], []);
                if (parsed?.Item1 == null && parsed?.Item2 == null) continue;

                if(parsed?.Item1 != null)
                    insts.Add(parsed?.Item1);
                else if(parsed?.Item2 != null)
                    vars.Add((((string, int ?))parsed?.Item2));
            }

            return (insts.ToArray(), vars.ToArray());
        }
    }
}