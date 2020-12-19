using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;


namespace ChristmasWallpaper
{
    static class State
    {
        private const string ConfigFilePath = @"..\..\config.json";
        private const string StateFilePath = @"..\..\state.json";
        public static DateTime StartDate;  // The date to start modifying the background
        public static DateTime EndDate;  // The day of the last modification, after which the modifications will be removed
        public static string BaseImage;  // The name of the base image, on which everything else will be added
        public static int DaysElapsed;  // The number of days between start and end date that a modification has been added
        public static Dictionary<string, string> Images;  // Names of images
        public static List<string> ImagesUsed;  // List of images that have already been used
        public static string OriginalWallpaperPath;  // The path of the original wallpaper, to revert back to after the last day

        public static void LoadConfig()
        {
            // Load data from config file
            using (FileStream configFile = File.OpenRead(ConfigFilePath))
            {
                byte[] fileContent = new byte[configFile.Length];
                configFile.Read(fileContent, 0, fileContent.Length);
                ConfigFields configData = JsonSerializer.Deserialize<ConfigFields>(fileContent);
                StartDate = DateTime.Parse(configData.StartDate);
                EndDate = DateTime.Parse(configData.EndDate);
                Images = configData.Images;
                BaseImage = configData.BaseImage;

                // Program does not take year into account, so change year to current year
                int currentYear = DateTime.Now.Year;
                StartDate = new DateTime(currentYear, StartDate.Month, StartDate.Day);
                EndDate = new DateTime(currentYear, EndDate.Month, EndDate.Day).AddHours(23).AddMinutes(59).AddSeconds(59);  // Time for end date should be 23:59:59
            }
            
        }

        public static void SaveConfig()
        {
            // Save settings to config file
            ConfigFields configData = new ConfigFields();
            configData.StartDate = StartDate.ToString("yyyy-MM-dd");
            configData.EndDate = EndDate.ToString("yyyy-MM-dd");
            configData.Images = Images;
            configData.BaseImage = BaseImage;
            string configJson = JsonSerializer.Serialize<ConfigFields>(configData);
            File.WriteAllText(ConfigFilePath, configJson);

        }

        public static void LoadState()
        {
            // Load data from state file
            using (FileStream stateFile = File.OpenRead(StateFilePath))
            {
                byte[] fileContent = new byte[stateFile.Length];
                stateFile.Read(fileContent, 0, fileContent.Length);
                StateFields stateData = JsonSerializer.Deserialize<StateFields>(fileContent);
                DaysElapsed = stateData.DaysElapsed;
                ImagesUsed = stateData.ImagesUsed;
                OriginalWallpaperPath = stateData.OriginalWallpaperPath;
            }

        }

        public static void SaveState()
        {
            // Save state to state file
            StateFields stateData = new StateFields();
            stateData.DaysElapsed = DaysElapsed;
            stateData.ImagesUsed = ImagesUsed;
            stateData.OriginalWallpaperPath = OriginalWallpaperPath;
            string stateJson = JsonSerializer.Serialize<StateFields>(stateData);
            File.WriteAllText(StateFilePath, stateJson);
        }
    }

    public class ConfigFields
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Dictionary<string, string> Images { get; set; }
        public string BaseImage { get; set; }
    }
    public class StateFields
    {
        public int DaysElapsed { get; set; }
        public List<string> ImagesUsed { get; set; }
        public string OriginalWallpaperPath { get; set; }
    }
}
