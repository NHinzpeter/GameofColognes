# GameofColognes
Historische Stadtführung durch Köln in AR<br>
<br>
<h2>Dokumentation</h2>
Historischer Hintergrund/Recherche<br>
<br>
<br>
<h2>Design</h2>
<br>
<br>
<h2>Technische Umsetzung</h2>
Als Entwicklungsumgebung wurde Unity verwendet. Für die Einbindung der Karte wurde zusätzlich die Erweiterung Mapbox und für die Objekterkennung Vuforia verwendet. Alles weitere wurde eigenhändig implementiert.<br>
Viel Arbeit ist hierbei in den Kartenmodus geflosen: Sowohl die Steuerung der Karte via Toucheingabe, als auch die Anzeige des Standorts des Spielers, sowie die farbigen, aklickbaren Bereiche auf der Karte wurden manuell implementiert.<br>
Insgesamt wurde bei der Implementierung viel Wert auf eine möglichst flexible Programmierung gelegt. So wurden keine Inhalte hardgecodet, der Inhalt der App (wie Texte, Bilder oder ganze Stationen) ist entsprechend in Unity schnell und einfach auszutauschen. Dies ermöglichte einen flexiblen Entwicklungsprozess.<br>
Auch das Spielerlebnis ist flexibel gehalten: Es existiert zwar grundsätzlich eine festgelegte Chronologie, die Spielenden müssen sich jedoch nicht zwangsläufig an diese halten. Um dennoch einen Überblick zu bewahren und Texte immer wieder aufrufen zu können, wurde ein "Chronologie-Menü" entwickelt, welches die Stationen in ihrer korrekten Reihenfolge darstellt.<br>
Um den Spielenden ein belohnendes Gefühl zu vermitteln, wenn eine Station gefunden wurde, wurde eine Animation und eine Vibration eingefügt.<br>
Da die Ladezeit beim Öffnen der App relativ lang ist, wurde ein Loadingscreen eingebaut, der den aktuellen Ladefortschritt in einem Balken anzeigt.<br>
Innerhalb des Spiels finden die Spielenden nützliche Hinweise und Hilfestellungen im "Hilfe"-Menü.<br>
Im Kamera-Modus kann außerdem der Handy-interne Blitz eingeschaltet werden, um die gesuchten Objekte auch bei schlechten Lichtverhältnissen scannen zu können.<br>
Um die Steuerung so intuitiv wie möglich zu gestalten, wurde zusätzlich der native "Zurück"-Button der Android-Geräte mit Befehlen belegt.<br>
