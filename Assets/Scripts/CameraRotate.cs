using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {

	public GameObject player;
	public float distance, speedLepr;
	
	void Start () {
		//distance = transform.position.y - player.transform.position.y;
	}
	
	void LateUpdate () {
		transform.position = Vector3.Lerp(transform.position, new Vector3 (player.transform.position.x ,
																	   player.transform.position.y + distance,
																	   transform.position.z), Time.deltaTime * speedLepr);		
	}
}
