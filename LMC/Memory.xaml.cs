using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LMC
{
    /// <summary>
    /// Interaction logic for Memory.xaml
    /// </summary>
    public partial class Memory : Window
    {
        public Memory()
        {
            InitializeComponent();

            Style = (Style)FindResource(typeof(Window));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Internal.Events.Memory.MemChanged += Memory_MemChanged;
            Internal.Events.Memory.MemResized += Memory_MemResized;
            Internal.Events.Memory.MemCleared += Memory_MemCleared;
        }

        int changed = -1;

        private void Memory_MemCleared(object? sender, EventArgs e)
        {
            Regen();
        }

        private void Memory_MemResized(object? sender, EventArgs e)
        {
            Regen();
        }

        private void Memory_MemChanged(object? sender, Internal.Events.MemUpdate.MemChangedArgs e)
        {
            changed = e.Addr;
            Regen();
        }

        public void Regen()
        {
            InnerGrid.Children.Clear();
            InnerGrid.ColumnDefinitions.Clear();
            InnerGrid.RowDefinitions.Clear();

            int w = 10;

            for (int i = 0; i < w; i++)
                InnerGrid.ColumnDefinitions.Add(new());

            for(int i = 0; i < Internal.Memory.MemSize / w + 1; i++)
                InnerGrid.RowDefinitions.Add(new());

            int c = 0;
            int r = 0;

            int p = (Internal.Memory.MemSize - 1).ToString().Length;

            for(int i = 0; i < Internal.Memory.MemSize; i++)
            {
                if (c == 10)
                {
                    c = 0;
                    r++;
                }

                Grid g = new Grid();
                g.Margin = new(2.5);
                Grid.SetColumn(g, c);
                Grid.SetRow(g, r);

                g.RowDefinitions.Add(new());
                g.RowDefinitions.Add(new());

                TextBlock mb = new();
                mb.Text = i.ToString().PadLeft(p, '0');
                mb.HorizontalAlignment = HorizontalAlignment.Center;
                g.Children.Add(mb);

                TextBlock v = new();
                v.Text = Internal.Memory.Mem[i].ToString();
                v.HorizontalAlignment = HorizontalAlignment.Center;
                v.FontSize = 17;
                Grid.SetRow(v, 1);
                g.Children.Add(v);

                InnerGrid.Children.Add(g);

                c++;
            }
        }
    }
}