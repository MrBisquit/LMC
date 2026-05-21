using System;
using System.Collections.Generic;
using System.Text;

namespace LMC.LMC
{
    public static class Events
    {
        public class MemUpdate
        {
            public class MemChangedArgs
            {
                public int Addr { get; private set; }
                public int From { get; private set; }
                public int To   { get; private set; }

                public MemChangedArgs(int Addr, int From, int To)
                {
                    this.Addr = Addr;
                    this.From = From;
                    this.To = To;
                }
            }

            public event EventHandler<MemChangedArgs>? MemChanged;
            public event EventHandler? MemResized;

            public virtual void OnMemChanged(MemChangedArgs args)
            {
                MemChanged?.Invoke(this, args);
            }

            public virtual void OnMemResized()
            {
                MemResized?.Invoke(this, EventArgs.Empty);
            }
        }

        public static MemUpdate Memory = new();
    }
}
