﻿using Budweg.Domain;
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
        ResourceRepository resourceRepo = new ResourceRepository();
        FeedbackRepository feedbackRepo = new FeedbackRepository();

        public ObservableCollection<BrakeCaliber> brakeCaliberList { get; set; }
        public ObservableCollection<Resource> resourceList { get; set; }
        public ObservableCollection<Feedback> feedbackList { get; set; }

        public CustomerSupportViewModel()
        {
            foreach (BrakeCaliber brake in brakeRepo.GetAll())
            {
                brakeCaliberList.Add(brake);
            }
            foreach (Resource resource in resourceRepo.GetAll())
            {
                resourceList.Add(resource);
            }
            foreach (Feedback feedback in feedbackRepo.GetAll())
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
                    // Ændres i databasen her.
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

        // -------------------------------------------
        //
        // RESOURCE
        //
        // -------------------------------------------

        public void AddResource(string title, string versionNumber)
        {
            Resource resource = new Resource(title, versionNumber);
            int addedResource = resourceRepo.Add(resource);

            resourceList.Add(resourceRepo.GetById(addedResource));
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

        public void UpdateResource(int id, string title, string versionNumber)
        {
            foreach (Resource resource in resourceList)
            {
                if (id == resource.Id)
                {
                    resource.Title = title;
                    resource.VersionNumber = versionNumber;
                    // Ændres i databasen her.
                }
            }
        }

        public void DeleteResource(int id)
        {
            foreach (Resource resource in resourceList)
            {
                if (id == resource.Id)
                {
                    resourceList.Remove(resource);
                    resourceRepo.Remove(resource);
                }
            }
        }

        // -------------------------------------------
        //
        // FEEDBACK
        //
        // -------------------------------------------

        public void AddFeedback(int rating, string description)
        {
            Feedback feedback = new Feedback(rating, description);
            int addedFeedback = feedbackRepo.Add(feedback);

            feedbackList.Add(feedbackRepo.GetById(addedFeedback));
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

        public void UpdateFeedback(int id, int rating, string description)
        {
            foreach(Feedback feedback in feedbackList)
            {
                if (id == feedback.Id)
                {
                    feedback.Rating = rating;
                    feedback.Description = description;
                    // Ændres i databasen her.
                }
            }
        }

        public void DeleteFeedback(int id)
        {
            foreach (Feedback feedback in feedbackList)
            {
                if (id == feedback.Id)
                {
                    feedbackList.Remove(feedback);
                    feedbackRepo.Remove(feedback);
                }
            }
        }
    }
}