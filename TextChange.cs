using HarmonyLib;
using static Date;

namespace WidgetTimeViewer
{
    /// <summary>
    /// Date.GetText()メソッドをパッチしてウィジェットの時間表示を改善する
    /// </summary>
    [HarmonyPatch(typeof(Date), nameof(Date.GetText), typeof(TextFormat))]
    internal static class DateTextPatch
    {
        /// <summary>
        /// ウィジェット用の時間テキストを独自フォーマットで上書きする
        /// </summary>
        /// <param name="__instance">Dateインスタンス</param>
        /// <param name="format">テキストフォーマット種別</param>
        /// <param name="__result">出力される結果文字列</param>
        /// <returns>元メソッドの実行をスキップするかどうか</returns>
        private static bool Prefix(Date __instance, TextFormat format, ref string __result)
        {
            if (format != TextFormat.Widget)
            {
                return true;
            }

            string formattedDate = FormatDate(__instance);
            string formattedTime = FormatTime(__instance);
            string lang = EClass.core.config.lang;

            __result = lang == "JP"
                ? $"{formattedDate} {__instance.hour}時{__instance.min}分"
                : $"{formattedDate} {formattedTime}";

            return false;
        }

        /// <summary>
        /// 日付部分をフォーマットする
        /// </summary>
        private static string FormatDate(Date instance)
        {
            return "dateYearMonthDay".lang(
                instance.year.ToString() ?? "",
                instance.month.ToString() ?? "",
                instance.day.ToString() ?? "",
                instance.hour.ToString()
            );
        }

        /// <summary>
        /// 時刻部分をフォーマットする (HH:MM形式)
        /// </summary>
        private static string FormatTime(Date instance)
        {
            string minute = instance.min < 10 ? $"0{instance.min}" : instance.min.ToString();
            return $"{instance.hour}:{minute}";
        }
    }
}