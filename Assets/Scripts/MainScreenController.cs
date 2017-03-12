using UnityEngine;
using UnityEngine.UI; 
using System.Collections;
//using GooglePlayGames;
using UnityEngine.SocialPlatforms;    

public class MainScreenController : MonoBehaviour {

	//public GoogleAnalyticsV3 googleAnalytics;

	public GameObject player, ui, tapToPlay, vk;
	public Color c1,c2;
	public float timeSwitch;
	bool state = true;
	float delta = 0;
	bool a = false;

    [Space(5)]
    [Header("Settings")]
    public float speed;
    public GameObject settB;
    public GameObject sound;
    public GameObject music;
    public GameObject inApp;
	public GameObject inAppBG;

    public float settDeltaPos = 7;
    private float startPos;
    private float finishSound;
    private float finishMusic;
    private float finishInApp;
    private bool openState = false;

    void Start()
    {
        startPos = settB.transform.position.y;
        //finishSound = sound.transform.position.y;
        //finishMusic = music.transform.position.y;
        //finishInApp = inApp.transform.position.y;
        float height = Screen.height / settDeltaPos;
        finishSound = startPos + height * 1;
        finishMusic = startPos + height * 2;
        finishInApp = startPos + height * 3;

        if(Application.systemLanguage != SystemLanguage.Russian){
            vk.SetActive(false);
        }

        //if (openState == false)
        //{
        //    sound.transform.position = new Vector3(settB.transform.position.x, startPos, 0);
        //    music.transform.position = new Vector3(settB.transform.position.x, startPos, 0);
        //    inApp.transform.position = new Vector3(settB.transform.position.x, startPos, 0);
        //}
    }

    public void SettingsClick()
    {
        openState = !openState;
    }
    void Update()
    {
        if (state == true)
        {
            if (delta >= timeSwitch)
            {
                delta = 0;
                a = !a;
            }

            if (a == false)
            {
                tapToPlay.GetComponent<Image>().color = Color.Lerp(c1, c2, delta / timeSwitch);
                delta += Time.deltaTime;
            }
            else
            {
                tapToPlay.GetComponent<Image>().color = Color.Lerp(c2, c1, delta / timeSwitch);
                delta += Time.deltaTime;
            }
        }
         startPos = settB.transform.position.y;
         float height = Screen.height / settDeltaPos;
         finishSound = startPos + height * 1;
         finishMusic = startPos + height * 2;
         finishInApp = startPos + height * 3;
         if (first == true)
         {
             if (openState == false)
             {
                 //sound.transform.position = new Vector3(sound.transform.position.x, startPos, 0);
                 //music.transform.position = new Vector3(music.transform.position.x, startPos, 0);
                 //inApp.transform.position = new Vector3(inApp.transform.position.x, startPos, 0);
             }
         }
         else
         {

			if (openState == false)
			{
				sound.transform.position = Vector3.Lerp(sound.transform.position, new Vector3(settB.transform.position.x, startPos, settB.transform.position.y), speed * Time.deltaTime);
				music.transform.position = Vector3.Lerp(music.transform.position, new Vector3(settB.transform.position.x, startPos, settB.transform.position.y), speed * Time.deltaTime);
				inApp.transform.position = Vector3.Lerp(inApp.transform.position, new Vector3(settB.transform.position.x, startPos, settB.transform.position.y), speed * Time.deltaTime);
				inAppBG.transform.position = Vector3.Lerp(inApp.transform.position, new Vector3(settB.transform.position.x, startPos, settB.transform.position.y), speed * Time.deltaTime);
			}
			else
			{
				sound.transform.position = Vector3.Lerp(sound.transform.position, new Vector3(settB.transform.position.x, finishSound, settB.transform.position.y), speed * Time.deltaTime);
				music.transform.position = Vector3.Lerp(music.transform.position, new Vector3(settB.transform.position.x, finishMusic, settB.transform.position.y), speed * Time.deltaTime);
				inApp.transform.position = Vector3.Lerp(inApp.transform.position, new Vector3(settB.transform.position.x, finishInApp, settB.transform.position.y), speed * Time.deltaTime);
				inAppBG.transform.position = Vector3.Lerp(inAppBG.transform.position, new Vector3(settB.transform.position.x, finishInApp, settB.transform.position.y), speed * Time.deltaTime);
			}
         }
         first = false;
    }
    bool first = true;
	
	public void View () {
		state = true;
		gameObject.SetActive (true);
		//googleAnalytics.LogScreen("Main Menu");
        startPos = settB.transform.position.y;       
	}

	public void Skip () {
		state = false;
		gameObject.SetActive (false);
	}

	public void Click () {
		Skip ();
		player.GetComponent<PlayerJump> ().setGameState (true);
		player.GetComponent<PlayerJump> ().jump ();
		//player.GetComponent<SetColorPlayer> ().randomColor ();
		ui.GetComponent<UIController> ().View();	
		//googleAnalytics.LogEvent("Button", "Click", "start_game_first", 1);
	}
}
