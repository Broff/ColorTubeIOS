using UnityEngine;
using System.Collections;

public class MoveDeadZone : MonoBehaviour {

	public GameObject player;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void move(){
		transform.position = new Vector3(transform.position.x, player.transform.position.y-1, transform.position.z);
	}
	
	void OnCollisionEnter(Collision collis) 
	{ 			
		if(collis.gameObject.name == "Player")
		{
			transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		}
	} 
}
