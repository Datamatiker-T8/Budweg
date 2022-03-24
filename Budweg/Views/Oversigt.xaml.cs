using Budweg.Domain;
using Budweg.ViewModels;
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

namespace Budweg
{
    /// <summary>
    /// Interaction logic for Oversigt.xaml
    /// </summary>
    public partial class Oversigt : Page
    {

        BrakeCaliberViewModel bcvm = new BrakeCaliberViewModel();

        public Oversigt()
        {
            InitializeComponent();
            DataContext = bcvm;
        }

        private void OverViewList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateWindow OC = new();
            OC.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BrakeCaliber value = (BrakeCaliber)Datagrid.SelectedValue;
            int breakid = value.BrakeCaliberId;

            bcvm.DeleteBrakeCaliber(breakid);
        }
    }
}
