using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using VampireCommandFramework;
using DiceRoller.Structs;

namespace DiceRoller;

#if (HOWTO)
// HOW-TO: This is the main class for your mod. It is what is loaded by BepInEx when the game is started.
// It is also the class that is used to register your Harmony patches.
// You will also see a PluginInfo class that contains information about your mod.

// You will see in this instance a Dependency on VampireCommandFramework. By default that is a HardDependency,
// This means that if VampireCommandFramework is not installed, your mod will not load.
// You can change this to a SoftDependency if you want your mod to load even if VampireCommandFramework is not installed.
// Learn more about Dependencies here: https://docs.bepinex.dev/master/api/BepInEx.BepInDependency.html
#endif
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency("gg.deca.VampireCommandFramework")]
public class Plugin : BasePlugin
{
#if (HOWTO)
    // At the start of our plugin we define our global variables
    // Instance is the reference to this plugin.
    // Harmony is the Harmony instance that is used to patch methods.
    // Log is the logger that is used to log messages to the console.
    // Settings is the settings class that is used to store the settings (config values) for the mod.
#endif
    Harmony _harmony;
    public static Plugin Instance { get; private set; }
    public static Harmony Harmony => Instance._harmony;
    public static ManualLogSource LogInstance => Instance.Log;
    public static Settings Settings { get; private set; }

#if (HOWTO)
    // This is the method that is called when the game is started.
    // It is an automatic method that is called by BepInEx
#endif
    public override void Load()
    {
        Instance = this;
        Settings = new Settings();
        Settings.InitConfig();

        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} version {MyPluginInfo.PLUGIN_VERSION} is loaded!");

        // Harmony patching
        _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        _harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());

        // Register all commands in the assembly with VCF
        CommandRegistry.RegisterAll();
    }

#if (HOWTO)
    // This is the method that is called when the game is closed.
    // It is an automatic method that is called by BepInEx
    // It is also used to unpatch the Harmony patches.
#endif
    public override bool Unload()
    {
        CommandRegistry.UnregisterAssembly();
        _harmony?.UnpatchSelf();
        return true;
    }
}
