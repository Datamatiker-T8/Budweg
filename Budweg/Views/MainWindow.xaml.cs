using Budweg.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Budweg
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Overviewbt_Click(object sender, RoutedEventArgs e)
        {
            Oversigt os = new Oversigt();
            MainFrameWindow.Navigate(os);
            logo.Visibility = Visibility.Hidden;
        }

        private void Infobt_Click(object sender, RoutedEventArgs e)
        {
            Information im = new Information();
            MainFrameWindow.Navigate(im);
            logo.Visibility = Visibility.Hidden;
        }

        private void kundesupportbt_Click(object sender, RoutedEventArgs e)
        {
            KundeSupport kt = new KundeSupport();
            MainFrameWindow.Navigate(kt);
            logo.Visibility = Visibility.Hidden;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
