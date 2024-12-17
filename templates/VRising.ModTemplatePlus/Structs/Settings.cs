using BepInEx;
using BepInEx.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DiceRoller.Structs;

#if (HOWTO)
// This struct allows you to neatly contain and order all the Settings for people to configure the mod. 
// Typically you'll include a global toggle boolean (ToggleMod) that allows the user to easily enable/distable the mod without deleting. 
// By default, BepInEx will order the "sections" alphabetically. In this example we use a specified OrderedSections to order the sections ourselves.
// To learn more about BepInEx configuration, see: https://docs.bepinex.dev/api/BepInEx.Configuration.html
// Credits: The InitConfigEntry<T>() utility is from zfolmt's RaidGuard mod. 
// Credits: The ReorderConfigSections utility is from CrimsonMod's CrimsonBanned mod.  
#endif
public readonly struct Settings
{
    public static ConfigEntry<bool> ToggleMod { get; private set; }

    private static readonly List<string> OrderedSections = new()
    {
        "Config",
    };

    public static void InitConfig()
    {
        ToggleMod = InitConfigEntry(OrderedSections[0], "Toggle", true,
            "If true the mod will be usable; otherwise it will be disabled.");

        ReorderConfigSections();
    }

    static ConfigEntry<T> InitConfigEntry<T>(string section, string key, T defaultValue, string description)
    {
        // Bind the configuration entry and get its value
        var entry = Plugin.Instance.Config.Bind(section, key, defaultValue, description);

        // Check if the key exists in the configuration file and retrieve its current value
        var newFile = Path.Combine(Paths.ConfigPath, $"{MyPluginInfo.PLUGIN_GUID}.cfg");

        if (File.Exists(newFile))
        {
            var config = new ConfigFile(newFile, true);
            if (config.TryGetEntry(section, key, out ConfigEntry<T> existingEntry))
            {
                // If the entry exists, update the value to the existing value
                entry.Value = existingEntry.Value;
            }
        }
        return entry;
    }

    private static void ReorderConfigSections()
    {
        var configPath = Path.Combine(Paths.ConfigPath, $"{MyPluginInfo.PLUGIN_GUID}.cfg");
        if (!File.Exists(configPath)) return;

        var lines = File.ReadAllLines(configPath).ToList();
        var sectionsContent = new Dictionary<string, List<string>>();
        string currentSection = "";

        // Parse existing file
        foreach (var line in lines)
        {
            if (line.StartsWith("["))
            {
                currentSection = line.Trim('[', ']');
                sectionsContent[currentSection] = new List<string> { line };
            }
            else if (!string.IsNullOrWhiteSpace(currentSection))
            {
                sectionsContent[currentSection].Add(line);
            }
        }

        // Rewrite file in ordered sections
        using var writer = new StreamWriter(configPath, false);
        foreach (var section in OrderedSections)
        {
            if (sectionsContent.ContainsKey(section))
            {
                foreach (var line in sectionsContent[section])
                {
                    writer.WriteLine(line);
                }
                writer.WriteLine(); // Add spacing between sections
            }
        }
    }
}