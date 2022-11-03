# Yummy Cook

Aplikace doporuèující recepty podle dostupných ingrediencí

## Struktura projektu

### yammyCook.csproj
- Základní nastavení app (jméno, verze, ikony, splashScreen, fonty)

### folder: Platforms
- Specifický kód pro dané platformy

### folder: Resources
- obsahuje všechny soubory použité v app (fotky, splashScreen, STYLY, fonty, ikony)
	- Styles
		- Colors - uložení hex. kódù barev do promenných 
		- Styles - styly všech stavebních prvkù app (Buttons, Pickers, Frames, Labels...)
		- Definujeme si vše na jednom místì, což ušetøí dost øádkù v XAMLu

## Tipy
- Jednoduchá implementace dark mode
```Xaml
<Setter Property="BackgroundColor" Value="{AppThemeBinding Light={StaticResource White}, Dark={StaticResource Black}}" />
```

## Užiteèné odkazy
- [.NET MAUI MVVM and Data Binding](https://youtu.be/XmdBXuNPShs)
- [.NET MAUI Full course](https://www.youtube.com/watch?v=DuNLR_NJv8U&list=WL&index=8)
- [Pøednáška IW5 2021](https://youtu.be/YpxtzUXLgCs)

## Debugging
- Emulátor nebo fyzické zaøízení
- USB nebo Wifi

### Aktivace
Nastavení -> vývojaøské možnosti (nutné altivovat: Informace o softwaru -> 7x kliknou na èíslo sestavení) -> Ladìní
- bezdrátové ladìní: (telefon) Android >= 11, (VS) Android SDK >= 30
	- ve VS otevøít Android Adb Command Prompt
```bash
adb pair [IP:port (údaje v telefonu)] 

Enter pairing code: 

adb connect [IP telefonu]
```