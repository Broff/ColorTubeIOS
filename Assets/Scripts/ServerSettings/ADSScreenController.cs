using UnityEngine;
using System.Collections;

public class ADSScreenController : MonoBehaviour {

	public GameObject mainSound;
	public GameObject buttHide;
	string URL = "";
	static ADSScreenController instance;
	
	void Start(){
		if(instance!=null){
            Destroy(gameObject);
            return;
        }
	    instance = this;
        DontDestroyOnLoad (gameObject);
	}
	
	public void View (string url) {		
		URL = url;
		Settings.nowADSShow = true;
		mainSound.GetComponent<MainSoundController>().setSoundVolume(0);
		gameObject.SetActive (true);		
	}
	
	public void ViewUpdate (string url) {		
		URL = url;
		Settings.nowADSShow = true;
		mainSound.GetComponent<MainSoundController>().setSoundVolume(0);
		buttHide.SetActive (false);	
		gameObject.SetActive (true);		
	}

	public void Skip () {		
		gameObject.SetActive (false);		
	}
	
	public void WatchADS(){
		Debug.Log("ShowADS");
		Application.OpenURL(URL);
	}

	public void HideADS () {
		Settings.nowADSShow = false;
		mainSound.GetComponent<MainSoundController>().muteSound();
		Skip ();
	}
}
