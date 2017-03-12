using UnityEngine;
using UnityEngine.UI; 
using System;
using System.Collections;
//using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class DeadScreenController : MonoBehaviour {
	
    //public GoogleAnalyticsV3 googleAnalytics;

	public GameObject player;
	public GameObject mainMenu;
	public GameObject textScore, textTopScore;
	public GameObject settings;
	public GameObject scoreUI;
    public GameObject firework;
    public GameObject vk;
    public string[] positions;

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

        if (Application.systemLanguage != SystemLanguage.Russian)
        {
            vk.SetActive(false);
        }

        //if (openState == false)
        //{
        //    sound.transform.position = new Vector3(settB.transform.position.x, startPos, 0);
        //    music.transform.position = new Vector3(settB.transform.position.x, startPos, 0);
        //    inApp.transform.position = new Vector3(settB.transform.position.x, startPos, 0);
        //}
    }

    IEnumerator HighscoreNew()
    {     
        GameObject cam = GameObject.Find("Camera");
        GameObject f = Instantiate(firework);
        f.GetComponent<DestroyController>().DestroyInit();
        string[] fireWPos = positions[UnityEngine.Random.Range(0, positions.Length)].Split('!');
        int x = Convert.ToInt32(fireWPos[0]);
        int y = Convert.ToInt32(fireWPos[1]);
        int z = Convert.ToInt32(fireWPos[2]);
        f.transform.position = new Vector3(x, cam.transform.position.y - 26.7f + y, z);
        f.SetActive(true);
        yield return new WaitForSeconds(UnityEngine.Random.Range(1, 1.5f));
        StartCoroutine(HighscoreNew());
    }

    

    public void SettingsClick()
    {
        openState = !openState;
    }
    void Update()
    {
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
		scoreUI.GetComponent<UIController>().Skip();
		gameObject.SetActive (true);
		
		int topScore = PlayerPrefs.GetInt("TopScore");
		int score = player.GetComponent<SetColorPlayer>().score;
		
		//googleAnalytics.LogEvent("Score", "get", "get_Score_"+score, score); //Write score
        
		if(score > topScore){
            StartCoroutine(HighscoreNew());
			PlayerPrefs.SetInt("TopScore", score);
			topScore = score;
			//googleAnalytics.LogEvent("Score", "newHirgscore", "get_Score_"+score, score); //Write score
		}		
		textScore.GetComponent<Text>().text = Convert.ToString(score);
		textTopScore.GetComponent<Text>().text = "BEST "+Convert.ToString(topScore);
		
		settings.GetComponent<GameSettings>().SetTop(score);
		settings.GetComponent<GameSettings>().CheckAchivements();
		//googleAnalytics.LogScreen("Dead Menu");
	}

	public void Skip () {
		gameObject.SetActive (false);
	}

	public void Click () {
		Skip ();
		///googleAnalytics.LogEvent("Button", "Click", "start_game_new", 1);
		Application.LoadLevel("main");
		//SceneManager.LoadScene("main");
	}
}
