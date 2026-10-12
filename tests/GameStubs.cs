public class Date
{
    public enum TextFormat { Default, Log, Widget, Schedule, Travel, YearMonthDay, LogPlusYear }
    public int year, month, day, hour, min, sec;
    public string GetText(TextFormat format) => "original";
}

public static class EClass
{
    public static Core core;
}

public class Core
{
    public Config config = new Config();
}

public class Config
{
    public string lang = "EN";
}

public static class ClassExtension
{
    public static int TranslationCalls;
    public static string lang(this string key, string year, string month, string day)
    {
        if (key != "dateYearMonthDay") throw new System.Exception("Unexpected localization key");
        TranslationCalls++;
        return EClass.core.config.lang == "JP"
            ? $"{year}年{month}月{day}日" : $"{year}/{month}/{day}";
    }
}

namespace HarmonyLib
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class HarmonyPatch : System.Attribute
    {
        public HarmonyPatch(System.Type type, string method, System.Type argument) { }
    }
}
