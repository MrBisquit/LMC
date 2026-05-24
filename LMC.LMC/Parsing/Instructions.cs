using System;
using System.Collections.Generic;
using System.Text;

namespace LMC.Internal.Parsing
{
    public static class Instructions
    {
        public static KeyValuePair<string, string>[] Instrs =
        {
            new("ADD", "1"),
            new("SUB", "2"),
            new("LDA", "5"),
            new("STA", "3"),
            new("HLT", "000"),
            new("INP", "901"),
            new("OUT", "902"),
            new("BRA", "6"),
            new("BRZ", "7"),
            new("BRP", "8")
        };

        public static string[] GenDefData()
        {
            List<string> data = new();

            switch (Input.CurrentMode)
            {
                case Input.Mode.UDLR:
                    data.Add("iup\tDAT\t\t; Up");
                    data.Add("idn\tDAT\t\t; Down");
                    data.Add("ilf\tDAT\t\t; Left");
                    data.Add("irt\tDAT\t\t; Right");
                    break;
                case Input.Mode.None:
                default:
                    break;
            }

            for(int y = 0; y < Screen.Height; y++)
            {
                for(int x = 0; x < Screen.Width; x++)
                {
                    data.Add($"s{(Screen.Height * y) + x}\tDAT\t\t; X:{x} Y:{y}");
                }
            }

            return data.ToArray();
        }

        public partial class Instruction
        {
            public virtual string ToMem() { return ""; }
        }

        public static class Instructs
        {
            public class INP : Instruction
            {
                public override string ToMem()
                {
                    return "901";
                }
            }

            public class OUT : Instruction
            {
                public override string ToMem()
                {
                    return "902";
                }
            }

            public class ADD : Instruction
            {
                public string arg1;

                public ADD(string arg1)
                {
                    this.arg1 = arg1;
                }

                public override string ToMem()
                {
                    return $"1{Variables.GetMemFromVar(arg1)}";
                }
            }

            public class SUB : Instruction
            {
                public string arg1;

                public SUB(string arg1)
                {
                    this.arg1 = arg1;
                }

                public override string ToMem()
                {
                    return $"2{Variables.GetMemFromVar(arg1)}";
                }
            }

            public class LDA : Instruction
            {
                public string arg1;

                public LDA(string arg1)
                {
                    this.arg1 = arg1;
                }

                public override string ToMem()
                {
                    return $"5{Variables.GetMemFromVar(arg1)}";
                }
            }

            public class STA : Instruction
            {
                public string arg1;

                public STA(string arg1)
                {
                    this.arg1 = arg1;
                }

                public override string ToMem()
                {
                    return $"3{Variables.GetMemFromVar(arg1)}";
                }
            }
            public class BRA : Instruction
            {
                public string arg1;

                public BRA(string arg1)
                {
                    this.arg1 = arg1;
                }

                public override string ToMem()
                {
                    return $"6{Variables.GetMemFromVar(arg1)}";
                }
            }
            public class BRP : Instruction
            {
                public string arg1;

                public BRP(string arg1)
                {
                    this.arg1 = arg1;
                }

                public override string ToMem()
                {
                    return $"8{Variables.GetMemFromVar(arg1)}";
                }
            }
            public class BRZ : Instruction
            {
                public string arg1;

                public BRZ(string arg1)
                {
                    this.arg1 = arg1;
                }

                public override string ToMem()
                {
                    return $"7{Variables.GetMemFromVar(arg1)}";
                }
            }
            public class HLT : Instruction
            {
                public override string ToMem()
                {
                    return "000";
                }
            }
        }
    }
}