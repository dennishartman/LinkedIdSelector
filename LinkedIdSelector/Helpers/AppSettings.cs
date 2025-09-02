using System.IO;
using Newtonsoft.Json;

public class AppSettings
{
    private const string SettingsFile = "UserSettings.json";

    private static AppSettings _instance;

    public static AppSettings Instance
    {
        get
        {
            if (_instance == null)
                _instance = Load();
            return _instance;
        }
    }

    public string FilePath { get; set; } = @"C:\Workspace";

    public void Save()
    {
        string json = JsonConvert.SerializeObject(this, Formatting.Indented);
        File.WriteAllText(SettingsFile, json);
    }

    private static AppSettings Load()
    {
        if (File.Exists(SettingsFile))
        {
            try
            {
                string json = File.ReadAllText(SettingsFile);
                return JsonConvert.DeserializeObject<AppSettings>(json) ?? new AppSettings();
            }
            catch
            {
                // Handle corrupt JSON
                return new AppSettings();
            }
        }
        else
        {
            // Create file with defaults
            var settings = new AppSettings();
            settings.Save();
            return settings;
        }
    }
}
