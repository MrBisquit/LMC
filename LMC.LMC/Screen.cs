using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LMC.Internal
{
    public static class Screen
    {
        public static int Width { get { return width; } set { width = value; Prep(); } }
        static int width = 5;
        public static int Height { get { return height; } set { height = value; Prep(); } }
        static int height = 5;

        public static int[,] Board { get { return board; } }
        static int[,] board = new int[0, 0];

        public static void Set(int x, int y, int v)
        {
            if (x > width || y > height) return;
            board[y, x] = v;
            Events.Screen.OnChanged(new(x, y, v));
        }

        /*private static int[,] InConv(List<List<int>> v)
        {
            if (v.Count == 0) return new int[0, 0];
            int[,] d = new int[v.Count, v[0].Count];
            for(int y = 0; y < v.Count; y++)
                for(int x = 0; x < v[0].Count; x++)
                    d[y, x] = v[y][x];
            return d;
        }*/

        public static void Prep()
        {
            Events.Screen.OnResized();

            board = new int[width, height];

            Debug.WriteLine($"{width} {height}");

            /*for(int y = 0; y < height; y++)
                for(int x = 0; x < width; x++)
                    board[y, x] = 0;*/
        }

        public static void Clear()
        {
            Events.Screen.OnCleared();

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    board[y, x] = 0;
        }
    }
}