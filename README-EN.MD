# WidgetTimeViewer

[日本語](README.MD)

A BepInEx mod for [Elin](https://store.steampowered.com/app/2135150/Elin/)

Adds time display to the widget in the upper left corner of the screen.

---

## Features

- Displays year, month, day, and time in the widget
- Shows time in Japanese format for JP locale, "HH:MM" format for others

## Installation

Just subscribe to [WidgetTimeViewer](https://steamcommunity.com/sharedfiles/filedetails/?id=3361530432) from Steam Workshop

## Development and checks

Copy reference DLLs into `lib/` from Elin Stable 23.355 (`Elin_Data/Managed/`) and its installed `BepInEx/core/`. Use the installed BepInEx and Harmony versions rather than upgrading them separately.

```powershell
dotnet build WidgetTimeViewer.csproj -c Release
dotnet run --project tests/WidgetTimeViewer.Tests.csproj -c Release
```

The build requires the .NET Framework 4.7.2 targeting pack; checks require .NET 10 SDK. Output: `bin/Release/WidgetTimeViewer.dll`. Reference DLLs are not copied to the output directory.

Checks use game stubs to verify formatting, caching, and date/language changes. They do not measure in-game rendering or prove an input-lag fix.

## Changelog

See [Changelog.md](resouce/Changelog.md)

## License

[MIT License](LICENCE)
