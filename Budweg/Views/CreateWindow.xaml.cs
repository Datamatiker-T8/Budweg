using Budweg.ViewModels;
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
using System.Windows.Shapes;

namespace Budweg.Views
{
    /// <summary>
    /// Interaction logic for CreateWindow.xaml
    /// </summary>
    public partial class CreateWindow : Window
    {
        BrakeCaliberViewModel vm;
        public CreateWindow()
        {
            vm = new();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string budwegNo = BudwegNO.Text;
            string linkQRCode = QRLINK.Text;
            string caliberName = Name.Text;
            bool stockStatus = (bool)StockStatus.IsChecked;
            string brakeSystem = BrakeSystem.Text;
            byte[] qR_Bytes = vm.QRCodeToBytes(linkQRCode);
            vm.AddBrakeCaliber(caliberName, budwegNo, stockStatus, brakeSystem, linkQRCode, qR_Bytes);
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
