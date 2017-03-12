using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class NewHighscore : MonoBehaviour {

    public Text newBest;
    public Color start;
    public Color finish;
    public float startScale;
    public float finishScale;
    public float pulseTime;
    public int pulseCount;
    public AudioClip audio;

    [Header("Fireworks")]
    public GameObject firework;
    public string[] positionsFirework;


    private float deltaScale;
    private int loopCount;
    private bool startState = false;

	void Start () {
        newBest.transform.localScale = new Vector3(startScale, startScale, startScale);
	    deltaScale = (finishScale - startScale) / (pulseTime / (2.0f * Time.deltaTime));
        //newBest.gameObject.SetActive(false);
	}
	
	void Update () {
	    if(startState == true){
            if(loopCount < pulseCount){
                Pulse();
            }
            else if (loopCount >= pulseCount)
            {
                Hide();
            }
        }
	}

    public void Play()
    {
        GameObject cam = GameObject.Find("Camera");
        foreach(string pos in positionsFirework){
            GameObject f = Instantiate(firework);
            f.GetComponent<DestroyController>().DestroyInit();
            string[] fireWPos = pos.Split('!');
            int x = Convert.ToInt32(fireWPos[0]);
            int y = Convert.ToInt32(fireWPos[1]);
            f.transform.position = new Vector3(x,cam.transform.position.y-26.7f + y,f.transform.position.z);
            f.SetActive(true);
        }
        if (PlayerPrefs.GetInt("SoundOn") == 0)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(audio);
        }
        startState = true;
        newBest.gameObject.SetActive(true);
    }

    private float pulseTimer = 0;
    private float plus = 0;
    private float minus = 0;
    
    private void Pulse(){

       float scale = newBest.transform.localScale.x;

        if (pulseTimer < pulseTime / 2.0f)
        {
            newBest.transform.localScale = new Vector3(scale + deltaScale, scale + deltaScale, scale + deltaScale);
            tweenColor(plus, pulseTime / 2.0f, start, finish);
            plus += Time.deltaTime;
        }
        else
        {
            newBest.transform.localScale = new Vector3(scale - deltaScale, scale - deltaScale, scale - deltaScale);
            tweenColor(minus, pulseTime / 2.0f, finish, start);
            minus += Time.deltaTime;
        }
        pulseTimer += Time.deltaTime;
        if (pulseTimer >= pulseTime)
        {
            loopCount++;
            pulseTimer = 0;
            plus = 0;
            minus = 0;
        }
    }

    void tweenColor(float t, float time, Color start, Color finish)
    {
        newBest.color = Color.Lerp(start, finish, t / time);
    }

    float hideTime = 0;
    private void Hide(){

        float scale = newBest.transform.localScale.x;
        newBest.transform.localScale = new Vector3(scale - deltaScale, scale - deltaScale, scale - deltaScale);
        tweenColor(hideTime, pulseTime / 2.0f, start, new Color(newBest.color.r, newBest.color.g, newBest.color.b, 0));
        hideTime += Time.deltaTime;
        if(scale <= 0){
            startState = false;
            loopCount = 0;
            newBest.transform.localScale = new Vector3(startScale, startScale, startScale);
            newBest.gameObject.SetActive(false);
        }
    }
}
