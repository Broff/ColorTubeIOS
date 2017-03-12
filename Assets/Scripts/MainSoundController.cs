using UnityEngine;
using System.Collections;

public class MainSoundController : MonoBehaviour {
		
	public static MainSoundController instance = null;
	
	void Start () {
        if(instance!=null){
            Destroy(gameObject);
			Debug.Log ("One Music Destroyed");
            return;
        }
	    instance = this;
        DontDestroyOnLoad (gameObject);
		checkSound();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}	
	
	public void muteSound(){
		if(PlayerPrefs.GetInt("MusicOn") == 1){
			//AudioListener.volume = 1;
            PlayerPrefs.SetInt("MusicOn", 0);
		} else {
			//AudioListener.volume = 0;
            PlayerPrefs.SetInt("MusicOn", 1);
		}
        checkSound();
	}

    void checkSound()
    {
        if (PlayerPrefs.GetInt("MusicOn") == 1)
        {
            GameObject.Find("MainTheme").GetComponent<AudioSource>().mute = true;
        }
        else
        {
            GameObject.Find("MainTheme").GetComponent<AudioSource>().mute = false;            
        }
    }
	
	public void setSoundVolume(int val){
		//AudioListener.volume = val;
	}
}
