using System.IO;
using System.Text;
using System.Windows;
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

            edRT = new System.Timers.Timer(2500);
            edRT.Elapsed += EdRT_Elapsed;
            edRT.AutoReset = false;
        }

        private void EdRT_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                EventDisplay.Text = "";
            });
        }

        Memory memory = new();
        Screen screen = new();
        Help help = new();
        System.Timers.Timer edRT;

        public static string? SavePath = null;

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            memory.Close();
            screen.Close();
            help.Close();

            Environment.Exit(0);
        }

        private void UpdateDefaultCode()
        {
            ReadOnlyCode.Text = string.Join('\n', Internal.Parsing.Instructions.GenDefData());
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
            screen.Show();
            Internal.Run.StartRun(Code.Text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Internal.Events.All.EventFired += EventFired;

            UpdateDefaultCode();

            string[] def =
            {
                "\tINP",
                "\tSTA\tnum1",
                "\tINP",
                "\tADD\tnum1",
                "\tOUT",
                "\tHLT",
                "",
                "num1\tDAT"
            };

            Code.Text = string.Join('\n', def);
            CodeUpdateLN();

            screen.AutoSize();
        }

        public void EventFired(object? sender, string text)
        {
            EventDisplay.Text = text;
            edRT.Stop();
            edRT.Start();
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

        private void ClockSpeed_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void MemorySize_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                int mem = int.Parse(MemorySize.Text);

                if (mem >= 100 && mem <= 1048576)
                {
                    Internal.Memory.MemSize = mem;
                }
            } catch { }
        }

        private void ScreenWidth_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                int w = int.Parse(ScreenWidth.Text);

                if(w <= 100)
                {
                    Internal.Screen.Width = w;

                    UpdateDefaultCode();
                }
            } catch { }
        }

        private void ScreenHeight_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                int h = int.Parse(ScreenHeight.Text);

                if (h <= 100)
                {
                    Internal.Screen.Height = h;

                    UpdateDefaultCode();
                }
            }
            catch { }
        }

        private void InputVariables_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            switch(InputVariables.SelectedIndex)
            {
                case 0:
                    Internal.Input.CurrentMode = Internal.Input.Mode.None;
                    break;
                case 1:
                    Internal.Input.CurrentMode = Internal.Input.Mode.UDLR;
                    break;
            }

            UpdateDefaultCode();
        }

        private void CodeScroll_ScrollChanged(object sender, System.Windows.Controls.ScrollChangedEventArgs e)
        {
            CodeLNScroll.ScrollToVerticalOffset(e.VerticalOffset);
        }

        private void CodeUpdateLN()
        {
            int lc = Code.LineCount;
            StringBuilder sb = new();

            for(int i = 1; i <= lc; i++)
            {
                sb.AppendLine(i.ToString());
            }

            CodeLN.Text = sb.ToString();
        }

        private void Code_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CodeUpdateLN();
        }
    }
}