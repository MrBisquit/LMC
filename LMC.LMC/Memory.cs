namespace LMC.Internal
{
    public static class Memory
    {
        public static int MemSize { get { return memSize; } set { memSize = value; Prep(); } }
        private static int memSize = 100;
        public static int[] Mem { get; private set; }

        internal static Events.MemUpdate update = new();
        internal static int nextFree = 0;

        public static void Prep()
        {
            Mem = new int[MemSize];
            Events.Memory.OnMemResized();
        }

        public static bool Set(int addr, int val)
        {
            if (addr > MemSize || addr < 0) return false;
            else
            {
                int old = Mem[addr];
                Mem[addr] = val;
                Events.Memory.OnMemChanged(new(addr, old, val));
                if (addr == nextFree) nextFree++;
                return true;
            }
        }
        
        public static void Clear()
        {
            Mem = new int[MemSize];
            nextFree = 0;
            Events.Memory.OnMemCleared();
        }

        public static int GetNextFree()
        {
            return nextFree;
        }
    }
}