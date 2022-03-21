using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budweg.Domain
{
    public class Resource
    {
        public int ResourceId { get; set; }
        public string Title { get; set; }
        public int Views { get; }
        public string VersionNumber { get; set; }
        public List<BrakeCaliber> BrakeCalibers { get; set; }

        public Resource(int resourceId, string title, int views, string versionNumber)
        {
            ResourceId = resourceId;
            Title = title;
            Views = views;
            VersionNumber = versionNumber;
            BrakeCalibers = new ();
        }

        public Resource(string title, string versionNumber)
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
