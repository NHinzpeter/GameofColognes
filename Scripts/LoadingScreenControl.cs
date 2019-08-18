using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Der Ladescreen ist die erste Szene die aufgerufen wird. Sobald ein kurzer Delay abgelaufen ist, wird das eigentliche Spiel geladen.
public class LoadingScreenControl : MonoBehaviour
{

    public GameObject loadingScreenObj;
    public Slider slider;
    private int delay;

    AsyncOperation async;


    //Läd das Spiel und überträgt den aktuellen Fortschritt auf den Ladebalken
    IEnumerator LoadingScreen()
    {
        yield return new WaitForSeconds(1);
        loadingScreenObj.SetActive(true);
        async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;

        while (async.isDone == false)
        {
            slider.value = async.progress;
            if (async.progress == 0.9f)
            {
                slider.value = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    void Start()
    {
        delay = 50;
        StartCoroutine(LoadingScreen());
    }
    
    
}
