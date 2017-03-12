using UnityEngine;
//using System;
using System.Collections;

public class PlayerJump : MonoBehaviour {

	public static GameObject settings = null;
	public GameObject settings1;
	public bool toPhone = false;
	public bool gameState = false;
	public bool jumpState = true;
	public Rigidbody rb;
	public int JumpSpeed = 2000;
	public GameObject mainMenu, deadMenu;
	public GameObject jumpSound;
	public static bool firsLaunch = true;
	public int gameNum = 0;
	public GameObject arrow;

	void Start ()
	{ 		
		if(PlayerJump.settings == null){
			PlayerJump.settings = settings1;
		}
	
		if(PlayerJump.firsLaunch == true){
            //PlayerPrefs.SetInt("TopScore", 2);
			//PlayerJump.settings.GetComponent<Settings>().showADS();			
			mainMenu.GetComponent<MainScreenController> ().View ();
			//appodeal.GetComponent<AppodealController>().showAds();
		} else {
			//PlayerJump.settings.GetComponent<Settings>().showADS();			
			gameState = true;
			gameObject.GetComponent<SetColorPlayer>().ui.GetComponent<UIController> ().View();	
			//appodeal.GetComponent<AppodealController>().showAds();
		}
		//gameState = true;
		rb = GetComponent<Rigidbody>();
		jumpState = true;		
		PlayerJump.firsLaunch = false;
        //GetComponent<SetColorPlayer>().nextColor();
	} 
	
	void Update () {
		if(gameState)
		{		
			if(toPhone == false){
				if(jumpState && Input.GetButtonDown("Jump"))
				{			
					jump();
				}			
			} else {
				if(jumpState && Input.GetTouch(0).phase == TouchPhase.Began)
				{			
					//jump();
				}
			}
		}
	}
	
	bool firstJump = false;
	
	public void jump(){
		if(Settings.nowADSShow == false){
			jumpSound.GetComponent<AudioController>().JumpSound();
			rb.AddForce( new Vector3(0,JumpSpeed,0), ForceMode.Impulse);	
			jumpState = false;
			if(firstJump == false){
				arrow.SetActive(false);
				firstJump = true;
			}
		}
	}
	
	void OnCollisionEnter(Collision collis) 
	{ 			
			
	} 
	
	void OnCollisionExit () {
		
	}
	
	public void setGameState(bool st){
		gameState = st;
	}
	
	public void setJumpState(bool st){
		jumpState = st;
	}
				
	void gameOver(){
		setGameState(false);
		deadMenu.GetComponent<DeadScreenController> ().View ();	
	}
	
	public void setPosition(float x, float y, float z){
		rb.position = new Vector3(x, y, z);
	}
}
