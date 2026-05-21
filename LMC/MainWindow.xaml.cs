using System.IO;
using System.Windows;
using LMC;
using Microsoft.Win32;

namespace LMC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Style = (Style)FindResource(typeof(Window));
        }

        Memory memory = new();
        Screen screen = new();
        Help help = new();

        public static string? SavePath = null;

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            memory.Close();
            screen.Close();
            help.Close();
        }

        private void ViewMemory_Click(object sender, RoutedEventArgs e)
        {
            memory.Show();
        }

        private void ViewScreen_Click(object sender, RoutedEventArgs e)
        {
            screen.Show();
        }

        private void ViewHelp_Click(object sender, RoutedEventArgs e)
        {
            help.Show();
        }

        private void RunBtn_Click(object sender, RoutedEventArgs e)
        {
            memory.Show();
            screen.Show();
            Internal.Run.StartRun(Code.Text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Internal.Events.All.EventFired += EventFired;
        }

        public void EventFired(object? sender, string text)
        {
            EventDisplay.Text = text;
        }

        private void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new();
            if(ofd.ShowDialog() == true)
            {
                Code.Text = File.ReadAllText(ofd.FileName);
                SavePath = ofd.FileName;
                SaveBtn.IsEnabled = true;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SavePath == null) return;
            else
            {
                File.WriteAllText(SavePath, Code.Text);
            }
        }

        private void SaveAsBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new();
            if(sfd.ShowDialog() == true)
            {
                File.WriteAllText(sfd.FileName, Code.Text);
                SavePath = sfd.FileName;
                SaveBtn.IsEnabled = true;
            }
        }
    }
}