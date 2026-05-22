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
    }
}