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
        ResourceRepository ResourceRepo = new ResourceRepository();
        FeedbackRepository FeedbackRepo = new FeedbackRepository();

        public ObservableCollection<BrakeCaliber> brakeCaliberList { get; set; }
        public ObservableCollection<Resource> resourceList { get; set; }
        public ObservableCollection<Feedback> feedbackList { get; set; }

        public CustomerSupportViewModel()
        {
            foreach (BrakeCaliber brake in brakeRepo.GetAll())
            {
                brakeCaliberList.Add(brake);
            }
            foreach (Resource resource in ResourceRepo.GetAll())
            {
                resourceList.Add(resource);
            }
            foreach (Feedback feedback in FeedbackRepo.GetAll())
            {
                feedbackList.Add(feedback);
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

        public Resource GetResource(int id)
        {
            Resource resourceResult = null;
            foreach (Resource resource in resourceList)
            {
                if (id == resource.Id)
                {
                    resourceResult = resource;
                }
            }
            return resourceResult;
        }

        public Resource UpdateResource(int id)
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
            Feedback feedbackResult = null;
            foreach (Feedback feedback in feedbackList)
            {
                if (id == feedback.Id)
                {
                    feedbackResult = feedback;
                }
            }
            return feedbackResult;
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
