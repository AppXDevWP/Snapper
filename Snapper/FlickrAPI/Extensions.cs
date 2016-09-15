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
        private static string api_key { get; } = "439f10aceb81cfe1a5bf1ed3aad1d35a";

        public static async Task<List<FlickrImage>> getTrendingImages(int per_page = 5)
        {
            var list = new List<FlickrImage>();
            var call_url = "https://api.flickr.com/services/rest/?method=flickr.interestingness.getList&api_key=" + api_key + "&per_page=" + per_page + "&format=rest";

            var response = await FlickrClient.getResponse(call_url);
            var elements = FlickrClient.parseXml(response, "photo");

            foreach (XElement item in elements)
            {
                FlickrImage obj = await getImageDetails(item);

                list.Add(obj);
            }

            return list;
        }

        private static async Task<FlickrImage> getImageDetails(XElement item)
        {
            FlickrImage obj = new FlickrImage();
            obj.id = item.Attribute("id").Value;
            obj.owner = item.Attribute("owner").Value;
            obj.title = item.Attribute("title").Value;
            obj.farm = item.Attribute("farm").Value;
            obj.secret = item.Attribute("secret").Value;
            obj.server = item.Attribute("server").Value;
            obj.image_url = FlickrClient.getImageUrl(obj.id, obj.server, obj.secret, obj.farm, FlickrImageSize.Large);

            var call_url = "https://api.flickr.com/services/rest/?method=flickr.photos.getInfo&api_key=" + api_key + "&photo_id=" + obj.id + "&format=rest";

            var response = await FlickrClient.getResponse(call_url);
            var elements = FlickrClient.parseXml(response, "photo");

            foreach (XElement item2 in elements)
            {
                obj.author = item2.Element("owner").Attribute("realname").Value;
                obj.description = item2.Element("description").Value;
            }

            return obj;
        }
    }
}
