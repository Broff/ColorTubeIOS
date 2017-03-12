using UnityEngine;
using System.Collections;

public class LevelRotation : MonoBehaviour {

	public int direction = 1;
	public int speed = 20;
	bool rotate = true;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(rotate == true){
			rotateLevel();
		}
	}
	
	public void rotateLevel(){
		transform.Rotate(new Vector3(0,0,speed * direction) * Time.deltaTime);
	}
	
	public void setSpeed(int s){
		speed = s;
	}
	
	public void setRotate(bool r){
		rotate = r;
	}
}
