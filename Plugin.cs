using BepInEx;
using HarmonyLib;
using BepInEx.Logging;


namespace WidgetTimeViwer
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        // const
        public const string pluginGuid = "com.github.yuu1111.widgettimeviwer";
        public const string pluginName = "WidgetTimeViwer Mod";
        public const string pluginVersion = "1.0.0";

        internal static new ManualLogSource Logger;

        public void Awake()
        {
            Logger = base.Logger;
            new Harmony(pluginGuid).PatchAll();
        }
    }
}
