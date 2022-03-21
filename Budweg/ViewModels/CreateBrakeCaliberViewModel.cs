using Budweg.Domain;
using Budweg.Persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budweg.ViewModels
{
    public class CreateBrakeCaliberViewModel
    {
        BrakecaliberRepository brakeRepo = new();
        public ObservableCollection<BrakeCaliber> brakeCaliberList { get; set; } = new ObservableCollection<BrakeCaliber>();
        public CreateBrakeCaliberViewModel()
        {
            foreach (BrakeCaliber brake in brakeRepo.GetAll())
            {
                brakeCaliberList.Add(brake);
            }
        }

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
