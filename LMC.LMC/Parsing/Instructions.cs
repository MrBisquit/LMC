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
    }
}