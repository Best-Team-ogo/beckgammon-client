using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ViewModel.Models
{
    public class Icons
    {
        public static Image BackgammonIcon = GetImageFor("BackgammonIcon .jpg");
        public static Image ChatIcon = GetImageFor("ChatIcon.jpg");
        public static Image MenuIcon = GetImageFor("MenuDownArrow.jpg");

        public static BitmapImage Die1 = GetBitmapImageFor("Die1.jpg");
        public static BitmapImage Die2 = GetBitmapImageFor("Die2.jpg");
        public static BitmapImage Die3 = GetBitmapImageFor("Die3.jpg");
        public static BitmapImage Die4 = GetBitmapImageFor("Die4.jpg");
        public static BitmapImage Die5 = GetBitmapImageFor("Die5.jpg");
        public static BitmapImage Die6 = GetBitmapImageFor("Die6.jpg");

        public static ImageBrush Board = GetImageBrushFor("BackgammonBoard1.png");

        private static ImageBrush GetImageBrushFor(string photo)
        {
            string path = $"Assets/{photo}";
            string imagePath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), path);

            var img = new BitmapImage();
            img.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
            return new ImageBrush()
            {
                ImageSource = img
            };
        }

        private static Image GetImageFor(string photo)
        {
            string path = $"Assets/{photo}";
            string imagePath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), path);

            var img = new BitmapImage();
            img.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
            return new Image()
            {
                Source = img
            };
        }

        private static BitmapImage GetBitmapImageFor(string photo)
        {
            string path = $"Assets/{photo}";
            string imagePath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), path);

            var img = new BitmapImage();
            img.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
            return img;
        }
    }
}
