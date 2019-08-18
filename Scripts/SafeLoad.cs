using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SafeLoad
{
    public static Spielstand spielstand;

    //Save() erstellt ein Objekt vom Typ Spielstand und speichert es in einer externen Datei
    public static void Save()
    {
        spielstand = new Spielstand();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGame.sav");
        bf.Serialize(file, spielstand);
        file.Close();
    }

    //Load() liest den Spielstand aus der externen Datei ein
    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGame.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGame.sav", FileMode.Open);
            Debug.Log(Application.persistentDataPath);
            spielstand = (Spielstand)bf.Deserialize(file);
            file.Close();

            //sofern bereits Orte gefunden wurden, werden diese Orte als gefunden markiert und die entsprechenden Bereiche auf der Karte werden entfernt.
            if (spielstand.gefundeneOrte.Count > 0)
            {
                for (int i=0; i < spielstand.gefundeneOrte.Count; i++)
                {
                    GameObject.Find(spielstand.gefundeneOrte[i]).GetComponent<DefaultTrackableEventHandler>().isFound = true;
                    Object.Destroy(GameObject.Find(spielstand.gefundeneOrte[i]).GetComponent<DefaultTrackableEventHandler>().interaktivesZiel);
                }
            }

            //Wenn es bereits einen Spielstand gab, wird der Kamera-Modus aufgerufen. Dadurch wird der Willkommens-Dialog nur dann angezeigt, wenn die App das erste Mal geöffnet wird, es also noch keinen Spielstand gab.
            GameObject.Find("Canvas").GetComponent<AppMenu>().ARCamMode();
        }
    }
}
