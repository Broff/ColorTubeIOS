using UnityEngine;
using System.Collections;

public class LevelUp : MonoBehaviour {

	public float deleteTime = 0.5f;
	public float Force1, Force2, minSpeed;
	public GameObject colorControl;
	public float scale = 0.0035f;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

	void Update () {
        if(player.GetComponent<PlayerJump>().gameState == true){
		    foreach (Transform hit in transform) {			
			    hit.localScale -= new Vector3(scale,scale,scale);
		    }		
		    deleteTime -= Time.deltaTime;
		    if(deleteTime <= 0){
			    Destroy(gameObject);
		    }
        }
	}
	
	public void LvlUp(){
		foreach (Transform hit in transform) {
			switchColor(hit);
			float x = Random.Range(Force1, Force2);
			//float y = (hit.position.y - transform.position.y);
			float z = Random.Range(Force1, Force2);
			
			if(x< 0){
				x -= minSpeed;
			} else {
				x += minSpeed;
			}
			
			if(z< 0){
				z -= minSpeed;
			} else {
				z += minSpeed;
			}
			
			hit.gameObject.GetComponent<Rigidbody> ().AddForce(new Vector3(x, 0,  z) 
			,ForceMode.Acceleration);
		}
	}
	
	void switchColor(Transform t){
		//int a = Random.Range(0,4);
		t.GetComponent<Renderer>().material = colorControl.GetComponent<ColorController>().m1;
		t.GetComponent<Renderer>().material.color = Color.white;
		/*switch(a){
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
		}	*/
	}
}
