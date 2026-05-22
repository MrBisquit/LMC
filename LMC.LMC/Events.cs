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

        public class InputUpdate
        {
            public class InputModeChangedArgs
            {
                public Input.Mode NewMode { get; private set; }

                public InputModeChangedArgs(Input.Mode NewMode)
                {
                    this.NewMode = NewMode;
                }
            }

            public event EventHandler<InputModeChangedArgs>? ModeChanged;

            public virtual void OnModeChanged(InputModeChangedArgs args)
            {
                ModeChanged?.Invoke(this, args);
                All.OnEventFired($"Input mode changed (M {args.NewMode})");
            }
        }

        public class ScreenUpdate
        {
            public class ScreenChangedArgs
            {
                public int X { get; private set; }
                public int Y { get; private set; }
                public int Val { get; private set; }

                public ScreenChangedArgs(int X, int Y, int Val)
                {
                    this.X = X;
                    this.Y = Y;
                    this.Val = Val;
                }
            }

            public event EventHandler? ScreenResized;
            public event EventHandler? ScreenCleared;
            public event EventHandler<ScreenChangedArgs>? ScreenChanged;

            public virtual void OnResized()
            {
                ScreenResized?.Invoke(this, EventArgs.Empty);
                All.OnEventFired($"Screen resized (W {Internal.Screen.Width} H {Internal.Screen.Height})");
            }

            public virtual void OnCleared()
            {
                ScreenCleared?.Invoke(this, EventArgs.Empty);
                All.OnEventFired("Screen cleared");
            }

            public virtual void OnChanged(ScreenChangedArgs args)
            {
                ScreenChanged?.Invoke(this, args);
                All.OnEventFired($"Screen changed (X {args.X} Y {args.Y} V {args.Val})");
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
        public static InputUpdate Input = new();
        public static ScreenUpdate Screen = new();
        public static AllUpdate All = new();
    }
}
