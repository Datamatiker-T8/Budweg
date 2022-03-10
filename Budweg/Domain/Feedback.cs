using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Budweg.Domain
{
    public class Feedback
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public List<string> ImagePaths { get; set; }
        public string Description { get; set; }
        public List<BrakeCaliber> BrakeCalibers { get; set; }
        public List<Customer> Customers { get; set; }


        public Feedback(int rating, string description):this(rating, description, 0) 
        { 
        }

        public Feedback(int rating, string description, int id)
        {
            Rating = rating;
            Description = description;
            Id = id;
        }
    }
}
