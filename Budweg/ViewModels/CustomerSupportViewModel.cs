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
    public class CustomerSupportViewModel
    {
        BrakecaliberRepository brakeRepo = new BrakecaliberRepository();
        public ObservableCollection<BrakeCaliber> brakeCaliberList { get; set; }
        public ObservableCollection<Ressource> ressourceList { get; set; }
        public ObservableCollection<Feedback> feedbackList { get; set; }

        public CustomerSupportViewModel()
        {
            foreach (BrakeCaliber brake in brakeRepo.GetAll())
            {
                brakeCaliberList.Add(brake);
            }
        }

        // -------------------------------------------
        //
        // BRAKECALIBER
        //
        // -------------------------------------------

        public void AddBrakeCaliber(string modelNumber, string linkQRCode) 
        {
            BrakeCaliber brakeCaliber = new BrakeCaliber(modelNumber, linkQRCode);
            int addedCaliber = brakeRepo.Add(brakeCaliber);

            brakeCaliberList.Add(brakeRepo.GetById(addedCaliber));
        }

        public BrakeCaliber GetBrakeCaliber(int id)
        {
            return brakeRepo.GetById(id);
        }

        public BrakeCaliber UpdateBrakeCaliber(int id)
        {
            return null;
        }

        public void DeleteBrakeCaliber(int id)
        {

        }

        // -------------------------------------------
        //
        // RESOURCE
        //
        // -------------------------------------------

        public void AddResource()
        {

        }

        public Ressource GetResource(int id)
        {
            return null;
        }

        public Ressource UpdateResource(int id)
        {
            return null;
        }

        public void DeleteResource(int id)
        {

        }

        // -------------------------------------------
        //
        // FEEDBACK
        //
        // -------------------------------------------

        public void AddFeedback()
        {

        }

        public Feedback GetFeedback(int id)
        {
            return null;
        }

        public Feedback UpdateFeedback(int id)
        {
            return null;
        }

        public void DeleteFeedback(int id)
        {

        }



    }
}
