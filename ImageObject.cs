using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace ChristmasWallpaper
{
    class ImageObject
    {
        private Image image;

        public ImageObject(String imageFilePath)
        {
            image = Image.FromFile(imageFilePath);
        }

        public Image GetImage()
        {
            return image;
        }

        public void OverlayImage(ImageObject overlay)  // Overlay the given image on top of this one
        {
            Image overlayImageData = overlay.GetImage();

            // Calculate coordinates such that overlay image is vertically at bottom of base image and horizontally centred
            int xValue = (image.Width / 2) - (overlayImageData.Width / 2);
            int yValue = image.Height - overlayImageData.Height;
            
            using (Graphics g = Graphics.FromImage(image))
            {
                // Must use rectange overload instead of point or overlay image may be resized
                g.DrawImage(overlayImageData, new Rectangle(xValue, yValue, overlayImageData.Width, overlayImageData.Height));
            }
        }

        public void SaveImage(String path)
        {
            image.Save(path, image.RawFormat);
        }
    }
}
