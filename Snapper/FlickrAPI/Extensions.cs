using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Snapper.FlickrAPI
{
    public class Extensions
    {
        public static string api_key = "439f10aceb81cfe1a5bf1ed3aad1d35a";

        public static async Task<List<FlickrImage>> getTrendingImages(int per_page = 5)
        {
            var list = new List<FlickrImage>();
            var call_url = "https://api.flickr.com/services/rest/?method=flickr.interestingness.getList&api_key=" + api_key + "&per_page=" + per_page + "&format=rest";

            var response = await FlickrClient.getResponse(call_url);
            var elements = FlickrClient.parseXml(response);

            foreach (XElement item in elements)
            {
                FlickrImage obj = new FlickrImage();
                obj.id = item.Attribute("id").Value;
                obj.owner = item.Attribute("owner").Value;
                obj.title = item.Attribute("title").Value;

                list.Add(obj);
            }

            return list;
        }
    }
}
