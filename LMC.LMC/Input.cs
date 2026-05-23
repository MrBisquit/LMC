namespace LMC.Internal
{
    public static class Input
    {
        public enum Mode
        {
            None,
            UDLR
        };

        public static Mode CurrentMode { get { return currentMode; } set { currentMode = value; PrepInput(); } }
        static Mode currentMode;

        public static Dictionary<ConsoleKey, int> MemDict { get; set; } = new();

        public static void PrepInput()
        {
            Events.Input.OnModeChanged(new(currentMode));

            MemDict.Clear();
            switch (currentMode)
            {
                case Mode.UDLR:
                    MemDict[ConsoleKey.UpArrow]     = Parsing.Variables.GetMemFromVar("iup");
                    MemDict[ConsoleKey.DownArrow]   = Parsing.Variables.GetMemFromVar("idn");
                    MemDict[ConsoleKey.LeftArrow]   = Parsing.Variables.GetMemFromVar("ilf");
                    MemDict[ConsoleKey.RightArrow]  = Parsing.Variables.GetMemFromVar("irt"); 
                    break;
                case Mode.None:
                default:
                    break;
            }
        }

        public static void AcceptInput(ConsoleKey key, bool down)
        {
            if (!Run.IsRunning) return;

            if(MemDict.ContainsKey(key))
                Memory.Set(MemDict[key], down ? -1 : 0);

            /* Bob
            Screen.Set(0, 0, 1);
            Screen.Set(0, 1, 1);

            Screen.Set(4, 0, 1);
            Screen.Set(4, 1, 1);

            Screen.Set(0, 3, 1);
            Screen.Set(1, 4, 1);
            Screen.Set(2, 4, 1);
            Screen.Set(3, 4, 1);
            Screen.Set(4, 3, 1);*/
        }

        public static void CreateVars()
        {
            switch (currentMode)
            {
                case Mode.UDLR:
                    Parsing.Variables.Create("iup");
                    Parsing.Variables.Create("idn");
                    Parsing.Variables.Create("ilf");
                    Parsing.Variables.Create("irt");
                    break;
                case Mode.None:
                default:
                    break;
            }
        }
    }
}