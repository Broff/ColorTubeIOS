using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {

	public Color c1,c2;
	public GameObject colors;
	public GameObject o1,o2;
	public float timeSwitch;
	float delta = 0.0f;
	bool a = false;
	void Start () {
		//c1 = colors.GetComponent<ColorController>().unactive.color;
		//c2 = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		if(delta >= timeSwitch){
			delta = 0;
			a = !a;
		} 
		
		if(a == false){
			transform.GetComponent<Renderer>().material.color = Color.Lerp(c1, c2, delta / timeSwitch);	
			//o2.transform.GetComponent<Renderer>().material.color = Color.Lerp(c1, c2, delta / timeSwitch);	
		} else {
			transform.GetComponent<Renderer>().material.color = Color.Lerp(c2, c1, delta / timeSwitch);	
			//o2.transform.GetComponent<Renderer>().material.color = Color.Lerp(c2, c1, delta / timeSwitch);	
		}
		delta += Time.deltaTime;
	}
	
	void OnCollisionEnter(Collision collis) 
	{ 
		Destroy(collis.gameObject);
	}
}
