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

            // Make sure overlay image is smaller than base image- resize if necessary
            int overlayWidth = overlayImageData.Width;
            int overlayHeight = overlayImageData.Height;
            int taskbarOffsetX = 0;
            int taskbarOffsetY = 0;

            string taskbarPos = WindowsWallpaper.GetTaskbarPosition();
            if (!WindowsWallpaper.TaskbarIsHidden()) {
                if (taskbarPos == "Left" || taskbarPos == "Right")
                {
                    // Taskbar uses space on x axis
                    taskbarOffsetX = WindowsWallpaper.GetTaskbarHeight();
                }
                else
                {
                    // Taskbar uses space on y axis
                    taskbarOffsetY = WindowsWallpaper.GetTaskbarHeight();
                }
            }
            // If overlay is bigger in either dimension then shrink by 10% until it is smaller
            while (image.Width - taskbarOffsetX < overlayWidth || image.Height - taskbarOffsetY < overlayHeight)
            {
                overlayWidth = (int)(overlayWidth * 0.9);
                overlayHeight = (int)(overlayHeight * 0.9);
            }
            
            
            // Calculate coordinates such that overlay image is vertically at bottom of base image and horizontally centred
            int xValue = (image.Width / 2) - (overlayWidth / 2);
            int yValue = (image.Height - overlayHeight);

            if (taskbarPos == "Right" || taskbarPos == "Bottom")
            {
                // Need to subtract offset
                taskbarOffsetX *= -1;
                taskbarOffsetY *= -1;
            }

            xValue += taskbarOffsetX;
            yValue += taskbarOffsetY;


            using (Graphics g = Graphics.FromImage(image))
            {
                // Must use rectange overload instead of point or overlay image may be resized
                g.DrawImage(overlayImageData, new Rectangle(xValue, yValue, overlayWidth, overlayHeight));
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
