using System.Reflection;
using WidgetTimeViewer;

var prefix = typeof(DateTextPatch).GetMethod("Prefix", BindingFlags.Static | BindingFlags.NonPublic);
int passed = 0;

void Check(bool condition, string requirement)
{
    if (!condition) throw new Exception(requirement);
    passed++;
}

(bool RunOriginal, string Text) Format(Date date, Date.TextFormat format = Date.TextFormat.Widget)
{
    object[] args = { date, format, "untouched" };
    bool runOriginal = (bool)prefix.Invoke(null, args);
    return (runOriginal, (string)args[2]);
}

var date = new Date { year = 514, month = 1, day = 2, hour = 3, min = 4 };
Check(Format(date) == (true, "untouched"), "Before core initialization, preserve the original formatter");
EClass.core = new Core { config = null };
Check(Format(date) == (true, "untouched"), "Before config initialization, preserve the original formatter");
EClass.core.config = new Config();
foreach (var format in Enum.GetValues<Date.TextFormat>())
    if (format != Date.TextFormat.Widget)
        Check(Format(date, format) == (true, "untouched"), $"Preserve non-widget format {format}");

var first = Format(date);
Check(first == (false, "514/1/2 3:04"), "Non-Japanese time retains zero-padded minutes");
int calls = ClassExtension.TranslationCalls;
for (int i = 0; i < 1000; i++)
    if (!ReferenceEquals(first.Text, Format(date).Text)) throw new Exception("Unchanged time must reuse its string");
Check(ClassExtension.TranslationCalls == calls, "Repeated refreshes do not repeat localization");
date.sec = 59;
Check(ReferenceEquals(first.Text, Format(date).Text), "Seconds do not invalidate minute-level display");
date.min = 10;
Check(Format(date).Text == "514/1/2 3:10", "Minute changes appear immediately");
date.hour = 0; date.min = 0;
Check(Format(date).Text == "514/1/2 0:00", "Midnight retains two-digit minutes");
Check(ClassExtension.TranslationCalls == calls, "Time changes do not regenerate the date");

date.day++; Check(Format(date).Text == "514/1/3 0:00", "Day changes invalidate cached date");
date.month++; Check(Format(date).Text == "514/2/3 0:00", "Month changes invalidate cached date");
date.year++; Check(Format(date).Text == "515/2/3 0:00", "Year changes invalidate cached date");
date.year = 500; Check(Format(date).Text == "500/2/3 0:00", "Loading an earlier date invalidates cached date");
EClass.core.config.lang = "JP";
Check(Format(date).Text == "500年2月3日 0時0分", "Switching language updates the date and time");
date.hour = 12; date.min = 9;
Check(Format(date).Text == "500年2月3日 12時9分", "Japanese minutes remain unpadded");
EClass.core.config.lang = "CN";
Check(Format(date).Text == "500/2/3 12:09", "Other languages use colon-separated time");
calls = ClassExtension.TranslationCalls;
var other = new Date { year = 500, month = 2, day = 3, hour = 12, min = 9 };
Check(Format(other).Text == "500/2/3 12:09" && ClassExtension.TranslationCalls == calls + 1,
    "A new Date instance rebuilds its localized display");
Console.WriteLine($"PASS {passed} checks; 1000 unchanged refreshes reuse the output string.");
