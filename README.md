# Yummy Cook

Aplikace doporu�uj�c� recepty podle dostupn�ch ingredienc�

## Struktura projektu

### yammyCook.csproj
- Z�kladn� nastaven� app (jm�no, verze, ikony, splashScreen, fonty)

### folder: Firebase
- Obsahuje t��dy pro komunikaci s Firebase a modely pro jednotliv� typy objekt�

### folder: Models


### folder: ViewModels
- Obsahuje view modely pro jednotliv� str�nky (n�kter� str�nky pou��vaj� stejn�, �i v�ce v�ce r�zn�ch view model�)

### folder: Views
- Obsahuje views v�ech str�nek pou��van�ch v aplikaci 
	- Main obsahuje views str�nek, kter� vyu��v� Shell navigace v tab bar
	- Others obsahuje v�echny ostatn� views str�nek aplikace

### folder: Platforms
- Specifick� k�d pro dan� platformy

### folder: Resources
- obsahuje v�echny soubory pou�it� v app (fotky, splashScreen, STYLY, fonty, ikony)
	- Styles
		- Colors - ulo�en� hex. k�d� barev do promenn�ch 
		- Styles - styly v�ech stavebn�ch prvk� app (Buttons, Pickers, Frames, Labels...)
		- Definujeme si v�e na jednom m�st�, co� u�et�� dost ��dk� v XAMLu


### AppShell
- Soubor slou�� pro implementaci navigace

### AppShell
- 


## Debugging
- Emul�tor nebo fyzick� za��zen�
- USB nebo Wifi

### Aktivace bezdr�tov�ho debuggingu
Nastaven� -> v�voja�sk� mo�nosti (nutn� altivovat: Informace o softwaru -> 7x kliknou na ��slo sestaven�) -> Lad�n�
- bezdr�tov� lad�n�: (telefon) Android >= 11, (VS) Android SDK >= 30
	- ve VS otev��t Android Adb Command Prompt
```bash
adb pair [IP:port (�daje v telefonu)] 

Enter pairing code: 

adb connect [IP telefonu]
```