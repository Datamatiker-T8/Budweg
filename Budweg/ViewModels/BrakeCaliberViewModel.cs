using System;
using Budweg.Domain;
using Budweg.Persistence;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Budweg.ViewModels
{
    public class BrakeCaliberViewModel : INotifyPropertyChanged
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
        // public ObservableCollection<BrakeCaliber> BrakeCalibers = new ObservableCollection<BrakeCaliber>();
        

    // ======================================================
    // Constructor
    // ======================================================

        public BrakeCaliberViewModel()
        {
            foreach (BrakeCaliber brake in brakeRepo.GetAll())
            {
                brakeCaliberList.Add(brake);
            }

            selectedBrake = brakeCaliberList[0];
        }

    // ======================================================
    // CRUD: Create
    // ======================================================

        public void AddBrakeCaliber(int brakeCaliberId, string caliberName, string budwegNo, bool stockStatus, string brakeSystem, string linkQRCode)
        {
            BrakeCaliber brakeCaliber = new(brakeCaliberId, caliberName, budwegNo, stockStatus, brakeSystem, linkQRCode);
            int addedCaliber = brakeRepo.Add(brakeCaliber);
            brakeCaliberList.Add(brakeRepo.GetById(addedCaliber));
        }

        public void AddBrakeCaliber(string caliberName, string budwegNo, bool stockStatus, string brakeSystem, string linkQRCode)
        {
            BrakeCaliber brakeCaliber = new(caliberName, budwegNo, stockStatus, brakeSystem, linkQRCode);
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
    }
}
