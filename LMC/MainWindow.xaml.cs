using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
    }
}