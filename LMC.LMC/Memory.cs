namespace LMC.Internal
{
    public static class Memory
    {
        public static int MemSize { get; set { MemSize = value; Prep(); }  }
        public static List<int> Mem { get; private set; } = new();

        internal static Events.MemUpdate update = new();

        public static void Prep()
        {
            Mem = new(MemSize);
            Events.Memory.OnMemResized();
        }

        public static bool Set(int addr, int val)
        {
            if (addr > MemSize) return false;
            else
            {
                int old = Mem[addr];
                Mem[addr] = val;
                Events.Memory.OnMemChanged(new(addr, old, val));
                return true;
            }
        }
        
        public static void Clear()
        {
            Mem = new(MemSize);
            Events.Memory.OnMemCleared();
        }
    }
}