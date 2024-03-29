﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Budweg.Domain;
using Budweg.ViewModels;

namespace Budweg
{
    /// <summary>
    /// Interaction logic for KundeSupport.xaml
    /// </summary>
    public partial class KundeSupport : Page, INotifyPropertyChanged
    {
        BrakeCaliberViewModel bcvm = new BrakeCaliberViewModel();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string path)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(path));
        }

        private DrawingImage imageAsObject;
        public DrawingImage ImageAsObject
        {
            set
            {
                imageAsObject = value;
                NotifyPropertyChanged(nameof(ImageAsObject));
            }
            get { return imageAsObject; }
        }

        public KundeSupport()
        {
            InitializeComponent();
            FillComboBox();
            DataContext = this;
            QRcodeImage();
        }

        private void FillComboBox()
        {
            myComboBox.ItemsSource = bcvm.brakeCaliberVM;
            myComboBox.DisplayMemberPath = "BudwegNo";
        }
        public System.Windows.Media.DrawingImage QRcodeImage()
        {
            if (myComboBox.SelectedValue != null)
            {
                BrakeCaliber value = (BrakeCaliber)myComboBox.SelectedValue;
                int Breakid = value.BrakeCaliberId;

                for (int i = 0; i < bcvm.brakeCaliberVM.Count; i++)
                {
                    if (bcvm.brakeCaliberVM[i].BrakeCaliberId == Breakid)
                    {
                        int id = bcvm.brakeCaliberVM[i].BrakeCaliberId;
                        return bcvm.QRCodeFromBytes(id);
                    }
                }
            }
            return null;

        }

        private void myComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DrawingImage myImage = new DrawingImage();
            ImageAsObject = QRcodeImage();
        }

    }
}
