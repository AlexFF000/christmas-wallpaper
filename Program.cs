using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ChristmasWallpaper
{
    static class Program
    {
        const string OverlayPath = @"..\..\Images\State\overlay.png";  // Location of image to overlay on desktop
        const string ModifiedWallpaperPath = @"..\..\Images\State\wallpaper.png";  // Base path used to generate paths to store modified wallpaper image
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            // Images\\State path may not exists on first run, so if necessary create it
            if (!Directory.Exists(Path.GetDirectoryName(ModifiedWallpaperPath))) 
            {
                Directory.CreateDirectory(Path.GetDirectoryName(ModifiedWallpaperPath));
            }

            LoadState();
            int updatesNeeded = UpdatesNeeded();
            for (int i = 0; i < updatesNeeded; i++)
            {
                UpdateWallpaper();
            }
            SaveState();


        }

        static void LoadState()
        {
            // Load data from config.json and state.json
            State.LoadConfig();
            State.LoadState();
        }

        static void SaveState()
        {
            // Save changes to config.json and state.json
            State.SaveConfig();
            State.SaveState();
        }

        static int UpdatesNeeded()
        {
            // Calculate the number of updates needed (i.e. number of new images that need to be added)
            if (DateTime.Now < State.StartDate || State.EndDate < DateTime.Now)
            {
                if (State.ImagesUsed.Count != 0)
                {
                    // Don't revert wallpaper if ImagesUsed is empty, as it will already have been reverted
                    RevertWallpaper();
                }
                return 0;
            } 
            // Get number of updates that should have been performed by now
            int expectedUpdates = (DateTime.Now - State.StartDate.AddDays(-1)).Days;
            return expectedUpdates - State.DaysElapsed;
        }

        static void RevertWallpaper()
        {
            // EndDate is over so reset
            WindowsWallpaper.SetWallpaper(State.OriginalWallpaperPath);
            State.ImagesUsed.Clear();
            State.DaysElapsed = 0;
        }

        static void UpdateWallpaper()
        {
            if (!(State.ImagesUsed.Count < State.Images.Count))
            {
                // If there are not enough images and all have been used, then do nothing
                return;
            }
            // Choose a random image to overlay (or use base image for first day)
            string image;
            if (State.DaysElapsed == 0)
            {
                // The first day
                image = State.BaseImage;
                // Save location of current desktop image
                State.OriginalWallpaperPath = WindowsWallpaper.GetWallpaperPath();
                // Save overlay
                string imagePath;
                State.Images.TryGetValue(image, out imagePath);
                ImageObject overlayImage = new ImageObject(imagePath);
                overlayImage.SaveImage(OverlayPath);
                State.ImagesUsed.Add(image);
            }
            else
            {
                image = RandomImage();
                string imagePath;
                State.Images.TryGetValue(image, out imagePath);
                // Overlay image on top of existing overlay
                ImageObject overlayImage = new ImageObject(OverlayPath);
                ImageObject newImage = new ImageObject(imagePath);
                overlayImage.OverlayEqualImage(newImage);
                overlayImage.SaveImage(OverlayPath);
                State.ImagesUsed.Add(image);
            }

            // Overlay on desktop
            ImageObject wallpaper = WindowsWallpaper.GetWallpaper();
            ImageObject overlay = new ImageObject(OverlayPath);
            wallpaper.OverlaySmallerImage(overlay);
            wallpaper.SaveImage(ModifiedWallpaperPath);
            WindowsWallpaper.SetWallpaper(Path.GetFullPath(ModifiedWallpaperPath));
            State.DaysElapsed++;


        }

        static string RandomImage()
        {
            // Randomly select an image that has not yet been used
            List<string> images = new List<string>(State.Images.Keys);
            Random generator = new Random();
            int index = generator.Next(images.Count);
            string image = images[index];
            // Prevent an image being used that has already been used
            while (State.ImagesUsed.Contains(image))
            {
                // Pick again until an unused image is chosen
                index = generator.Next(images.Count);
                image = images[index];
            }
            return image;
        }
    }
}
