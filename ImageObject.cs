using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ChristmasWallpaper
{
    class ImageObject
    {
        private Image image;

        public ImageObject(String imageFilePath)
        {
            image = Image.FromFile(imageFilePath);
        }
    }
}
