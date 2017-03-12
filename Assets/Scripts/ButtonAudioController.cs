using UnityEngine;
using System.Collections;

public class ButtonAudioController : MonoBehaviour {

	public AudioClip click;
	AudioSource audio;
	int state = 1;
	
	void Start(){
		audio = GetComponent<AudioSource>();
		if(PlayerPrefs.GetInt("SoundOn") == 0){
			state = 0;
		}
	}
	
	public void Click(){
		if(PlayerPrefs.GetInt("SoundOn") == 0){
			audio.PlayOneShot(click, 0.5f);
		}
	}
}
