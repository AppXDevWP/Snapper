using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Web.Http;

namespace Snapper.FlickrAPI
{
    public class FlickrClient
    {
        public static async Task<string> getResponse(string call_url)
        {
            var client = new HttpClient();
            var response = await client.GetStringAsync(new Uri(call_url, UriKind.Absolute));

            return response;
        }

        public static List<XElement> parseXml(string xml, string header)
        {
            XElement xmlitems = XElement.Parse(xml);
            List<XElement> elements = xmlitems.Descendants(header).ToList();

            return elements;
        }

        public static string getImageUrl(string id, string server_id, string secret, string farm_id, string size = "z")
        {
            if (size.Equals("q") || size.Equals("n") || size.Equals("z") || size.Equals("h") || size.Equals("o"))
            {
                var url = "https://farm" + farm_id + ".staticflickr.com/" + server_id + "/" + id + "_" + secret + "_" + size + ".jpg";

                return url;
            }
            else
            {
                return "";
            }
        }
    }
}
