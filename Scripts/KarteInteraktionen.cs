using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Steuert die Kamera, die auf die Karte gerichtet ist, indem es die Touch-Eingabe auswertet
public class KarteInteraktionen : MonoBehaviour
{
    public Vector3 campos;
    public float dist, prevdist;

    // Start is called before the first frame update
    void Start()
    {
        campos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Verschieben der Kamera funktioniert nur, wenn der User sich im Kamera-Modus befindet
        if (GetComponent<Camera>().targetTexture == null && GameObject.Find("Scroll View").GetComponent<UnityEngine.UI.Image>().enabled == false && GameObject.Find("Chrono Scroll View").GetComponent<UnityEngine.UI.Image>().enabled == false)
        {
            //Wenn sich nur ein Finger auf dem Bildschirm befindet, wird die Kamera entsprechend der Bewegung des Fingers bewegt.
            if (Input.touchCount == 1)
            {
                if (Input.touches[0].phase == TouchPhase.Moved) 
                {
                    //Die Bewegung des Fingers wird nicht 1zu1 übertragen, sondern in Abhängigkeit des Zooms angepasst (Kamera bewegt sich langsamer, wenn sie nah rangezoomt ist)
                    campos = new Vector3(campos.x - Input.touches[0].deltaPosition.x / (10f-(campos.y-249)/40), campos.y , campos.z - Input.touches[0].deltaPosition.y / (10f - (campos.y - 249) / 40));

                    //Das Bewegungsfeld der Kamera wird auf bestimmte Grenzen eingeschränkt
                    if (campos.x > 5300) campos.x = 5300;
                    else if (campos.x < 5050) campos.x = 5050;
                    if (campos.z > 420) campos.z = 420;
                    else if (campos.z < 100) campos.z = 100;
                    
                }
            }

            //wenn sich mehr als ein Finger auf dem Bildschirm befindet, wird die Kamera auf Basis der sich verändernden Distanz zwischen den Fingern nach oben oder unten bewegt.
            if (Input.touchCount > 1)
            {
                //Zu Beginn des Touches wird die Distanz zwischen den Fingern gespeichert (Berechnung mit Satz des Pythagoras)
                if (Input.touches[1].phase == TouchPhase.Began)
                {
                    prevdist = Mathf.Sqrt(Mathf.Pow((Input.touches[0].position.x - Input.touches[1].position.x), 2) + Mathf.Pow((Input.touches[0].position.y - Input.touches[1].position.y), 2));
                }
                else if (Input.touches[1].phase == TouchPhase.Moved)
                {
                    dist = Mathf.Sqrt(Mathf.Pow((Input.touches[0].position.x - Input.touches[1].position.x), 2) + Mathf.Pow((Input.touches[0].position.y - Input.touches[1].position.y), 2));

                    //Wenn sich die Distanz zwischen den Fingern verändert, wird die Kamera entsprechend bewegt
                    if (dist != prevdist)
                    {
                        campos.y = campos.y - (dist - prevdist);

                        if (campos.y > 450) campos.y = 450;
                        else if (campos.y < 250) campos.y = 250;
                    }
                    prevdist = dist;
                }
            }

            transform.position = campos;
        }
    }
}
