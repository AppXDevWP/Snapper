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

        public static List<XElement> parseXml(string xml)
        {
            XElement xmlitems = XElement.Parse(xml);
            List<XElement> elements = xmlitems.Descendants("photos").ToList();

            return elements;
        }
    }
}
