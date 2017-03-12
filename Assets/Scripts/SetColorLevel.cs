using UnityEngine;
using System.Collections;

public class SetColorLevel : MonoBehaviour {

	public GameObject colorControl;
	public GameObject s1,s2,s3,s4,s5,s6,s7,s8,s9,s10,s11,s12,s13,s14,s15,s16,s17,s18;
	public GameObject player;
	
	public void setColor(){
		getColor((GameObject) Instantiate(s1));
		getColor((GameObject) Instantiate(s2));
		getColor((GameObject) Instantiate(s3));
		getColor((GameObject) Instantiate(s4));
		getColor((GameObject) Instantiate(s5));
		getColor((GameObject) Instantiate(s6));
		getColor((GameObject) Instantiate(s7));
		getColor((GameObject) Instantiate(s8));
		getColor((GameObject) Instantiate(s9));
		getColor((GameObject) Instantiate(s10));
		getColor((GameObject) Instantiate(s11));
		getColor((GameObject) Instantiate(s12));
		getColor((GameObject) Instantiate(s13));
		getColor((GameObject) Instantiate(s14));
		getColor((GameObject) Instantiate(s15));
		getColor((GameObject) Instantiate(s16));
		getColor((GameObject) Instantiate(s17));
		getColor((GameObject) Instantiate(s18));
	}
	
	public void offLevel(){
		getColor(s1, true);
		getColor(s2, true);
		getColor(s3, true);
		getColor(s4, true);
		getColor(s5, true);
		getColor(s6, true);
		getColor(s7, true);
		getColor(s8, true);
		getColor(s9, true);
		getColor(s10, true);
		getColor(s11, true);
		getColor(s12, true);
		getColor(s13, true);
		getColor(s14, true);
		getColor(s15, true);
		getColor(s16, true);
		getColor(s17, true);
		getColor(s18, true);
	}
	
	void getColor(GameObject o, bool off = false){
		int a = Random.Range(0,4);
		if(off == false){
			switch(a){
				case 0:
				o.tag = "red";
				o.transform.GetComponent<Renderer>().material = colorControl.GetComponent<ColorController>().m1;
				break;
				case 1:
				o.tag = "green";
				o.transform.GetComponent<Renderer>().material = colorControl.GetComponent<ColorController>().m2;
				break;
				case 2:
				o.tag = "blue";
				o.transform.GetComponent<Renderer>().material = colorControl.GetComponent<ColorController>().m3;
				break;
				case 3:
				o.tag = "yellow";
				o.transform.GetComponent<Renderer>().material = colorControl.GetComponent<ColorController>().m4;
				break;			
			}	
		} else {
			o.tag = "gray";
			o.transform.GetComponent<Renderer>().material.color = Color.gray;
		}
	}
}
