# Yummy Cook
Debugging
-Emul�tor nebo fyzick� za��zen�
-USB nebo Wifi
-aktivace: Nastaven� -> v�voja�sk� mo�nosti (nutn� altivovat: Informace o softwaru -> 7x kliknou na ��slo sestaven�) -> Lad�n�
-bezdr�tov� lad�n�: (telefon) Android >= 11, (VS) Android SDK >= 30
	-ve VS otev��t Android Adb Command Prompt
	-> adb pair [IP adresa:port (�daje v telefonu)] 
	-> pairing code
	-> adb connect [IP telefonu]

Struktura projektu
-yammyCook.csproj
	-z�kladn� nastaven� app (jm�no, verze, ikony, splashScreen, fonty)
-Platforms
	-specifick� k�d pro dan� platformy
-Resources
	-obsahuje v�echny soubory pou�it� v app (fotky, splashScreen, STYLY, fonty, ikony)
	-Styles
		-Colors - ulo�en� hex. k�d� barev do promenn�ch 
		-Styles - styly v�ech stavebn�ch prvk� app (Buttons, Pickers, Frames, Labels...)
		-Definujeme si v�e na jednom m�st� co� u�et�� dost ��dk� v XAMLu