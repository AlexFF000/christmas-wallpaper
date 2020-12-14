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
        public static int DaysElapsed;  // The number of days between start and end date that a modification has been added
        public static Dictionary<string, string> Images;  // Names of images
        public static List<string> ImagesUsed;  // List of images that have already been used

        public static void LoadConfig()
        {
            // Load data from config file
            using (FileStream configFile = File.OpenRead(ConfigFilePath))
            {
                byte[] fileContent = new byte[configFile.Length];
                configFile.Read(fileContent, 0, fileContent.Length);
                Dictionary<string, DateTime> configData = JsonSerializer.Deserialize<Dictionary<string, DateTime>>(fileContent);
                configData.TryGetValue("StartDate", out StartDate);
                configData.TryGetValue("EndDate", out EndDate);
            }
            
        }

        public static void SaveConfig()
        {
            // Save settings to config file
            Dictionary<string, string> configData = new Dictionary<string, string>();
            configData.Add("StartDate", StartDate.ToString("yyyy-MM-dd"));
            configData.Add("EndDate", StartDate.ToString("yyyy-MM-dd"));
            string configJson = JsonSerializer.Serialize<Dictionary<string, string>>(configData);
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
                Images = stateData.Images;
                ImagesUsed = stateData.ImagesUsed;
            }

        }

        public static void SaveState()
        {
            // Save state to state file
            StateFields stateData = new StateFields();
            stateData.DaysElapsed = DaysElapsed;
            stateData.Images = Images;
            stateData.ImagesUsed = ImagesUsed;
            string stateJson = JsonSerializer.Serialize<StateFields>(stateData);
            File.WriteAllText(StateFilePath, stateJson);
        }
    }

    public class StateFields
    {
        public int DaysElapsed { get; set; }
        public Dictionary<string, string> Images { get; set; }
        public List<string> ImagesUsed { get; set; }
    }
}
