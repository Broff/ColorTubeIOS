using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundController : MonoBehaviour {

	//public GoogleAnalyticsV3 googleAnalytics;

	public Sprite on, off;
	// Use this for initialization
	void Start () {
		setImage();		
	}
	
	void setImage(){
		if(PlayerPrefs.GetInt("MusicOn") == 0){
			gameObject.GetComponent<Button>().image.sprite = on;
		} else {
			gameObject.GetComponent<Button>().image.sprite = off;
		}
	}
	
	public void click(){
        int a = PlayerPrefs.GetInt("MusicOn");
		if(a == 0){
			//googleAnalytics.LogEvent("Button", "Click", "sound_on", 1);
		} else {
			//googleAnalytics.LogEvent("Button", "Click", "sound_off", 0);
		}		
		GameObject.Find("MainTheme").GetComponent<MainSoundController>().muteSound();
        setImage();
	}
}
