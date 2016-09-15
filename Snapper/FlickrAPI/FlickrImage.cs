using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snapper.FlickrAPI
{
    public class FlickrImage
    {
        public string id { get; set; }
        public string owner { get; set; }
        public string title { get; set; }

        public FlickrImage()
        {
            id = "";
            owner = "";
            title = "";
        }
    }
}
