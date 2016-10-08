using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snapper {
    public class Extensions {
        public static string[] LandingImage = { "ms-appx:///Images/LandingImages/Image1.jpg", "ms-appx:///Images/LandingImages/Image2.jpg", "ms-appx:///Images/LandingImages/Image3.jpg", "ms-appx:///Images/LandingImages/Image4.jpg" };

        public static string getRandomLandingImage() {
            return LandingImage[new Random().Next(1, 4) - 1];
        }
    }
}
