/*==============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using Vuforia;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
///
/// Changes made to this file could be overwritten when upgrading the Vuforia version.
/// When implementing custom event handler behavior, consider inheriting from this class instead.
/// </summary>
/// 

//Modifiziertes Skript von Vuforia: Zeigt die Animation und den entsprechenden Text an, wenn ein Objekt gefunden wurde.
//Skript ist auf jedem Image-Target, deshalb ist der Code so flexibel wie moeglich gehalten.
public class DefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    #region PROTECTED_MEMBER_VARIABLES

    [TextArea(3, 10)]  public string Ueberschrift, Teaser, Artikel1, Artikel2;
    public Texture HeuteBild, OptBild;
    public bool isFound;
    public GameObject interaktivesZiel;
    public RenderTexture MiniMapBild;

    public Texture2D[] frames;

    private bool playAni=false;
    private int index;

    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;

    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        
    }


    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    /// 

    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound();

            //Wenn ein Objekt gefunden wurde, Vibriert das Handy und zeigeAnimation() wird aufgerufen, außerdem wird gespeichert
            Handheld.Vibrate();
            zeigeAnimation();

        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    //Startet die Animation, nach einer Sekunde Delay wird der Text angezeigt
    void zeigeAnimation()
    {
        GameObject.Find("BlitzIcon").GetComponent<RawImage>().enabled = true;
        GameObject.Find("ARCamera").GetComponent<Camera>().enabled = true;
        GameObject.Find("MiniMap").GetComponent<RawImage>().enabled = true;
        GameObject.Find("KameraIcon").GetComponent<RawImage>().enabled = false;
        GameObject.Find("ARCamera").GetComponent<Camera>().targetTexture = null;
        GameObject.Find("KarteCamera").GetComponent<Camera>().targetTexture = MiniMapBild;
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

        transform.Find("Video").GetComponent<Renderer>().enabled = true;
        playAni = true;
        index = 0;
    }


    void Update()
    {
        //hier wird die Animation (bestehend aus einzelnen Frames in einem Array) abgespielt
        if (playAni == true)
        {
            transform.Find("Video").GetComponent<Renderer>().material.mainTexture = frames[index];
            if (index == frames.Length - 1 || transform.Find("Video").GetComponent<Renderer>().enabled == false) {
                playAni = false;
                transform.Find("Video").GetComponent<Renderer>().enabled = false;
                zeigeText();
            }
            index++;
        }
    }

    //zeigt den Text zum gefunden Ort an
    public void zeigeText()
    {
        if (Ueberschrift != "")
        {
            GameObject.Find("Ueberschrift").GetComponent<Text>().text = Ueberschrift;
            GameObject.Find("Ueberschrift").GetComponent<Text>().enabled = true;
        }
        else GameObject.Find("Ueberschrift").GetComponent<Text>().enabled = false;

        if (Teaser != "")
        {
            GameObject.Find("Teaser").GetComponent<Text>().text = Teaser;
            GameObject.Find("Teaser").GetComponent<Text>().enabled = true;
        }
        else GameObject.Find("Teaser").GetComponent<Text>().enabled = false;

        if (Artikel1 != "")
        {
            GameObject.Find("Artikel1").GetComponent<Text>().text = Artikel1;
            GameObject.Find("Artikel1").GetComponent<Text>().enabled = true;
        }
        else GameObject.Find("Artikel1").GetComponent<Text>().enabled = false;

        if (Artikel2 != "")
        {
            GameObject.Find("Artikel2").GetComponent<Text>().text = Artikel2;
            GameObject.Find("Artikel2").GetComponent<Text>().enabled = true;
        }
        else GameObject.Find("Artikel2").GetComponent<Text>().enabled = false;

        if (HeuteBild != null)
        {
            GameObject.Find("HeuteBild").GetComponent<RawImage>().texture = HeuteBild;
            GameObject.Find("HeuteBild").GetComponent<RawImage>().enabled = true;
            GameObject.Find("HeuteBild").GetComponent<LayoutElement>().enabled = true;
        }
        else
        {
            GameObject.Find("HeuteBild").GetComponent<RawImage>().texture = null;
            GameObject.Find("HeuteBild").GetComponent<RawImage>().enabled = false;
            GameObject.Find("HeuteBild").GetComponent<LayoutElement>().enabled = false;
        }

        if (OptBild != null)
        {
            GameObject.Find("OptBild").GetComponent<RawImage>().texture = OptBild;
            GameObject.Find("OptBild").GetComponent<RawImage>().enabled = true;
            GameObject.Find("OptBild").GetComponent<LayoutElement>().enabled = true;
        }
        else
        {
            GameObject.Find("OptBild").GetComponent<RawImage>().texture = null;
            GameObject.Find("OptBild").GetComponent<RawImage>().enabled = false;
            GameObject.Find("OptBild").GetComponent<LayoutElement>().enabled = false;
        }

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

        //Ort wird als gefunden markiert, der farbige Bereich auf der Karte wird gelöscht und der nächste Bereich wird grün gefärbt
        isFound = true;
        Destroy(interaktivesZiel);
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

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    protected virtual void OnTrackingFound()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Enable rendering:
        foreach (var component in rendererComponents)
            component.enabled = true;

        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = true;

        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;
    }


    protected virtual void OnTrackingLost()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Disable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;
    }

    #endregion // PROTECTED_METHODS
}
