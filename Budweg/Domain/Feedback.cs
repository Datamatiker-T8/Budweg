﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Budweg.Domain
{
    public class Feedback
    {
        public int Rating { get; set; }
        public List<string> ImagePaths { get; set; }
        public string Description { get; set; }
        public List<BrakeCaliber> BrakeCalibers { get; set; }
        public List<Customer> Customers { get; set; }

        public Feedback(int rating, string description)
        {
            Rating = rating;
            Description = description;
        }
    }
}