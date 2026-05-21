using System.Windows;
using LMC;

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
    }
}