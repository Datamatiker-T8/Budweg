using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using Budweg.ViewModels;

namespace Budweg
{
    /// <summary>
    /// Interaction logic for KundeSupport.xaml
    /// </summary>
    public partial class KundeSupport : Page
    {
        CustomerSupportViewModel csvm = new CustomerSupportViewModel();

        public KundeSupport()
        {
            InitializeComponent();
            FillComboBox();
            DataContext = csvm;
        }

        private void FillComboBox()
        {
            myComboBox.ItemsSource = csvm.BrakeList;
            myComboBox.DisplayMemberPath = "BudwegNo";
        }

    }
}
