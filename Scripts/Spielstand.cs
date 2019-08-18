using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Die Klasse "Spielstand" enthält eine Liste aller bislang gefundenen Orte. Diese Klasse wird in SafeLoad.cs genutzt, um den aktuellen Spielstand zu laden bzw zu speichern.

[System.Serializable]
public class Spielstand
{
    public List<string> gefundeneOrte;

    public Spielstand()
    {
        gefundeneOrte = new List<string>();
        int i = 0;

        foreach (Transform child in GameObject.Find("ImageTargets").transform)
        {
            if (child.GetComponent<DefaultTrackableEventHandler>().isFound == true)
            {
                gefundeneOrte.Insert(i, child.name);
            }
        }
    }
}
