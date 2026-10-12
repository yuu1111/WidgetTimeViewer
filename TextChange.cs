using HarmonyLib;
using static Date;

namespace WidgetTimeViewer
{
    [HarmonyPatch(typeof(Date), nameof(Date.GetText), typeof(TextFormat))]
    internal static class DateTextPatch
    {
        private static Date cachedInstance;
        private static string cachedLanguage;
        private static int cachedYear, cachedMonth, cachedDay, cachedHour, cachedMinute;
        private static string cachedDate;
        private static string cachedText;

        private static bool Prefix(Date __instance, TextFormat format, ref string __result)
        {
            if (format != TextFormat.Widget || EClass.core == null || EClass.core.config == null)
            {
                return true;
            }

            string language = EClass.core.config.lang;
            bool dateChanged = cachedDate == null
                || !ReferenceEquals(cachedInstance, __instance)
                || cachedLanguage != language
                || cachedYear != __instance.year
                || cachedMonth != __instance.month
                || cachedDay != __instance.day;

            if (dateChanged)
            {
                cachedDate = "dateYearMonthDay".lang(
                    __instance.year.ToString(),
                    __instance.month.ToString(),
                    __instance.day.ToString());
                cachedInstance = __instance;
                cachedLanguage = language;
                cachedYear = __instance.year;
                cachedMonth = __instance.month;
                cachedDay = __instance.day;
            }

            if (dateChanged || cachedHour != __instance.hour || cachedMinute != __instance.min)
            {
                cachedText = language == "JP"
                    ? $"{cachedDate} {__instance.hour}時{__instance.min}分"
                    : $"{cachedDate} {__instance.hour}:{__instance.min:00}";
                cachedHour = __instance.hour;
                cachedMinute = __instance.min;
            }

            __result = cachedText;
            return false;
        }
    }
}
