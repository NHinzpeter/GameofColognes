using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Verwaltet den "Hilfe"-Button
public class Hilfe : MonoBehaviour
{
    private Button Info;
    [TextArea(3, 10)] public string ARModeUeberschrift, ARModeText, KarteModeUeberschrift, KarteModeText, ChronoModeUeberschrift, ChronoModeText;

    // Start is called before the first frame update
    void Start()
    {
        Info = GetComponent<Button>();
        Info.onClick.AddListener(InfoOnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void InfoOnClick()
    {
        Help();
    }

    //Wenn der Button gedrückt wird, wird in Abhängigkeit des Modus ein entsprechender Hilfetext angezeigt.
    void Help()
    {
        if (GameObject.Find("Chrono Scroll View").GetComponent<UnityEngine.UI.Image>().enabled == true)
        {
            if (ChronoModeUeberschrift != "")
            {
                GameObject.Find("Ueberschrift").GetComponent<Text>().text = ChronoModeUeberschrift;
                GameObject.Find("Ueberschrift").GetComponent<Text>().enabled = true;
            }
            else GameObject.Find("Ueberschrift").GetComponent<Text>().enabled = false;

            if (ChronoModeText != "")
            {
                GameObject.Find("Artikel1").GetComponent<Text>().text = ChronoModeText;
                GameObject.Find("Artikel1").GetComponent<Text>().enabled = true;
            }
            else GameObject.Find("Artikel1").GetComponent<Text>().enabled = false;
            GameObject.Find("Canvas").GetComponent<AppMenu>().InfoMode();
            GameObject.Find("Scroll View").GetComponent<UnityEngine.UI.Image>().enabled = false;
            GameObject.Find("Chrono Scroll View").GetComponent<UnityEngine.UI.Image>().enabled = true;
        }

        else if (GameObject.Find("KarteCamera").GetComponent<Camera>().targetTexture != null)
        {
            if (ARModeUeberschrift != "")
            {
                GameObject.Find("Ueberschrift").GetComponent<Text>().text = ARModeUeberschrift;
                GameObject.Find("Ueberschrift").GetComponent<Text>().enabled = true;
            }
            else GameObject.Find("Ueberschrift").GetComponent<Text>().enabled = false;

            if (ARModeText != "")
            {
                GameObject.Find("Artikel1").GetComponent<Text>().text = ARModeText;
                GameObject.Find("Artikel1").GetComponent<Text>().enabled = true;
            }
            else GameObject.Find("Artikel1").GetComponent<Text>().enabled = false;
            GameObject.Find("Canvas").GetComponent<AppMenu>().InfoMode();
        }

        else
        {
            if (KarteModeUeberschrift != "")
            {
                GameObject.Find("Ueberschrift").GetComponent<Text>().text = KarteModeUeberschrift;
                GameObject.Find("Ueberschrift").GetComponent<Text>().enabled = true;
            }
            else GameObject.Find("Ueberschrift").GetComponent<Text>().enabled = false;

            if (KarteModeText != "")
            {
                GameObject.Find("Artikel1").GetComponent<Text>().text = KarteModeText;
                GameObject.Find("Artikel1").GetComponent<Text>().enabled = true;
            }
            else GameObject.Find("Artikel1").GetComponent<Text>().enabled = false;
            GameObject.Find("Canvas").GetComponent<AppMenu>().InfoMode();
        }

    }

}