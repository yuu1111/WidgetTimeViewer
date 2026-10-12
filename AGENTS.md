# プロジェクト概要

ElinゲームのBepInEx Mod。画面左上のウィジェットに年月日・時刻表示を追加する。

# ビルド

```bash
# Visual Studioでビルド
msbuild WidgetTimeViewer.sln /p:Configuration=Release

# または dotnet CLIで
dotnet build WidgetTimeViewer.csproj -c Release
```

出力先: `bin/Release/WidgetTimeViewer.dll`

# 技術スタック

- C# (.NET Framework 4.7.2)
- BepInEx 6.x (インストール済みElinのBepInExを参照)
- Harmony 2.x (ランタイムパッチライブラリ)

# アーキテクチャ

```
Plugin.cs      -- BepInExエントリポイント、Harmonyパッチを初期化
TextChange.cs  -- Date.GetText()をパッチして時間表示をカスタマイズ
lib/           -- ゲーム本体・BepInEx・Unity等の依存DLL
```

**パッチの仕組み**: `DateTextPatch`クラスがHarmonyのPrefixパッチでゲームの`Date.GetText(TextFormat.Widget)`をインターセプトし、独自フォーマットの時間文字列を返す。日本語環境では「○時○分」形式、その他は「HH:MM」形式。

# バージョン管理

プラグインバージョンは[Plugin.cs:15](Plugin.cs#L15)の`PluginVersion`定数で管理。リリース時はここを更新する。
