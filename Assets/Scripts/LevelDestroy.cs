using UnityEngine;
using System.Collections;

public class LevelDestroy : MonoBehaviour {

	public bool destroyState = false;
	public float destroyTime = 5.0f;
	float destroyTimeS = 5.0f;
	public float destroyDeltaTime = 0.5f;
	float destroyDeltaTimeS = 0.5f;
	public float destroySpeed = 10.0f;
	public GameObject level, colors;
	public int segmentCount;
	
	void Start () {
		destroyDeltaTimeS = destroyDeltaTime;	
		destroyTimeS = destroyTime;		
	}
	
	int index = 0;
	float tweenTime = 0;
	void Update () {
		if(destroyState == true){			
			destroyTime -= Time.deltaTime;			
			level.GetComponent<LevelRotation>().setRotate(false);
			
			if(destroyTime <= 0.0f)
			{
				if(index < segmentCount)
				{
					tweenColor(index, tweenTime, destroyDeltaTimeS, colors.GetComponent<ColorController>().unactive.color, Color.red);
				}
				tweenTime += Time.deltaTime;
				destroyDeltaTime -= Time.deltaTime;				
				if(destroyDeltaTime <= 0.0f && index < segmentCount)
				{					
					destroyPart(index);
					destroyDeltaTime = destroyDeltaTimeS;
					index++;	
					tweenTime = 0;					
				} 
			} else {
				for(int i = 0; i < 16; i++){		
					tweenColor(i, destroyTime, destroyTimeS, colors.GetComponent<ColorController>().unactive.color, Color.white);
				}				
			}
		}
	}
	
	void OnTriggerEnter (Collider c) {
		if(c.gameObject.name == "Player"){	
			destroyState = true;	
			transform.rotation = Quaternion.Euler(-90, 0, 0);		
			offLevel(gameObject);
		}
	}
	
	public void offLevel(GameObject o){
		for(int i = 0; i < 16; i++){		
			getColor(o.transform.FindChild(getName(i)).gameObject);
		}
	}
	
	void destroyPart(int i){
		string name = getName(i);		
		transform.FindChild(name).gameObject.AddComponent<Rigidbody>();
		transform.FindChild(name).gameObject.GetComponent<Collider>().isTrigger = true;
		transform.FindChild(name).gameObject.GetComponent<Rigidbody>().mass = 50;
	}
	
	void tweenColor(int i, float t, float time, Color start, Color finish){
		transform.FindChild(getName(i)).gameObject.transform.GetComponent<Renderer>().material.color
		= Color.Lerp(start, finish, t / time);		
	}
	
	string getName(int i){
		string name ="";
		switch(i){
			case 0:
				name = "Object02";				
			break;
			case 1:
				name = "Object03";
			break;
			case 2:
				name = "Object01";
			break;
			case 3:
				name = "Object04";					
			break;
			case 4:
				name = "Object16";	
			break;
			case 5:
				name = "Object05";
			break;
			case 6:
				name = "Object15";
			break;
			case 7:
				name = "Object06";
			break;
			case 8:
				name = "Object14";
			break;
			case 9:
				name = "Object07";
			break;
			case 10:
				name = "Object13";
			break;
			case 11:
				name = "Object08";
			break;
			case 12:
				name = "Object12";
			break;
			case 13:
				name = "Object09";
			break;
			case 14:		
				name = "Object11";
			break;				
			case 15:				
				name = "Object10";
			break;
		}
		return name;
	}
	
	
	void getColor(GameObject o){	
		o.transform.GetComponent<Renderer>().material = colors.GetComponent<ColorController>().unactive;		
	}
}
