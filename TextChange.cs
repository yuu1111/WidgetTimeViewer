using HarmonyLib;
using static Date;

namespace WidgetTimeViewer
{
    
    [HarmonyPatch(typeof(Date), "GetText", typeof(TextFormat))]
    static class TextChange
    {
        static bool Prefix(Date __instance, TextFormat format, ref string __result)
        {
            if (format == TextFormat.Widget)
            {
                // 年月日、時刻の共通フォーマット取得
                string formattedDate = FormatDate(__instance);
                string formattedTime = FormatTime(__instance);

                // 言語設定による出力
                string lang = EClass.core.config.lang;
                
                __result = lang == "JP" ? $"{formattedDate} {__instance.hour}時{__instance.min}分" : $"{formattedDate} {formattedTime}";
                
                return false;
            }
            return true;
        }

        
        // 日付部分をフォーマットする関数
        private static string FormatDate(Date instance)
        {
            return "dateYearMonthDay".lang(
                instance.year.ToString() ?? "",
                instance.month.ToString() ?? "",
                instance.day.ToString() ?? "",
                instance.hour.ToString()
            );
        }

        // 時刻部分をフォーマットする関数
        private static string FormatTime(Date instance)
        {
            string minute = instance.min < 10 ? $"0{instance.min}" : instance.min.ToString();
            return $"{instance.hour}:{minute}";
        }
    }
}