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
    /// Interaction logic for Screen.xaml
    /// </summary>
    public partial class Screen : Window
    {
        public Screen()
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
            Internal.Events.Screen.ScreenResized += Screen_ScreenResized;
            Internal.Events.Screen.ScreenCleared += Screen_ScreenCleared;
            Internal.Events.Screen.ScreenChanged += Screen_ScreenChanged;
        }

        private void Screen_ScreenChanged(object? sender, Internal.Events.ScreenUpdate.ScreenChangedArgs e)
        {
            Regen();
        }

        private void Screen_ScreenCleared(object? sender, EventArgs e)
        {
            Regen();
        }

        private void Screen_ScreenResized(object? sender, EventArgs e)
        {
            AutoSize();
            Regen();
        }

        public void AutoSize()
        {
            Width = (25 * Internal.Screen.Width) + 10 + (5 * Internal.Screen.Width);
            Height = (25 * Internal.Screen.Height) + 10 + (5 * Internal.Screen.Height) + 10;
        }

        public void Regen()
        {
            InnerGrid.Children.Clear();
            InnerGrid.ColumnDefinitions.Clear();
            InnerGrid.RowDefinitions.Clear();

            for(int i = 0; i < Internal.Screen.Width; i++)
                InnerGrid.ColumnDefinitions.Add(new());

            for (int i = 0; i < Internal.Screen.Height; i++)
                InnerGrid.RowDefinitions.Add(new());

            for(int y = 0; y < Internal.Screen.Height; y++)
            {
                for(int x = 0; x < Internal.Screen.Width; x++)
                {
                    Rectangle rect = new();
                    Grid.SetColumn(rect, x);
                    Grid.SetRow(rect, y);

                    rect.Width = 25;
                    rect.Height = 25;
                    rect.Margin = new(2.5);

                    switch(Internal.Screen.Board[y, x])
                    {
                        case 1:
                            rect.Fill = new SolidColorBrush(Colors.White);
                            break;
                        case 2:
                            rect.Fill = new SolidColorBrush(Colors.Red);
                            break;
                        case 3:
                            rect.Fill = new SolidColorBrush(Colors.Green);
                            break;
                        case 4:
                            rect.Fill = new SolidColorBrush(Colors.Blue);
                            break;
                        case 0:
                        default:
                            rect.Fill = new SolidColorBrush(Colors.Black);
                            break;
                    }

                    InnerGrid.Children.Add(rect);
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Internal.Input.AcceptInput(Maps.KeyToConsole(e.Key), true);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            Internal.Input.AcceptInput(Maps.KeyToConsole(e.Key), false);
        }
    }
}