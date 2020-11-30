using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace ChristmasWallpaper
{
    static class WindowsWallpaper
    {
        const String wallpaperKeyPath = @"HKEY_CURRENT_USER\Control Panel\Desktop";
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
    }
}
