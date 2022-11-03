# Yummy Cook
Debugging
-Emulátor nebo fyzické zaøízení
-USB nebo Wifi
-aktivace: Nastavení -> vývojaøské možnosti (nutné altivovat: Informace o softwaru -> 7x kliknou na èíslo sestavení) -> Ladìní
-bezdrátové ladìní: (telefon) Android >= 11, (VS) Android SDK >= 30
	-ve VS otevøít Android Adb Command Prompt
	-> adb pair [IP adresa:port (údaje v telefonu)] 
	-> pairing code
	-> adb connect [IP telefonu]

Struktura projektu
-yammyCook.csproj
	-základní nastavení app (jméno, verze, ikony, splashScreen, fonty)
-Platforms
	-specifický kód pro dané platformy
-Resources
	-obsahuje všechny soubory použité v app (fotky, splashScreen, STYLY, fonty, ikony)
	-Styles
		-Colors - uložení hex. kódù barev do promenných 
		-Styles - styly všech stavebních prvkù app (Buttons, Pickers, Frames, Labels...)
		-Definujeme si vše na jednom místì což ušetøí dost øádkù v XAMLu