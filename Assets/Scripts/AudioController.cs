using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	public AudioClip jump, dead, boom;
	AudioSource audio;
	int state = 1;
	
	void Start(){
		audio = GetComponent<AudioSource>();
		
	}
	
	public void JumpSound(){
		if(PlayerPrefs.GetInt("SoundOn") == 0){
			audio.PlayOneShot(jump, 0.7f);
		}
	}
	
	public void DeadSound(){
		if(PlayerPrefs.GetInt("SoundOn") == 0){
			audio.PlayOneShot(dead);
		}
	}
	
	public void BoomSound(){
		if(PlayerPrefs.GetInt("SoundOn") == 0){
			audio.PlayOneShot(boom, 0.1f);
		}
	}
}
