# WidgetTimeViewer

[English](README-EN.MD)

[Elin](https://store.steampowered.com/app/2135150/Elin/) 用のBepInEx Mod

画面左上の表示情報に時間表示を追加します。

---

## 機能

- ウィジェットに年月日と時刻を表示
- 日本語環境では「○時○分」形式、それ以外では「HH:MM」形式で表示

## インストール

Steam Workshopから [WidgetTimeViewer](https://steamcommunity.com/sharedfiles/filedetails/?id=3361530432) をサブスクライブ

## 開発・検証

参照DLLはElin Stable 23.355の `Elin_Data/Managed/` と、同じインストール先の `BepInEx/core/` から `lib/` にコピーします。BepInExやHarmonyを別途最新版へ更新する必要はありません。

```powershell
dotnet build WidgetTimeViewer.csproj -c Release
dotnet run --project tests/WidgetTimeViewer.Tests.csproj -c Release
```

ビルドには.NET Framework 4.7.2のターゲティングパック、テストには.NET 10 SDKが必要です。出力は `bin/Release/WidgetTimeViewer.dll` です。参照DLLは出力先へコピーしません。

テストはゲームの型をスタブに置き換え、表示形式・キャッシュ・日時と言語の変更を検証します。ゲーム内の描画負荷や入力遅延の改善を保証するものではありません。

## 変更履歴

[Changelog.md](resouce/Changelog.md) を参照

## ライセンス

[MIT License](LICENCE)
