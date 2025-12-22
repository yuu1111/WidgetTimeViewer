using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace WidgetTimeViewer
{
    /// <summary>
    /// 時間ウィジェットの表示形式を改善するBepInExプラグイン
    /// </summary>
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        private const string PluginGuid = "com.github.yuu1111.widgettimeviewer";
        private const string PluginName = "WidgetTimeViewer";
        private const string PluginVersion = "1.0.5";

        /// <summary>
        /// プラグイン用のログ出力インスタンス
        /// </summary>
        internal new static ManualLogSource Logger;

        /// <summary>
        /// プラグイン初期化時に呼び出される
        /// </summary>
        public void Awake()
        {
            Logger = base.Logger;
            Logger.LogInfo(PluginName + " v" + PluginVersion + " is loaded!");
            new Harmony(PluginGuid).PatchAll();
        }
    }
}
