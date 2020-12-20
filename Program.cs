using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace ChristmasWallpaper
{
    static class Program
    {
        const string OverlayPath = @"..\..\Images\State\overlay.png";  // Location of image to overlay on desktop
        const string ModifiedWallpaperPath = @"..\..\Images\State\wallpaper.png";  // Base path used to generate paths to store modified wallpaper image
        const string StartupKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // Make sure all needed files are useable
            StartupChecks();
            // If necessary, ask user permission to set program to run on startup
            RunOnStartup();
            // Images\\State path may not exist on first run, so if necessary create it
            if (!Directory.Exists(Path.GetDirectoryName(ModifiedWallpaperPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(ModifiedWallpaperPath));
            }
            // Add to the desktop how ever many times is necessary
            int updatesNeeded = UpdatesNeeded();
            for (int i = 0; i < updatesNeeded; i++)
            {
                UpdateWallpaper();
            }

            // Save configuration and data
            SaveState();

            // Start settings UI
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());


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
            try
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
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Unable to modify images, as program is unauthorised to access one of them", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Exit program with error code 5 (Access denied)
                Environment.Exit(5);
            }
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

        static void RunOnStartup()
        {
            // Add executable to start up if not already there and if user gives permission

            RegistryKey runKey = Registry.CurrentUser.OpenSubKey(StartupKey);
            if (!runKey.GetValueNames().Contains("ChristmasWallpaper"))
            {
                // Ask the user for permission to create shortcut
                if (MessageBox.Show("Grant permission to run on startup? (If you click no, you will need to start the application manually every day)",
                    "Startup Permission",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Add to registry
                    string keyPath = Path.Combine("HKEY_CURRENT_USER", StartupKey);
                    Registry.SetValue(keyPath, "ChristmasWallpaper", Application.ExecutablePath);

                }
            }
        }

        public static void RemoveFromStartup()
        {
            try
            {
                RegistryKey runKey = Registry.CurrentUser.OpenSubKey(StartupKey, true);
                runKey.DeleteValue("ChristmasWallpaper");
                MessageBox.Show("Removed ChristmasWallpaper from startup");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("ChristmasWallpaper is not set to run on startup", "Cannot remove from startup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static void StartupChecks()
        {
            // Check that the necessary files exist before starting

            // Check that config.json and state.json are intact
            try
            {
                State.LoadConfig();
                State.LoadState();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Unable to start program as one or more of the config files (config.json, state.json) cannot be found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Exit program with error code 2 (file not found)
                Environment.Exit(2);
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to start program as one or more of the config files (config.json, state.json) cannot be processed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Exit program with error code 13 (invalid data)
                Environment.Exit(13);
            }
            // Check that all the image files exist
            foreach (string filePath in State.Images.Values)
            {
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Unable to start program as one or more of the image files cannot be found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Exit program with error code 2 (file not found)
                    Environment.Exit(2);
                }
            }
        }
    }
}
