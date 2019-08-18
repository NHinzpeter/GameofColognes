# GameofColognes
In "Game of Colognes" lernen die Spielenden die wichtigsten Schauorte der "Schlacht an der Ulrepforte" kennen und können so ganz neue Ecken von Köln entdecken. Mithilfe von AR müssen die Spielenden hierbei innerhalb von Köln nach Objekten suchen und diese einscannen, um die Geschichte Stück für Stück zu erfahren.<br>
<br>
<a href="https://drive.google.com/file/d/1DLZmCD4LhlJUlWQtBIJJ1tNweA9AoxTw/view?usp=sharing">Download der finalen Version</a><br>
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
Viel Arbeit ist hierbei in den Kartenmodus geflossen: Sowohl die Steuerung der Karte via Toucheingabe, als auch die Anzeige des Standorts des Spielers, sowie die farbigen, anklickbaren Bereiche auf der Karte wurden manuell implementiert.<br>
Insgesamt wurde bei der Implementierung viel Wert auf eine möglichst flexible Programmierung gelegt. So wurden keine Inhalte hardgecodet, der Inhalt der App (wie Texte, Bilder oder ganze Stationen) ist entsprechend in Unity schnell und einfach auszutauschen. Dies ermöglichte einen flexiblen Entwicklungsprozess.<br>
Auch das Spielerlebnis ist flexibel gehalten: Es existiert zwar grundsätzlich eine festgelegte Chronologie, die Spielenden müssen sich jedoch nicht zwangsläufig an diese halten. Um dennoch einen Überblick zu bewahren und Texte immer wieder aufrufen zu können, wurde ein "Chronologie-Menü" entwickelt, welches die Stationen in ihrer korrekten Reihenfolge darstellt.<br>
Zeitlich sind die Spielenden ebenfalls flexibel - die App speichert den Spielstand automatisch und ermöglicht so ein zeitversetztes Wiederaufnehmen der Spielsitzung.<br>
Um den Spielenden ein belohnendes Gefühl zu vermitteln, wenn eine Station gefunden wurde, wurde eine Animation und eine Vibration eingefügt.<br>
Da die Ladezeit beim Öffnen der App relativ lang ist, wurde ein Loadingscreen eingebaut, der den aktuellen Ladefortschritt in einem Balken anzeigt.<br>
Innerhalb des Spiels finden die Spielenden nützliche Hinweise und Hilfestellungen im "Hilfe"-Menü.<br>
Im Kamera-Modus kann außerdem der Handy-interne Blitz eingeschaltet werden, um die gesuchten Objekte auch bei schlechten Lichtverhältnissen scannen zu können.<br>
Um die Steuerung so intuitiv wie möglich zu gestalten, wurde zusätzlich der native "Zurück"-Button der Android-Geräte mit Befehlen belegt.<br>
Known issue: Auf leistungsschwachen Endgeräten kann es passieren, dass die App beim erstmaligen Öffnen einfriert. Verantwortlich dafür ist Vuforia, sodass wir hieran leider nichts ändern konnten. Sollte der Bug auftreten, muss die App einfach neu gestartet werden, der Einleitungstext kann dann erneut im Chronologie-Menü aufgerufen werden.<br>
<br>
<h2>Screenshots</h2><br>
<img src="./Screenshots/Screenshot_20190818-214953.png" height=500px><br>
<img src="./Screenshots/Screenshot_20190818-214924.png" height=500px><br>
<img src="./Screenshots/Screenshot_20190818-215443.png" height=500px><br>
<img src="./Screenshots/Screenshot_20190818-214738.png" height=500px><br>
