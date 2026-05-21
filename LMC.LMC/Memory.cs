namespace LMC.LMC
{
    public static class Memory
    {
        public static int MemSize { get; set { MemSize = value; Prep(); }  }
        public static List<KeyValuePair<int, int>> Mem { get; private set; } = new();

        internal static Events.MemUpdate update = new();

        public static void Prep()
        {
            Mem = new(MemSize);
            Events.Memory.OnMemResized();
        }
    }
}
