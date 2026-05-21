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

            Internal.Events.Memory.MemChanged += Memory_MemChanged;
            Internal.Events.Memory.MemResized += Memory_MemResized;
            Internal.Events.Memory.MemCleared += Memory_MemCleared;
        }

        private void Memory_MemCleared(object? sender, EventArgs e)
        {
            Update();
        }

        private void Memory_MemResized(object? sender, EventArgs e)
        {
            Update();
        }

        private void Memory_MemChanged(object? sender, Internal.Events.MemUpdate.MemChangedArgs e)
        {
            Update();
        }

        private void Update()
        {

        }
    }
}
