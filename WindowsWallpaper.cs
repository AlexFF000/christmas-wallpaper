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
        public static void setWallpaper()  // Change desktop wallpaper
        {
            String newWallpaperPath = "";
            

            SystemParametersInfo(
                SPI_SETDESKWALLPAPER,  // Specifies that the wallpaper should be changed
                0,  // 0 as irrelevant to changing wallpaper
                newWallpaperPath,  // Path of new wallpaper image
                SPIF_SENDCHANGE  // Specifies that change should be broadcast to all top level windows (so explorer knows to update the image)
                );  
        }
    }
}
