using System;
using System.Collections.Generic;
using System.IO;

namespace OVHDD.Classes
{
    public class AppConstants
    {
        //app names
        public const string Name = "OVH DynDns Changer";        
        public const string OwnAppDataFolderName = ".ovhdd";
                
        //paths
        public static readonly string AppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + OwnAppDataFolderName + Path.DirectorySeparatorChar;
        public static readonly string SettingsFileName = AppDataFolder + "config.json";
    }
}
