# Yummy Cook

Aplikace doporu�uj�c� recepty podle dostupn�ch ingredienc�

## Struktura projektu

### yammyCook.csproj
- Z�kladn� nastaven� app (jm�no, verze, ikony, splashScreen, fonty)

### folder: Platforms
- Specifick� k�d pro dan� platformy

### folder: Resources
- obsahuje v�echny soubory pou�it� v app (fotky, splashScreen, STYLY, fonty, ikony)
	- Styles
		- Colors - ulo�en� hex. k�d� barev do promenn�ch 
		- Styles - styly v�ech stavebn�ch prvk� app (Buttons, Pickers, Frames, Labels...)
		- Definujeme si v�e na jednom m�st�, co� u�et�� dost ��dk� v XAMLu

## Tipy
- Jednoduch� implementace dark mode
```Xaml
<Setter Property="BackgroundColor" Value="{AppThemeBinding Light={StaticResource White}, Dark={StaticResource Black}}" />
```

## U�ite�n� odkazy
- [.NET MAUI MVVM and Data Binding](https://youtu.be/XmdBXuNPShs)
- [.NET MAUI Full course](https://www.youtube.com/watch?v=DuNLR_NJv8U&list=WL&index=8)
- [P�edn�ka IW5 2021](https://youtu.be/YpxtzUXLgCs)

## Debugging
- Emul�tor nebo fyzick� za��zen�
- USB nebo Wifi

### Aktivace
Nastaven� -> v�voja�sk� mo�nosti (nutn� altivovat: Informace o softwaru -> 7x kliknou na ��slo sestaven�) -> Lad�n�
- bezdr�tov� lad�n�: (telefon) Android >= 11, (VS) Android SDK >= 30
	- ve VS otev��t Android Adb Command Prompt
```bash
adb pair [IP:port (�daje v telefonu)] 

Enter pairing code: 

adb connect [IP telefonu]
```