﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budweg.Domain
{
    public class Ressource
    {
        public string Title { get; set; }
        public int Views { get; }

        public string VersionNumber { get; set; }
        public List<BrakeCaliber> BrakeCalibers { get; set; }

        public Ressource(string title, string versionNumber)
        {
            Title = title;
            VersionNumber = versionNumber;
        }
        public void ViewCounter()
        {
            // TODO: Should count the amount of clicks/views for this ressource
        }
    }
}