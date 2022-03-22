using Budweg.Domain;
using Budweg.Persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Budweg.ViewModels
{
    public class CustomerSupportViewModel : INotifyPropertyChanged
    {
        BrakecaliberRepository brakeRepo = new BrakecaliberRepository();
        ResourceRepository resourceRepo = new ResourceRepository();
        FeedbackRepository feedbackRepo = new FeedbackRepository();

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            { 
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private List<BrakeCaliber> brakeList;
        public List<BrakeCaliber> BrakeList
        {
            get { return brakeList; }
            set 
            {
                brakeList = value;
            }
        }

        private BrakeCaliber selectedBrake;
        public BrakeCaliber SelectedBrake
        {
            get { return selectedBrake; }
            set
            {
                selectedBrake = value;
                NotifyPropertyChanged("SelectedBrake");
                Name = SelectedBrake.CaliberName;
            }
        }

        private string CnnStr = Properties.Settings.Default.WPF_Connection;
        
        public CustomerSupportViewModel()
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(CnnStr))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = new SqlCommand("select * from [BRAKECALIBER]", connection);
                dataAdapter.Fill(ds);
            }

            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            brakeList = new List<BrakeCaliber>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr = dt.Rows[i];
                BrakeCaliber brakeCaliber = new BrakeCaliber();
                brakeCaliber.BrakeCaliberId = (int)dr["BrakeCaliberId"];
                brakeCaliber.CaliberName = dr["CaliberName"].ToString();
                brakeCaliber.BudwegNo = dr["BudwegNo"].ToString();
                brakeCaliber.StockStatus = bool.Parse(dr["StockStatus"].ToString());
                brakeCaliber.BrakeSystem = dr["BrakeSystem"].ToString();
                brakeCaliber.LinkQRCode = dr["LinkQRCode"].ToString();

                brakeList.Add(brakeCaliber);
            }

            selectedBrake = brakeList[0];
        }



        public ObservableCollection<BrakeCaliber> brakeCaliberList { get; set; }

        public ObservableCollection<Resource> resourceList { get; set; }
        public ObservableCollection<Feedback> feedbackList { get; set; }

        //public CustomerSupportViewModel()
        //{
        //    foreach (BrakeCaliber brake in brakeRepo.GetAll())
        //    {
        //        brakeCaliberList.Add(brake);
        //    }
        //    foreach (Resource resource in resourceRepo.GetAll())
        //    {
        //        resourceList.Add(resource);
        //    }
        //    foreach (Feedback feedback in feedbackRepo.GetAll())
        //    {
        //        feedbackList.Add(feedback);
        //    }
        //}

        // -------------------------------------------
        //
        // BRAKECALIBER
        //
        // -------------------------------------------

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
                if (id == resource.ResourceId)
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
                if (id == resource.ResourceId)
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
                if (id == resource.ResourceId)
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
                if (id == feedback.FeedbackId)
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
                if (id == feedback.FeedbackId)
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
                if (id == feedback.FeedbackId)
                {
                    feedbackList.Remove(feedback);
                    feedbackRepo.Remove(feedback);
                }
            }
        }
    }
}
