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
        BrakecaliberRepository bcrepo = new();
        public ObservableCollection<BrakeCaliber> bcList { get; set; } = new ObservableCollection<BrakeCaliber>();
        public CreateBrakeCaliberViewModel()
        {
            foreach (BrakeCaliber brake in bcrepo.GetAll())
            {
                bcList.Add(brake);
            }
        }

        public void AddBrakeCaliber(int brakeCaliberId, string caliberName, string budwegNo, bool stockStatus, string brakeSystem, string linkQRCode)
        {
            BrakeCaliber brakeCaliber = new(brakeCaliberId, caliberName, budwegNo, stockStatus, brakeSystem, linkQRCode);
            int addedCaliber = bcrepo.Add(brakeCaliber);
            bcList.Add(bcrepo.GetById(addedCaliber));
        }
        public void AddBrakeCaliber(string caliberName, string budwegNo, bool stockStatus, string brakeSystem, string linkQRCode)
        {
            BrakeCaliber brakeCaliber = new(caliberName, budwegNo, stockStatus, brakeSystem, linkQRCode);
            int addedCaliber = bcrepo.Add(brakeCaliber);
            bcList.Add(bcrepo.GetById(addedCaliber));
        } 

        public BrakeCaliber GetBrakeCaliber(int id)
        {
            BrakeCaliber brakeCaliberResult = null;
            foreach (BrakeCaliber brake in bcList)
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
            foreach (BrakeCaliber brake in bcList)
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
            foreach (BrakeCaliber brake in bcList)
            {
                if (id == brake.BrakeCaliberId)
                {
                    bcList.Remove(brake);
                    bcrepo.Remove(brake);
                }
            }
        }
    }
}
