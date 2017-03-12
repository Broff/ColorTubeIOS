using UnityEngine;
//using System;
using System.Collections;

public class PlayerDestroy : MonoBehaviour {

	public float Force1,Force2;
	public GameObject deadPlayer,colorControl;
	GameObject temp = null;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(temp != null){
			foreach (Transform hit in temp.transform) {
				hit.Rotate(new Vector3(Random.Range(-500,500), Random.Range(-500,500), Random.Range(-500,500)) * Time.deltaTime);
				hit.localScale += new Vector3(0.001f,0.001f,0.001f);
			}
		}
	}
	
	public void boom(){		
		//gameObject.SetActive (false);
		gameObject.GetComponent<Renderer>().enabled = false;
		gameObject.GetComponent<Rigidbody>().useGravity = false;
		temp = Instantiate(deadPlayer);
		temp.transform.position = transform.position;
		temp.SetActive(true);
		foreach (Transform hit in temp.transform) {
			switchColor(hit);
			float x = (hit.position.x - transform.position.x);
			float y = (hit.position.y - transform.position.y);
			float z = (hit.position.z - transform.position.z);
			//if(y<0){
				y+=4.5f;
			//}
			
			hit.gameObject.GetComponent<Rigidbody> ().AddForce(new Vector3(x, y, z) * Random.Range(Force1, Force2),ForceMode.Acceleration);
		}
	}
	
	void switchColor(Transform t){
		int a = Random.Range(0,4);
		switch(a){
			case 0:
			t.GetComponent<Renderer>().material = colorControl.GetComponent<ColorController>().m1;
			break;
			case 1:
			t.GetComponent<Renderer>().material = colorControl.GetComponent<ColorController>().m2;
			break;
			case 2:
			t.GetComponent<Renderer>().material = colorControl.GetComponent<ColorController>().m3;
			break;
			case 3:
			t.GetComponent<Renderer>().material = colorControl.GetComponent<ColorController>().m4;
			break;			
		}		
	}
}
