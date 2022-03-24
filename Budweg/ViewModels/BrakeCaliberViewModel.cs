using System;
using Budweg.Domain;
using Budweg.Persistence;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using QRCoder;
using QRCoder.Xaml;

namespace Budweg.ViewModels
{
    public class BrakeCaliberViewModel
    {

        // ======================================================
        // Fields & Props
        // ======================================================

        BrakecaliberRepository brakeRepo = new BrakecaliberRepository();

        // This code:

        public List<BrakeCaliber> brakeCaliberList = new List<BrakeCaliber>();

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged("Name"); }
        }

        private BrakeCaliber selectedBrake;
        public BrakeCaliber SelectedBrake
        {
            get { return selectedBrake; }
            set { selectedBrake = value; NotifyPropertyChanged("SelectedBrake"); Name = SelectedBrake.CaliberName; }
        }

        // Can be:
        //public ObservableCollection<BrakeCaliber> brakeCaliberList = new ObservableCollection<BrakeCaliber>();


        // ======================================================
        // Constructor
        // ======================================================

        public BrakeCaliberViewModel()
        {
            foreach (BrakeCaliber brake in brakeRepo.GetAll())
            {
                brakeCaliberList.Add(brake);
            }

            //selectedBrake = brakeCaliberList[0];
        }

        // ======================================================
        // CRUD: Create
        // ======================================================

        public void AddBrakeCaliber(int brakeCaliberId, string caliberName, string budwegNo, bool stockStatus, string brakeSystem, string linkQRCode, byte[] qR_Bytes)
        {
            BrakeCaliber brakeCaliber = new(brakeCaliberId, caliberName, budwegNo, stockStatus, brakeSystem, linkQRCode, qR_Bytes);
            int addedCaliber = brakeRepo.Add(brakeCaliber);
            brakeCaliberList.Add(brakeRepo.GetById(addedCaliber));
        }
        public void AddBrakeCaliber(string caliberName, string budwegNo, bool stockStatus, string brakeSystem, string linkQRCode, byte[] qR_Bytes)
        {
            BrakeCaliber brakeCaliber = new(caliberName, budwegNo, stockStatus, brakeSystem, linkQRCode, qR_Bytes);
            int addedCaliber = brakeRepo.Add(brakeCaliber);
            brakeCaliberList.Add(brakeRepo.GetById(addedCaliber));
        }

        // ======================================================
        // CRUD: Read
        // ======================================================

        public BrakeCaliber GetBrakeCaliber(int id)
        {
            BrakeCaliber brakeCaliberResult = null;
            foreach (BrakeCaliber brake in brakeCaliberList)
            {
                if (id == brake.BrakeCaliberId)
                {
                    brakeCaliberResult = brake;
                }
            }
            return brakeCaliberResult;
        }

        public List<BrakeCaliber> GetAll()
        {
            return brakeCaliberList;
        }

    // ======================================================
    // CRUD: Update
    // ======================================================

        public void UpdateBrakeCaliber(int id, string modelNumber, string linkQRCode)
        {
            foreach (BrakeCaliber brake in brakeCaliberList)
            {
                if (id == brake.BrakeCaliberId)
                {
                    brake.BudwegNo = modelNumber;
                    brake.LinkQRCode = linkQRCode;
                }
            }
        }

    // ======================================================
    // CRUD: Delete
    // ======================================================

        public void DeleteBrakeCaliber(int id)
        {
            foreach (BrakeCaliber brake in brakeCaliberList)
            {
                if (id == brake.BrakeCaliberId)
                {
                    brakeCaliberList.Remove(brake);
                    brakeRepo.Remove(brake);
                }
            }
        }

        // ======================================================
        // Generation of QR-Code
        // ======================================================

        public byte[] QRCodeToBytes(string link)
        {
            byte[] pic = BrakeCaliber.GenerateQRCode(link);
            
            return pic;
        }

        public System.Windows.Media.DrawingImage QRCodeFromBytes(int id)
        {
            System.Windows.Media.DrawingImage qrCodeAsXaml = null;
            id = id - 1;
            if (brakeCaliberList[id].QR_Bytes != null)
            {
                QRCodeData qrCodeData = new QRCodeData(brakeCaliberList[id].QR_Bytes, QRCodeData.Compression.Uncompressed); // de-compresser.
                XamlQRCode qrCode = new XamlQRCode(qrCodeData);
                //QRCode qrCode = new QRCode(qrCodeData); // create qr-code from the data, all in memory
                qrCodeAsXaml = qrCode.GetGraphic(20); // draws img
            }

            return qrCodeAsXaml;
        }

    }
}
