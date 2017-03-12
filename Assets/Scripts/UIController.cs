using UnityEngine;
using UnityEngine.UI; 
using System.Collections;

public class UIController : MonoBehaviour {

	//public GoogleAnalyticsV3 googleAnalytics;
	public GameObject player, tapToPlay;
    public Button pause, resume;
    public GameObject higscoreEffect;
    
	public void View () {
        //NewHighscore();
		//googleAnalytics.LogScreen("Game Screen");
		gameObject.SetActive (true);
        tapToPlay.SetActive(false);
        resume.gameObject.SetActive(false);
	}

	public void Skip () {
		gameObject.SetActive (false);
	}

    

    public void Pause()
    {
        player.GetComponent<PlayerJump>().setGameState(false);
        Time.timeScale = 0;
        tapToPlay.SetActive(true);
        resume.gameObject.SetActive(true);
        pause.gameObject.SetActive(false);
    }

    public void Resume()
    {
        player.GetComponent<PlayerJump>().setGameState(true);
        Time.timeScale = 1;
        tapToPlay.SetActive(false);
        resume.gameObject.SetActive(false);
        pause.gameObject.SetActive(true);
    }

    public void NewHighscore()
    {
        higscoreEffect.GetComponent<NewHighscore>().Play();
    }
}
