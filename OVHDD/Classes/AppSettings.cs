using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace OVHDD.Classes
{
    public class AppSettings
    {
        private static FileInfo SettingsFile = new FileInfo(AppConstants.SettingsFileName);
        private static Dictionary<string, object> CurrentSettings;

        public static void Initialize(string key, object value)
        {
            if(!Contains(key))
            {
                Set(key, value);
            }
        }

        public static void Set(string key, object value)
        {
            if(Contains(key))
            {
                CurrentSettings[key] = value;
            }
            else
            {
                CurrentSettings.Add(key, value);
            }
        }

        public static T Get<T>(string key)
        {
            if(CurrentSettings[key] is JArray)
            {
                return ((JArray)CurrentSettings[key]).ToObject<T>();
            }

            return (T)Convert.ChangeType(CurrentSettings[key], typeof(T));
        }

        public static void Remove(string key)
        {
            CurrentSettings.Remove(key);
        }

        public static void RemoveAll()
        {
            foreach(var item in CurrentSettings)
            {
                Remove(item.Key);
            }
        }

        public static bool Contains(string key)
        {
            return CurrentSettings.ContainsKey(key);
        }



        public static void ReadSettingsFile()
        {
            bool error = false;

            try
            {
                var file = GetSettingFile();
                string content = File.ReadAllText(file.FullName);
                CurrentSettings = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
            }
            catch
            {
                error = true;
            }

            if(error)
            {
                var file = GetSettingFile();
                File.WriteAllText(file.FullName, JsonConvert.SerializeObject(new Dictionary<string, object>()));

                InitializeSettings();
                string content = File.ReadAllText(file.FullName);
                CurrentSettings = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
            }
        }

        private static FileInfo GetSettingFile()
        {
            //create folder
            if(!Directory.Exists(SettingsFile.DirectoryName))
            {
                Directory.CreateDirectory(SettingsFile.DirectoryName);
            }

            //create file
            if(!File.Exists(SettingsFile.FullName))
            {
                using(var fs = File.Create(SettingsFile.FullName))
                {
                    fs.Close();
                }
                File.WriteAllText(SettingsFile.FullName, JsonConvert.SerializeObject(new Dictionary<string, object>(), Formatting.Indented));
            }

            return SettingsFile;
        }

        public static void SaveSettingsFile()
        {
            var file = GetSettingFile();
            File.WriteAllText(file.FullName, JsonConvert.SerializeObject(CurrentSettings, Formatting.Indented));
        }

        public static void InitializeSettings()
        {
            ReadSettingsFile();

            //generals
            Initialize("profiles", new List<Profile>());

            SaveSettingsFile();
        }

    }
}
