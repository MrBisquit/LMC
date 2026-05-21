using System;
using System.Collections.Generic;
using System.Text;

namespace LMC.Internal
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
            public event EventHandler? MemCleared;

            public virtual void OnMemChanged(MemChangedArgs args)
            {
                MemChanged?.Invoke(this, args);
                All.OnEventFired($"Memory changed (A {args.Addr} F {args.From} T {args.To})");
            }

            public virtual void OnMemResized()
            {
                MemResized?.Invoke(this, EventArgs.Empty);
                All.OnEventFired($"Memory resized (S {Internal.Memory.MemSize})");
            }

            public virtual void OnMemCleared()
            {
                MemCleared?.Invoke(this, EventArgs.Empty);
                All.OnEventFired("Memory cleared");
            }
        }

        public class AllUpdate
        {
            public event EventHandler<string>? EventFired;

            public virtual void OnEventFired(string text)
            {
                EventFired?.Invoke(this, text);
            }
        }

        public static MemUpdate Memory = new();
        public static AllUpdate All = new();
    }
}
