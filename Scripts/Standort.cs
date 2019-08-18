using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Standort.cs ermittelt die GPS-Koordinaten des Handys und transformiert diese auf die Karte in Unity
//Hierzu werden 2 Punkte (pos1 und pos2) zur Georeferenzierung genutzt. "pos1rl" und "pos1map" sind die exakt gleichen Orte, einmal als Koordinaten in der echten Welt und einmal als Koordinaten in Unity auf der Karte
public class Standort : MonoBehaviour
{
    private float lat = 0, lon = 0;
    public Vector2 pos1rl, pos2rl, pos1map, pos2map;
    private float distxrl, distyrl, distxmap, distymap;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        distxmap = pos2map.x - pos1map.x;
        distymap = pos2map.y - pos1map.y;
        distxrl = pos2rl.x - pos1rl.x;
        distyrl = pos2rl.y - pos1rl.y;


    }

    // Update is called once per frame
    void Update()
    {
        //Start des Standortservices
        if (Input.location.status != LocationServiceStatus.Initializing && Input.location.status != LocationServiceStatus.Running && Input.location.isEnabledByUser==true)
        {
            Input.location.Start(0.1f, 0.1f);
        }

        //Aktualisierung der Koordinaten
        if (Input.location.status == LocationServiceStatus.Running)
        {
            lat = Input.location.lastData.latitude;
            lon = Input.location.lastData.longitude;
        }

        //Hinweis in Textform, wenn GPS deaktiviert ist
        if (!Input.location.isEnabledByUser && GameObject.Find("KarteCamera").GetComponent<Camera>().targetTexture == null && GameObject.Find("Scroll View").GetComponent<UnityEngine.UI.Image>().enabled != true && GameObject.Find("Chrono Scroll View").GetComponent<UnityEngine.UI.Image>().enabled != true)
        {
            GameObject.Find("ContentImage").GetComponent<RawImage>().enabled = true;
            GameObject.Find("Content").GetComponent<Text>().text = ("Aktiviere GPS, um deinen Standort zu sehen");
            GameObject.Find("ContentText").GetComponent<Text>().text = ("Aktiviere GPS, um deinen Standort zu sehen");
        }
        else
        {

            GameObject.Find("ContentImage").GetComponent<RawImage>().enabled = false;
            GameObject.Find("Content").GetComponent<Text>().text = ("");
            GameObject.Find("ContentText").GetComponent<Text>().text = ("");
        }


        //Transformation auf die Karte:
        if (Input.location.status == LocationServiceStatus.Running && Input.location.isEnabledByUser == true && lat>pos2rl.x && lat < pos1rl.x && lon>pos1rl.y && lon < pos2rl.y)
        {
            //Georeferenzierung: Berechnet die Koordinaten auf der Karte in Unity, die den GPS-Daten entsprächen
            player.transform.position = new Vector3((lon - pos1rl.y) / distyrl * distxmap + pos1map.x, 88f, (lat - pos1rl.x) / distxrl * distymap + pos1map.y);  //normalisiert die GPS-Latitude auf einen Wert zwischen 0 und 1, multipliziert ihn dann mit der Distanz auf der Karte und addiert diese Distanz dann auf den entsprechenden Wert von pos1
        }
        else player.transform.position = new Vector3(0, 0, 0);
    }
}
