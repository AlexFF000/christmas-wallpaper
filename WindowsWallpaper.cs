using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;


namespace ChristmasWallpaper
{
    static class WindowsWallpaper
    {
        const String wallpaperKeyPath = @"HKEY_CURRENT_USER\Control Panel\Desktop";
        const String taskbarKeyPath = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\StuckRects3";
        const uint SPI_SETDESKWALLPAPER = 20;
        const uint SPIF_SENDCHANGE = 2;
        
        // Use SystemParametersInfo function from user32.dll (Win32 API's UI library) as C# method
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern int SystemParametersInfo(uint uiAction, uint uiParam, string pvParam, uint fWinIni);
       
        public static void SetWallpaper(String newWallpaperPath)  // Change desktop wallpaper
        {

            // Change the wallpaper key in the registry, otherwise the wallpaper change will be lost when the user signs in again
            Registry.SetValue(wallpaperKeyPath, "WallPaper", newWallpaperPath);

            // Change wallpaper fit mode to "stretch" so that everything fits in frame
            Registry.SetValue(wallpaperKeyPath, "WallpaperStyle", "2");

            // Update the wallpaper for the current session (otherwise the changes won't take effect until the user next signs in)
            SystemParametersInfo(
                SPI_SETDESKWALLPAPER,  // Specifies that the wallpaper should be changed
                0,  // 0 as irrelevant to changing wallpaper
                newWallpaperPath,  // Path of new wallpaper image
                SPIF_SENDCHANGE  // Specifies that change should be broadcast to all top level windows (so explorer knows to update the image)
                );  
        }

        public static ImageObject GetWallpaper()
        {
            String wallpaperImagePath = Registry.GetValue(wallpaperKeyPath, "WallPaper", null).ToString();
            return new ImageObject(wallpaperImagePath);

        }

        public static bool TaskbarIsHidden()
        {
            // Byte 8 (starting from 0) of the taskbar settings registry value represents whether the taskbar is hidden
            byte[] taskbarSettings = (byte[])Registry.GetValue(taskbarKeyPath, "Settings", null);
            if (taskbarSettings[8] == 3) return true;
            return false;
        }

        public static string GetTaskbarPosition()
        {
            // Byte 12 (starting from 0) of the taskbar settings registry value represents the taskbar's position on the screen
            byte[] taskbarSettings = (byte[]) Registry.GetValue(taskbarKeyPath, "Settings", null);
            switch (taskbarSettings[12])
            {
                case 0:
                    return "Left";
                case 1:
                    return "Top";
                case 2:
                    return "Right";
                default:
                    return "Bottom";
            }
        }

        public static int GetTaskbarHeight()
        {
            // Get size of working area (screen size excluding taskbar)
            Rectangle workingArea = Screen.GetWorkingArea(new Point(0, 0));
            // Get size of whole screen
            Rectangle screenArea = Screen.GetBounds(new Point(0, 0));

            // Calculate taskbar height
            string taskbarPosition = GetTaskbarPosition();
            if (taskbarPosition == "Top" || taskbarPosition == "Bottom")
            {
                return screenArea.Height - workingArea.Height;
            }
            else
            {
                return screenArea.Width - workingArea.Width;
            }
        }
    }
}
