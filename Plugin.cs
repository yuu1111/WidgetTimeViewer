using BepInEx;
using HarmonyLib;
using BepInEx.Logging;

namespace WidgetTimeViewer
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        // const
        private const string PluginGuid = "com.github.yuu1111.widgettimeviewer";
        private const string PluginName = "WidgetTimeViewer Mod";
        private const string PluginVersion = "1.0.4";

        internal new static ManualLogSource Logger;

        public void Awake()
        {
            Logger = base.Logger;
            Logger.LogInfo("WidgetTimeViewer v" + PluginVersion + " is loaded!");
            new Harmony(PluginGuid).PatchAll();
        }
    }
}
