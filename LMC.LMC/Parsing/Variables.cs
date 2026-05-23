using System;
using System.Collections.Generic;
using System.Text;

namespace LMC.Internal.Parsing
{
    public static class Variables
    {
        public static Dictionary<string, int> MemMap { get { return memMap; } }
        static Dictionary<string, int> memMap = new();
        static List<string> memList = new();

        public static int GetMemFromVar(string varName)
        {
            if (!memMap.ContainsKey(varName)) return -1;
            return memMap[varName];
        }

        public static void Clear()
        {
            for(int i = 0; i < memList.Count; i++)
            {
                Memory.Set(memMap[memList[i]], 0);
            }

            memMap.Clear();
            memList.Clear();
        }

        public static void Create(string varName)
        {
            int nextFree = Memory.GetNextFree();
            memMap[varName] = nextFree;
            Memory.Set(nextFree, 0);
        }
    }
}
