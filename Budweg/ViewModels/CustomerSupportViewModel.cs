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
        public ObservableCollection<BrakeCaliber> BrakeCaliberList { get; set; }
        public ObservableCollection<Ressource> RessourceList { get; set; }
        public ObservableCollection<Feedback> FeedbackList { get; set; }

        public CustomerSupportViewModel()
        {
            
        }

        public void AddCaliber() {}

    }
}
