using BepInEx.Logging;
using HarmonyLib;
using System;
using static Date;

namespace WidgetTimeViewer
{

    [HarmonyPatch(typeof(Date))]
    [HarmonyPatch(typeof(Date), "GetText", new Type[] { typeof(TextFormat) })]
    static class TextChange
    {
        static bool Prefix(Date __instance, TextFormat format, ref string __result)
        {
            if (format == TextFormat.Widget)
            {
                string lang = EClass.core.config.lang;
                if (lang == "JP")
                {
                    __result = "dateYearMonthDay".lang(__instance.year.ToString() ?? "", __instance.month.ToString() ?? "", __instance.day.ToString() ?? "", __instance.hour.ToString()) + " " + __instance.hour.ToString() + "時" + __instance.min.ToString() + "分";
                    return false;
                } else
                {
                    __result = "dateYearMonthDay".lang(__instance.year.ToString() ?? "", __instance.month.ToString() ?? "", __instance.day.ToString() ?? "", __instance.hour.ToString()) + " " + __instance.hour.ToString() + ":" + __instance.min.ToString();
                    return false;
                }
            }
            return true;
        }
    }
}