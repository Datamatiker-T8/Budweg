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
        DispatcherTimer timer;

        double panelWidth;
        bool hidden;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 8);
            timer.Tick += Timer_Tick;

            panelWidth = sidePanel.Width;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            double Timepanelspeed = 1.5;
            
            if (hidden)
            {
                sidePanel.Width += Timepanelspeed;
                if (sidePanel.Width >= panelWidth)
                {
                    timer.Stop();
                    hidden = false;
                }
            }
            else
            {
                sidePanel.Width -= Timepanelspeed;
                if (sidePanel.Width <= 50)
                {
                    timer.Stop();
                    hidden = true;
                }
            }
        }
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void PanelHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void KundeSupport_Selected(object sender, RoutedEventArgs e)
        {
            this.MainFrameWindows.Navigate(new KundeSupport());
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            this.MainFrameWindows.Navigate(new Oversigt());
        }

        private void ListViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            this.MainFrameWindows.Navigate(new Information());
        }

        private void Overviewbt_Click(object sender, RoutedEventArgs e)
        {
            Oversigt os = new Oversigt();
            MainFrameWindows.Navigate(os);
        }

        private void Infobt_Click(object sender, RoutedEventArgs e)
        {
            Information im = new Information();
            MainFrameWindows.Navigate(im);
        }


        private void kundesupportbt_Click(object sender, RoutedEventArgs e)
        {
            KundeSupport kt = new KundeSupport();
            MainFrameWindows.Navigate(kt);
        }
    }
}
