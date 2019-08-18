using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Verwaltet die "interaktiven Ziele", also die farbigen Bereiche auf der Karte. Jedes interaktive Ziel verfügt über ein eigenes Skript, deshalb ist der Code so flexibel wie möglich gehalten.
public class InteraktivesZiel : MonoBehaviour
{
    public Texture gesuchtesBild;
    public Button area;
    public float aspectRatiowh;
    public GameObject ImageTarget;

    void OnDestroy()
    {
        //speichert, wenn ein interaktives Ziel zerstört wird (sprich: wenn eine Station gefunden wurde)
        SafeLoad.Save();
        Debug.Log("Test");
    }

    // Start is called before the first frame update
    void Start()
    {
        //area = this.GetComponent<Button>();
        area.onClick.AddListener(AreaOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Wenn auf ein Ziel getippt wird, wird das entsprechende Bild angezeigt
    void AreaOnClick()
    {

        //basierend auf dem Seitenverhältnis des gesuchten Bildes wird eines der beiden Image-Elemente angezeigt. Diese entscheiden sich dadurch, ob sie sich in dier Höhe oder in die Breite ausdehnen.
        //Dadurch wird vermieden, dass die Bilder zu hoch oder zu breit für das Display sind
        if (aspectRatiowh > 0.66f)
        {
            GameObject.Find("GesuchtesBild").GetComponent<RawImage>().enabled = true;
            GameObject.Find("GesuchtesBild1").GetComponent<RawImage>().enabled = false;
            GameObject.Find("GesuchtesBild").GetComponent<RawImage>().texture = gesuchtesBild;
            GameObject.Find("GesuchtesBild").GetComponent<AspectRatioFitter>().aspectRatio = aspectRatiowh;
        }
        else
        {
            GameObject.Find("GesuchtesBild").GetComponent<RawImage>().enabled = false;
            GameObject.Find("GesuchtesBild1").GetComponent<RawImage>().enabled = true;
            GameObject.Find("GesuchtesBild1").GetComponent<RawImage>().texture = gesuchtesBild;
            GameObject.Find("GesuchtesBild1").GetComponent<AspectRatioFitter>().aspectRatio = aspectRatiowh;

        }

        GameObject.Find("Canvas").GetComponent<AppMenu>().InfoMode();
    }
}
