using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SetColorPlayer : MonoBehaviour {

	public GameObject colorControl;
	public Rigidbody rb;
	public GameObject player;
	string type = "red";
	Color color;
	public GameObject deadMenu, ui, deadZone;
	public int score = 0;
	public GameObject scoreText;
	public float currentTime = 0;
	public float delay = 3;
	public GameObject deadSound, lvlUp, levelUpSound;	
	
	void Start () {
		scoreText.GetComponent<Text>().text = score.ToString ();
		//randomColor();
		rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
		/*if(player.GetComponent<PlayerJump>().jumpState == true) {
			if(currentTime != 0){
				if(Time.time - currentTime >= delay){
					//randomColor();
					currentTime = Time.time;
				}
			} else {
				currentTime = Time.time;
			}
		}	*/
	}
	
	void OnCollisionEnter(Collision collis) 
	{ 		
	
		if(player.GetComponent<PlayerJump>().jumpState == false) {
		//Debug.Log(""+collis.gameObject.tag);
		//Debug.Log(""+type);
			currentTime = Time.time;
			if(collis.gameObject.tag == type)
			{
				rb.transform.position = new Vector3(rb.transform.position.x,collis.gameObject.transform.position.y, rb.transform.position.z);				
				//randomColor();
				nextColor();
				score++;
				scoreText.GetComponent<Text>().text = score.ToString ();
                if (PlayerPrefs.GetInt("TopScore")+1 == score && score != 1 && score != 0)
                {
                    ui.GetComponent<UIController>().NewHighscore();
                }
				deadZone.GetComponent<MoveDeadZone>().move();	
				//if(score % 5 == 0){				
					levelUp();
				//}
				//collis.gameObject.GetComponent<SetColorLevel>().offLevel();
			}	
			else if(collis.gameObject.tag != "gray"){
				gameOver();
			}			
			player.GetComponent<PlayerJump>().setJumpState(true);
		}
		
		if(collis.gameObject.name == "DeadZone")
		{
			gameOver();
		}
	} 
	
	void gameOver(){
		deadSound.GetComponent<AudioController>().DeadSound();
		player.GetComponent<PlayerJump> ().setGameState(false);
		player.GetComponent<PlayerDestroy> ().boom();
		deadMenu.GetComponent<DeadScreenController> ().View ();	
		ui.GetComponent<UIController> ().Skip();	
		//score = 0;
	}
	public int colorNum;
	
	public void randomColor(){
		int a = Random.Range(0,4);
		colorNum = a;
		//transform.GetComponent<Renderer>().material = colorControl.GetComponent<ColorController>().m1;
		getColorM(a);		
	}
	
	LinkedList<int> colorNext = new LinkedList<int>();
	
	public void nextColor(){
		int a = colorNext.First.Value;
		getColorM(a-1);
		colorNext.RemoveFirst();
	}
	
	public void addColor(int col){
		colorNext.AddLast(col);
        Debug.Log(col+"Color");
	}
	
	public void getColorM(int a){
		switch(a){
			case 0:
			type = "red";
			transform.GetComponent<Renderer>().material = colorControl.GetComponent<ColorController>().m1;
			//transform.GetComponent<Renderer>().material.color = new Color32(116,41,204,1);
			break;
			case 1:
			type = "green";
			transform.GetComponent<Renderer>().material = colorControl.GetComponent<ColorController>().m2;

			//transform.GetComponent<Renderer>().material.color = new Color32(252,45,125,1);
			break;
			case 2:
			type = "blue";
			transform.GetComponent<Renderer>().material = colorControl.GetComponent<ColorController>().m3;
			//transform.GetComponent<Renderer>().material.color = colorControl.GetComponent<ColorController>().c3;
			//transform.GetComponent<Renderer>().material.color = new Color32(245,226,0,1);
			break;
			case 3:
			type = "yellow";
			transform.GetComponent<Renderer>().material = colorControl.GetComponent<ColorController>().m4;
			//transform.GetComponent<Renderer>().material.color = colorControl.GetComponent<ColorController>().c4;
			//transform.GetComponent<Renderer>().material.color = new Color32(36,233,240,1);
			break;			
		}	
	}
	
	public string getColor(){
		return type;
	}
	
	public Color getColorVal(){
		return transform.GetComponent<Renderer>().material.color;
	}
	
	void levelUp(){	
		GameObject temp = Instantiate(lvlUp);
		temp.transform.position = transform.position;
		temp.SetActive(true);
		temp.GetComponent<LevelUp>().LvlUp();
		levelUpSound.GetComponent<AudioController>().BoomSound();
	}
}
