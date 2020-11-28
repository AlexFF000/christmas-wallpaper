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

        public void OverlaySmallerImage(ImageObject overlay)  // Overlay a smaller image on top of this one (or if not smaller then resize)
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

        public void OverlayEqualImage(ImageObject overlay)  // Overlay an image of the same size as this one on top of this one
        {
            Image overlayImageData = overlay.GetImage();
            using (Graphics g = Graphics.FromImage(image))
            {
                g.DrawImage(overlayImageData, new Rectangle(0, 0, overlayImageData.Width, overlayImageData.Height));
            }
        }

        public void SaveImage(String path)
        {
            image.Save(path, image.RawFormat);
        }
    }
}
