using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//steuert die einzelnen Abschnitte im Chronologie-Menü
public class Chronologie : MonoBehaviour
{
    public GameObject ImageTarget;
    public Texture known, unknown;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(showInfo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //läd das entsprechende Bild: entweder das Fragzeichen, sofern der Ort noch nicht gefunden wurde, oder das tatsächliche Bild und der Text darauf
    public void LoadImage()
    {
        if (ImageTarget.GetComponent<DefaultTrackableEventHandler>().isFound == true)
        {
            GetComponent<RawImage>().texture = known;
            GetComponent<Button>().enabled = true;
            transform.Find("RawImage").GetComponent<RawImage>().enabled = true;
            transform.Find("Text").GetComponent<Text>().enabled = true;
        }
        else
        {
            GetComponent<RawImage>().texture = unknown;
        }
        GetComponent<RawImage>().enabled = true;
        GetComponent<LayoutElement>().enabled = true;
    }

    //Wenn auf den Abschnitt getippt wird, wird der Text des Ortes angezeigt.
    void showInfo()
    {
        //Die Höhe des Sichtfensters wird gespeichert, damit sie nach dem Schließen des Textes wiederhergestellt werden kann
        GameObject.Find("Canvas").GetComponent<AppMenu>().ChronoY = GameObject.Find("Chronologie").GetComponent<RectTransform>().anchoredPosition.y;
        GameObject.Find("Canvas").GetComponent<AppMenu>().ChronoH = GameObject.Find("Chronologie").GetComponent<RectTransform>().sizeDelta.y;

        ImageTarget.GetComponent<DefaultTrackableEventHandler>().zeigeText();
        GameObject.Find("Scroll View").GetComponent<UnityEngine.UI.Image>().enabled = false;
        GameObject.Find("Chrono Scroll View").GetComponent<UnityEngine.UI.Image>().enabled = true;
    }
}
