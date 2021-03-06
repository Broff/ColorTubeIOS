﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundEffectsController : MonoBehaviour
{

    //public GoogleAnalyticsV3 googleAnalytics;

    public Sprite on, off;
    // Use this for initialization
    void Start()
    {
        setImage();
    }

    void setImage()
    {
        if (PlayerPrefs.GetInt("SoundOn") == 0)
        {
            gameObject.GetComponent<Button>().image.sprite = on;
        }
        else
        {
            gameObject.GetComponent<Button>().image.sprite = off;
        }
    }

    public void click()
    {
        int a = PlayerPrefs.GetInt("SoundOn");
        if (a == 0)
        {
            a = 1;
            //googleAnalytics.LogEvent("Button", "Click", "sound_on", 1);
        }
        else
        {
            a = 0;
            //googleAnalytics.LogEvent("Button", "Click", "sound_off", 0);
        }
        PlayerPrefs.SetInt("SoundOn", a);
        setImage();
    }
}