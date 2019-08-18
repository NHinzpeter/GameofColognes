using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//AppMenu.cs verwaltet die UI-Elemente
public class AppMenu : MonoBehaviour
{

    public Button K, OK, Chrono;
    public Camera ARCam, Cam;
    public RenderTexture ARCamBild, MiniMapBild;
    public Texture Blitzon, Blitzoff;
    public Button BlitzButton;
    public float ChronoY, ChronoH;

    private bool blitz = false;
    // Start is called before the first frame update
    void Start()
    {
        //Initialisierung der UI Buttons
        K.onClick.AddListener(KOnClick);
        OK.onClick.AddListener(OKOnClick);
        Chrono.onClick.AddListener(ChronoOnClick);
        BlitzButton.onClick.AddListener(BlitzButtonOnClick);


        //Einführung bei Appstart:
        GameObject.Find("Einleitung").GetComponent<DefaultTrackableEventHandler>().zeigeText();

        //Laden des aktuellen Spielstands bei Appstart, die Funktion ist in SafeLoad.cs zu finden
        SafeLoad.Load();
        SafeLoad.Save();

        //Hier wird das Areal auf der Karte grün eingefärbt, das entsprechend der Chronologie als nächstes zu finden ist
        //Die ImageTargets sind in Unity der Chronologie nach geordnet, das erste Target, das noch nicht gefunden wurde, wird grün gefärbt und die Schleife bricht ab
        foreach (Transform child in GameObject.Find("ImageTargets").transform)
        {
            if (child.GetComponent<DefaultTrackableEventHandler>().isFound != true)
            {
                if (child.GetComponent<DefaultTrackableEventHandler>().interaktivesZiel != null)
                {
                    child.GetComponent<DefaultTrackableEventHandler>().interaktivesZiel.GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 1f, 0f, 0.35f);
                    break;
                }
            }
        }

    }


    void Update()
    {
        //Implementation der "Zurück"-Taste für Android
            if (Input.GetKeyDown(KeyCode.Escape))
            {

            //Wenn das "X" in der Ecke zu sehen ist, wird ein Klick auf dieses simuliert, wenn das Chronologie-Menü geöffnet ist, wird es geschlossen, ansonsten wird der Kamera-Modus aufgerufen
            if (OK.enabled) OKOnClick();
            else if (GameObject.Find("Chrono Scroll View").GetComponent<UnityEngine.UI.Image>().enabled) ChronoOnClick();
            else ARCamMode();


        }

        //Blitz wird an oder ausgeschaltet
        if (blitz) Vuforia.CameraDevice.Instance.SetFlashTorchMode(true);
        else Vuforia.CameraDevice.Instance.SetFlashTorchMode(false);
        

    }

    //Ein Klick auf den Button unten rechts wechselt zwischen Kamera- und Karten-Modus
    void KOnClick()
    {
        if (Cam.targetTexture != null)
        {
            MapMode();
        }
        else
        {
            ARCamMode();
        }
    }

    //Das "X" oben rechts schließt den Info-Modus und geht in den entsprechenden Modus, der sich "hinter" dem Text befindet
    void OKOnClick()
    {
        if (GameObject.Find("Chrono Scroll View").GetComponent<UnityEngine.UI.Image>().enabled == true) ChronoMode();
        else if (Cam.targetTexture != null) ARCamMode();
        else MapMode();
    }

    //Ein Klick auf den Menü-Button öffnet bzw schließt das Chronologie-Menü
    void ChronoOnClick()
    {
        if (GameObject.Find("Chrono Scroll View").GetComponent<UnityEngine.UI.Image>().enabled != true)
        {
            ChronoY = 0.0f;
            ChronoMode();
        }
        else if (Cam.targetTexture != null) ARCamMode();
        else MapMode();
    }

    //schaltet den Blitz an oder aus
    private void BlitzButtonOnClick()
    {
        if (blitz)
        {
            blitz = false;
            GameObject.Find("BlitzIcon").GetComponent<RawImage>().texture = Blitzon;
        }
        else
        {
            blitz = true;
            GameObject.Find("BlitzIcon").GetComponent<RawImage>().texture = Blitzoff;
        }
    }

    //stellt alle UI-Elemente so ein, wie sie im Kamera-Modus sein sollen
    public void ARCamMode()
    {
        GameObject.Find("BlitzIcon").GetComponent<RawImage>().enabled = true;
        ARCam.enabled = true;
        GameObject.Find("MiniMap").GetComponent<RawImage>().enabled = true;
        GameObject.Find("KameraIcon").GetComponent<RawImage>().enabled = false;
        ARCam.targetTexture = null;
        Cam.targetTexture = MiniMapBild;
        GameObject.Find("OKButtonImage").GetComponent<RawImage>().enabled = false;
        GameObject.Find("OKButton").GetComponent<Button>().enabled = false;
        GameObject.Find("Scroll View").GetComponent<UnityEngine.UI.Image>().enabled = false;
        GameObject.Find("Chrono Scroll View").GetComponent<UnityEngine.UI.Image>().enabled = false;
        GameObject.Find("InfoIcon").GetComponent<RawImage>().enabled = true;
        GameObject.Find("Info").GetComponent<Button>().enabled = true;
        GameObject.Find("EinstellungenIcon").GetComponent<RawImage>().enabled = true;
        GameObject.Find("Einstellungen").GetComponent<Button>().enabled = true;
        GameObject.Find("MiniMap").GetComponent<RawImage>().enabled = true;
        GameObject.Find("KartenButton").GetComponent<Button>().enabled = true;
        GameObject.Find("Ueberschrift").GetComponent<Text>().enabled = false;
        GameObject.Find("Teaser").GetComponent<Text>().enabled = false;
        GameObject.Find("Artikel1").GetComponent<Text>().enabled = false;
        GameObject.Find("Artikel2").GetComponent<Text>().enabled = false;
        GameObject.Find("HeuteBild").GetComponent<RawImage>().enabled = false;
        GameObject.Find("HeuteBild").GetComponent<LayoutElement>().enabled = false;
        GameObject.Find("OptBild").GetComponent<RawImage>().enabled = false;
        GameObject.Find("OptBild").GetComponent<LayoutElement>().enabled = false;
        GameObject.Find("ChronoUeberschrift").GetComponent<Text>().enabled = false;
        foreach (Transform child in GameObject.Find("Chronologie").transform)
        {
            if (child.name != "ChronoUeberschrift")
            {
                child.GetComponent<Button>().enabled = false;
                child.GetComponent<RawImage>().enabled = false;
                child.GetComponent<LayoutElement>().enabled = false;
                child.Find("RawImage").GetComponent<RawImage>().enabled = false;
                child.Find("Text").GetComponent<Text>().enabled = false;
            }
        }
    }

    //stellt alle UI-Elemente so ein, wie sie im Karten-Modus sein sollen
    public void MapMode()
    {
        GameObject.Find("BlitzIcon").GetComponent<RawImage>().enabled = false;
        GameObject.Find("MiniMap").GetComponent<RawImage>().enabled = false;
        GameObject.Find("KameraIcon").GetComponent<RawImage>().enabled = true;
        ARCam.targetTexture = ARCamBild;
        Cam.targetTexture = null;
        GameObject.Find("OKButtonImage").GetComponent<RawImage>().enabled = false;
        GameObject.Find("OKButton").GetComponent<Button>().enabled = false;
        GameObject.Find("Scroll View").GetComponent<UnityEngine.UI.Image>().enabled = false;
        GameObject.Find("Chrono Scroll View").GetComponent<UnityEngine.UI.Image>().enabled = false;
        GameObject.Find("InfoIcon").GetComponent<RawImage>().enabled = true;
        GameObject.Find("Info").GetComponent<Button>().enabled = true;
        GameObject.Find("EinstellungenIcon").GetComponent<RawImage>().enabled = true;
        GameObject.Find("Einstellungen").GetComponent<Button>().enabled = true;
        GameObject.Find("KartenButton").GetComponent<Button>().enabled = true;
        GameObject.Find("Ueberschrift").GetComponent<Text>().enabled = false;
        GameObject.Find("Teaser").GetComponent<Text>().enabled = false;
        GameObject.Find("Artikel1").GetComponent<Text>().enabled = false;
        GameObject.Find("Artikel2").GetComponent<Text>().enabled = false;
        GameObject.Find("HeuteBild").GetComponent<RawImage>().enabled = false;
        GameObject.Find("HeuteBild").GetComponent<LayoutElement>().enabled = false;
        GameObject.Find("OptBild").GetComponent<RawImage>().enabled = false;
        GameObject.Find("OptBild").GetComponent<LayoutElement>().enabled = false;
        GameObject.Find("GesuchtesBild").GetComponent<RawImage>().enabled = false;
        GameObject.Find("GesuchtesBild1").GetComponent<RawImage>().enabled = false;
        GameObject.Find("ChronoUeberschrift").GetComponent<Text>().enabled = false;
        foreach (Transform child in GameObject.Find("Chronologie").transform)
        {
            if (child.name != "ChronoUeberschrift")
            {
                child.GetComponent<Button>().enabled = false;
                child.GetComponent<RawImage>().enabled = false;
                child.GetComponent<LayoutElement>().enabled = false;
                child.Find("RawImage").GetComponent<RawImage>().enabled = false;
                child.Find("Text").GetComponent<Text>().enabled = false;
            }
        }
    }

    //stellt alle UI-Elemente so ein, wie sie im Info-Modus sein sollen
    public void InfoMode()
    {
        GameObject.Find("BlitzIcon").GetComponent<RawImage>().enabled = false;
        GameObject.Find("OKButtonImage").GetComponent<RawImage>().enabled = true;
        GameObject.Find("OKButton").GetComponent<Button>().enabled = true;
        GameObject.Find("Scroll View").GetComponent<UnityEngine.UI.Image>().enabled = true;
        GameObject.Find("InfoIcon").GetComponent<RawImage>().enabled = false;
        GameObject.Find("Info").GetComponent<Button>().enabled = false;
        GameObject.Find("EinstellungenIcon").GetComponent<RawImage>().enabled = false;
        GameObject.Find("Einstellungen").GetComponent<Button>().enabled = false;
        GameObject.Find("MiniMap").GetComponent<RawImage>().enabled = false;
        GameObject.Find("KameraIcon").GetComponent<RawImage>().enabled = false;
        GameObject.Find("KartenButton").GetComponent<Button>().enabled = false;
        GameObject.Find("ChronoUeberschrift").GetComponent<Text>().enabled = false;
        foreach (Transform child in GameObject.Find("Chronologie").transform)
        {
            if (child.name != "ChronoUeberschrift")
            {
                child.GetComponent<Button>().enabled = false;
                child.GetComponent<RawImage>().enabled = false;
                child.GetComponent<LayoutElement>().enabled = false;
                child.Find("RawImage").GetComponent<RawImage>().enabled = false;
                child.Find("Text").GetComponent<Text>().enabled = false;
            }
        }
    }

    //stellt alle UI-Elemente so ein, wie sie im Chronologie-Modus sein sollen
    public void ChronoMode()
    {
        GameObject.Find("BlitzIcon").GetComponent<RawImage>().enabled = false;
        GameObject.Find("OKButtonImage").GetComponent<RawImage>().enabled = false;
        GameObject.Find("OKButton").GetComponent<Button>().enabled = false;
        GameObject.Find("Chrono Scroll View").GetComponent<UnityEngine.UI.Image>().enabled = true;
        GameObject.Find("InfoIcon").GetComponent<RawImage>().enabled = true;
        GameObject.Find("Info").GetComponent<Button>().enabled = true;
        GameObject.Find("EinstellungenIcon").GetComponent<RawImage>().enabled = true;
        GameObject.Find("Einstellungen").GetComponent<Button>().enabled = true;
        GameObject.Find("MiniMap").GetComponent<RawImage>().enabled = false;
        GameObject.Find("KameraIcon").GetComponent<RawImage>().enabled = false;
        GameObject.Find("KartenButton").GetComponent<Button>().enabled = false;
        GameObject.Find("ChronoUeberschrift").GetComponent<Text>().enabled = true;
        GameObject.Find("Ueberschrift").GetComponent<Text>().enabled = false;
        GameObject.Find("Teaser").GetComponent<Text>().enabled = false;
        GameObject.Find("Artikel1").GetComponent<Text>().enabled = false;
        GameObject.Find("Artikel2").GetComponent<Text>().enabled = false;
        GameObject.Find("HeuteBild").GetComponent<RawImage>().enabled = false;
        GameObject.Find("HeuteBild").GetComponent<LayoutElement>().enabled = false;
        GameObject.Find("OptBild").GetComponent<RawImage>().enabled = false;
        GameObject.Find("OptBild").GetComponent<LayoutElement>().enabled = false;
        GameObject.Find("GesuchtesBild").GetComponent<RawImage>().enabled = false;
        GameObject.Find("GesuchtesBild1").GetComponent<RawImage>().enabled = false;

        foreach (Transform child in GameObject.Find("Chronologie").transform)
        {
            if (child.name != "ChronoUeberschrift")
            {
                child.GetComponent<Chronologie>().LoadImage();
                
            }
        }

        //Stellt das Sichtfenster wieder auf die vorherige Höhe ein
        GameObject.Find("Chronologie").GetComponent<RectTransform>().sizeDelta = new Vector2(GameObject.Find("Chronologie").GetComponent<RectTransform>().sizeDelta.x, ChronoH);
        GameObject.Find("Chronologie").GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, ChronoY);
    }
}
