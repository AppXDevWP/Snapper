using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snapper.API {
    public class FlickrImage {
        public string id { get; set; }

        public string owner { get; set; }

        public string title { get; set; }

        public string secret { get; set; }

        public string server { get; set; }

        public string farm { get; set; }

        public string image_url { get; set; }

        public string author { get; set; }

        public string description { get; set; }

        public FlickrImage() {
            id = "";
            owner = "";
            title = "";
            secret = "";
            server = "";
            farm = "";
            image_url = "";
            author = "";
            description = "";
        }
    }
}
