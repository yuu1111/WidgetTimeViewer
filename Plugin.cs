using BepInEx;
using HarmonyLib;
using BepInEx.Logging;


namespace WidgetTimeViewer
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        // const
        public const string PluginGuid = "com.github.yuu1111.widgettimeviewer";
        public const string PluginName = "WidgetTimeViewer Mod";
        public const string PluginVersion = "1.0.3";

        internal static new ManualLogSource Logger;

        public void Awake()
        {
            Logger = base.Logger;
            new Harmony(PluginGuid).PatchAll();
        }
    }
}
