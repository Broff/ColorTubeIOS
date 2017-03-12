using UnityEngine;
using System.Collections;
using com.playGenesis.VkUnityPlugin;
using com.playGenesis.VkUnityPlugin.MiniJSON;

public class VkButtonController : MonoBehaviour {

	public GameSettings settings;
	
	void Start () {
		
	}

	public void click(){
		if(VkApi.VkApiInstance.IsUserLoggedIn == false){
			login();
		} else {
			share();
		}
	}
	
	void login(){
		VkApi.VkApiInstance.Login();
	}
	
	void share(){
		settings.GetComponent<GameSettings>().VKScreenShot();
	}
	
	
	
}
