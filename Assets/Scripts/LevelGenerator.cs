using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class LevelGenerator : MonoBehaviour {

	public bool toPhone = false;
	public GameObject colorControl;
	public GameObject level;
	public GameObject player;
	public float startPosition, delta;
	public int lvlDeltaGeneration = 2;
	public int startlvlCount = 3;	
	public GameObject levels;
	float position;
	int index = 0;
    //int[] defaultLevels = new int[]{1,2,3};
    int[] defaultLevels = new int[]{0,0,1,1,1,1,1,2,2,2,1,2,2,3,2,1,3,3,2,3,1,3,2};
	int defInt = 0;
	int[] pattern = new int[]{1,3,2,3,1,2,2,3,2,3,2,1};
    ArrayList arr0 = new ArrayList();
    ArrayList arr1 = new ArrayList();
	ArrayList arr2 = new ArrayList();
	ArrayList arr3 = new ArrayList();
	
	//int[][] levelColors = new int[1][1];
	
	void Start () {
		position = startPosition;
		//info = new int[5][18];
		parseFile();
		startGeneration();
	}
	
	void Update () {
		if(player.transform.position.y >= position -  delta*lvlDeltaGeneration){			
			generateLevel(getLevelStr());
		}
	}
	
	string getLevelStr(){
		string s;

        int diffic = 0;
        if (defInt < defaultLevels.Length)
        {
            diffic = Convert.ToInt32(defaultLevels[defInt]);
            defInt++;
        }
        else
        {
            diffic = Convert.ToInt32(pattern[index]);
            index++;
            if (index > pattern.Length - 1)
            {
                index = 0;
            }
        }
        if (diffic == 0)
        {
            s = (string)arr0[UnityEngine.Random.Range(0, arr0.Count)];
        }
        else if (diffic == 1)
        {
            s = (string)arr1[UnityEngine.Random.Range(0, arr1.Count)];
        }
        else if (diffic == 2)
        {
            s = (string)arr2[UnityEngine.Random.Range(0, arr2.Count)];
        }
        else
        {
            s = (string)arr3[UnityEngine.Random.Range(0, arr3.Count)];
        }
		if(player.GetComponent<PlayerJump>().gameNum > 0){
			
		} else {
            player.GetComponent<SetColorPlayer>().getColorM(Convert.ToInt32(s.Split('!')[3]) - 1);
			/*int the1color = UnityEngine.Random.Range(0,4)+1;
			int the2color = the1color;
			while(the1color == the2color){
				the2color = UnityEngine.Random.Range(0,4)+1;
			}
			s = "";
			for(int i = 0; i < 8; i++){
				s+=the1color+".";
			}
			for(int i = 0; i < 7; i++){
				s+=the2color+".";
			}
			s+=the2color+"!150!2!1";*/
			//player.GetComponent<SetColorPlayer>().getColorM(the2color - 1);
		}
		player.GetComponent<PlayerJump>().gameNum++;
		//Debug.Log(s);
		return s;
	}
	
	public void startGeneration(){
	Debug.Log("START GENER");
		for(int i = 0; i < startlvlCount; i++){
			generateLevel(getLevelStr());
		}
	}
	
	void generateLevel(string lvlStr){
		GameObject go = (GameObject) Instantiate(level);
		string[] lvl = lvlStr.Split('!');
		setColor(go, lvl[0]);
		go.GetComponent<LevelRotation>().speed = Convert.ToInt32(lvl[1]);
		go.GetComponent<LevelDestroy>().destroyDeltaTime = Convert.ToInt32(lvl[2]) * 0.1f;
		go.transform.position = new Vector3 (0, position, 0);	
		go.SetActive(true);
		position += delta;

        if (player.GetComponent<PlayerJump>().gameNum != 1)
        {
		    player.GetComponent<SetColorPlayer>().addColor(Convert.ToInt32(lvl[3]));
        }
	}
	
	
	public void setColor(GameObject o, string colorNums){
	string[] colors = colorNums.Split('.');
		getColor(o.transform.FindChild("Object01").gameObject, (string)colors[0]);
		getColor(o.transform.FindChild("Object02").gameObject, (string)colors[1]);
		getColor(o.transform.FindChild("Object03").gameObject, (string)colors[2]);
		getColor(o.transform.FindChild("Object04").gameObject, (string)colors[3]);
		getColor(o.transform.FindChild("Object05").gameObject, (string)colors[4]);
		getColor(o.transform.FindChild("Object06").gameObject, (string)colors[5]);
		getColor(o.transform.FindChild("Object07").gameObject, (string)colors[6]);
		getColor(o.transform.FindChild("Object08").gameObject, (string)colors[7]);
		getColor(o.transform.FindChild("Object09").gameObject, (string)colors[8]);
		getColor(o.transform.FindChild("Object10").gameObject, (string)colors[9]);
		getColor(o.transform.FindChild("Object11").gameObject, (string)colors[10]);
		getColor(o.transform.FindChild("Object12").gameObject, (string)colors[11]);
		getColor(o.transform.FindChild("Object13").gameObject, (string)colors[12]);
		getColor(o.transform.FindChild("Object14").gameObject, (string)colors[13]);
		getColor(o.transform.FindChild("Object15").gameObject, (string)colors[14]);
		getColor(o.transform.FindChild("Object16").gameObject, (string)colors[15]);
	}
	
	void getColor(GameObject o, string s){		
		int a  = Convert.ToInt32(s);
		switch(a){
			case 1:
			o.tag = "red";
			o.transform.GetComponent<Renderer>().material = colorControl.GetComponent<ColorController>().m1;
			break;
			case 2:
			o.tag = "green";
			o.transform.GetComponent<Renderer>().material = colorControl.GetComponent<ColorController>().m2;
			break;
			case 3:
			o.tag = "blue";
			o.transform.GetComponent<Renderer>().material = colorControl.GetComponent<ColorController>().m3;
			break;
			case 4:
			o.tag = "yellow";
			o.transform.GetComponent<Renderer>().material = colorControl.GetComponent<ColorController>().m4;
			break;		
		}
	}
	
	void parseFile(){
		string fileName;
		if(toPhone == false){
			fileName = "D://colorlevels.txt";
		} else {
			fileName = "/sdcard/colorlevels.txt";
		}
        string[] linesString = s1.Split('\n');//= File.ReadAllLines(fileName);//
		
		Debug.Log("Count levels - "+linesString.Length);
		string[] lvl;
		for(int i = 0; i <= linesString.Length-1; i++){
			lvl = linesString[i].Split('!');
			int t = Convert.ToInt32(lvl[4]);
			if(t == 0){
				arr0.Add(linesString[i]);
			} else if(t == 1){
				arr1.Add(linesString[i]);
			} else if(t == 2){
				arr2.Add(linesString[i]);
			} else if(t == 3) {
				arr3.Add(linesString[i]);
			}			
			Debug.Log(""+linesString[i]);
		}
	}

    string s1 = "4.4.4.4.4.4.4.4.2.2.2.2.2.2.2.2!170!2!2!0\n"+
"1.1.1.1.1.2.2.2.2.2.3.3.3.3.3.3!-180!2!3!0\n"+
"1.1.1.1.1.1.4.4.4.4.4.4.4.4.4.4!180!2!1!0\n"+
"3.3.3.3.1.1.1.1.3.3.3.3.1.1.1.1!-180!2!3!0\n"+
"1.1.1.1.1.3.3.3.3.3.2.2.2.2.2.2!180!2!3!1\n"+
"1.1.1.1.3.3.3.3.1.1.1.1.3.3.3.3!180!2!1!1\n"+
"4.4.4.4.4.4.1.1.4.4.4.4.4.4.1.1!180!2!1!1\n"+
"2.2.2.2.3.3.3.2.2.2.2.2.3.3.3.2!-180!2!3!1\n"+
"4.4.4.4.3.3.3.3.2.2.2.2.1.1.1.1!180!2!2!1\n"+
"1.1.1.1.1.1.1.1.4.4.4.4.4.4.4.4!-300!2!1!1\n"+
"3.3.3.3.3.3.2.2.3.3.3.3.3.3.2.2!180!2!2!1\n"+
"2.2.2.2.4.4.4.4.2.2.2.2.4.4.4.4!-190!2!2!1\n"+
"4.4.4.4.4.4.3.3.4.4.4.4.4.4.3.3!-185!2!3!1\n"+
"3.3.3.3.2.2.2.2.3.3.3.3.2.2.2.2!-200!2!2!1\n"+
"3.3.3.3.3.4.4.4.3.3.3.3.3.4.4.4!190!2!4!1\n"+
"1.1.1.1.1.3.3.3.3.3.2.2.2.2.2.2!250!2!3!2\n"+
"1.1.1.1.3.3.3.3.1.1.1.1.3.3.3.3!-250!2!1!2\n"+
"4.4.4.4.4.4.1.1.4.4.4.4.4.4.1.1!230!2!1!2\n"+
"2.2.2.2.3.3.3.2.2.2.2.2.3.3.3.2!-250!2!3!2\n"+
"4.4.4.4.3.3.3.3.2.2.2.2.1.1.1.1!270!2!3!2\n"+
"2.2.2.2.3.3.3.3.3.3.3.3.3.3.3.3!250!2!2!1\n"+
"1.1.1.1.1.1.1.1.4.4.4.4.4.4.4.4!-300!2!1!1\n"+
"3.3.3.3.3.3.2.2.3.3.3.3.3.3.2.2!230!2!2!1\n"+
"2.2.2.2.4.4.4.4.2.2.2.2.4.4.4.4!-240!2!2!1\n"+
"4.4.4.4.4.4.3.3.4.4.4.4.4.4.3.3!-220!2!3!1\n"+
"3.3.3.3.2.2.2.2.3.3.3.3.2.2.2.2!-250!2!2!1\n"+
"3.3.3.3.3.4.4.4.3.3.3.3.3.4.4.4!230!2!4!1\n"+
"1.1.2.2.3.3.4.4.1.1.4.4.2.2.3.3!250!2!4!2\n"+
"1.1.4.4.4.3.3.3.1.1.1.4.4.2.2.2!230!2!3!2\n"+
"1.1.1.4.4.3.3.3.1.1.1.4.4.2.2.2!230!2!4!2\n"+
"1.1.2.2.3.3.4.4.1.1.4.4.2.2.3.3!250!2!2!2\n"+
"1.1.1.2.2.2.2.1.1.1.4.4.4.3.3.3!230!2!2!2\n"+
"3.3.4.4.4.2.2.2.1.1.1.4.4.2.2.2!260!2!1!2\n"+
"1.1.2.2.2.3.3.3.1.1.3.3.4.4.4.4!260!2!4!2\n"+
"3.3.2.2.2.4.4.1.1.4.4.2.2.3.3.3!250!2!2!2\n"+
"4.4.2.2.2.3.3.3.4.4.3.3.1.1.1.1!260!2!1!2\n"+
"1.1.2.2.3.3.4.4.1.1.4.4.2.2.3.3!260!2!!2\n"+
"1.1.2.2.3.3.4.4.1.1.1.2.2.2.4.4!280!2!2!2\n"+
"1.1.4.4.2.2.2.1.1.1.4.4.3.3.3.3!260!2!1!2\n"+
"1.1.1.4.4.4.4.2.2.2.1.1.3.3.3.3!280!2!2!2\n"+
"1.1.2.2.3.3.1.1.1.4.4.2.2.3.3.3!-280!2!3!2\n"+
"1.1.4.4.4.3.3.3.1.1.1.4.4.2.2.2!-250!2!4!2\n"+
"1.1.1.4.4.3.3.3.1.1.4.4.4.2.2.2!-250!2!1!2\n"+
"1.1.2.2.3.3.1.1.1.4.4.2.2.3.3.3!-280!2!2!2\n"+
"1.1.1.2.2.2.1.1.1.4.4.4.3.3.3.3!-260!2!3!2\n"+
"3.3.4.4.4.2.2.2.1.1.1.4.4.2.2.2!-240!2!4!2\n"+
"1.1.2.2.2.3.3.1.1.1.3.3.4.4.4.4!-240!2!1!2\n"+
"3.3.2.2.4.4.4.1.1.1.2.2.2.3.3.3!-260!2!2!2\n"+
"4.4.2.2.2.3.3.3.4.4.3.3.1.1.1.1!-260!2!3!2\n"+
"1.1.2.2.4.4.4.1.1.1.2.2.2.3.3.3!-280!2!4!2\n"+
"1.1.1.3.3.2.2.1.1.4.4.2.2.2.4.4!-280!2!1!2\n"+
"1.1.4.4.2.2.2.1.1.1.4.4.3.3.3.3!-260!2!2!2\n"+
"1.1.1.4.4.4.4.2.2.2.1.1.3.3.3.3!-280!2!3!2\n"+
"1.1.1.1.3.3.3.3.4.4.4.4.2.2.2.2!330!2!4!3\n"+
"1.1.1.2.2.2.2.1.1.1.4.4.4.3.3.3!350!2!1!3\n"+
"1.1.1.1.1.3.3.3.4.4.4.4.4.2.2.2!330!2!2!3\n"+
"1.1.4.4.4.3.3.3.1.1.1.4.4.2.2.2!330!2!3!3\n"+
"4.4.4.4.1.1.1.1.3.3.3.3.2.2.2.2!350!2!4!3\n"+
"2.2.2.1.1.1.1.2.2.2.4.4.4.3.3.3!350!2!1!3\n"+
"1.1.1.3.3.3.3.3.4.4.4.4.2.2.2.2!350!2!2!3\n"+
"1.1.4.4.4.3.3.3.1.1.1.4.4.2.2.2!330!2!3!3\n"+
"1.1.1.1.3.3.3.4.4.4.4.2.2.2.2.2!350!2!4!3\n"+
"1.1.1.1.1.3.3.3.3.4.4.4.4.2.2.2!330!2!1!3\n"+
"1.1.1.4.4.3.3.3.2.2.2.4.4.4.2.2!330!2!2!3\n"+
"1.1.1.1.3.3.3.3.4.4.4.4.2.2.2.2!-330!2!3!3\n"+
"1.1.1.2.2.2.2.1.1.1.4.4.4.3.3.3!-330!2!4!3\n"+
"1.1.1.1.1.3.3.3.4.4.4.4.4.2.2.2!-330!2!1!3\n"+
"1.1.4.4.4.3.3.3.1.1.1.4.4.2.2.2!-330!2!2!3\n"+
"4.4.4.4.1.1.1.1.3.3.3.3.2.2.2.2!-350!2!3!3\n"+
"2.2.2.1.1.1.1.2.2.2.4.4.4.3.3.3!-330!2!4!3\n"+
"1.1.1.3.3.3.3.3.4.4.4.4.2.2.2.2!-350!2!1!3\n"+
"1.1.4.4.4.3.3.3.1.1.1.4.4.2.2.2!-330!2!2!3\n"+
"1.1.1.1.3.3.3.4.4.4.4.2.2.2.2.2!-350!2!3!3\n"+
"1.1.1.1.1.3.3.3.3.4.4.4.4.2.2.2!-350!2!4!3\n"+
"1.1.1.4.4.3.3.3.2.2.2.4.4.4.2.2!-330!2!1!3\n"+
"1.1.1.1.1.3.3.3.3.3.2.2.2.2.2.2!300!2!3!3\n"+
"1.1.1.1.3.3.3.3.1.1.1.1.3.3.3.3!-300!2!1!3\n"+
"4.4.4.4.4.4.1.1.4.4.4.4.4.4.1.1!270!2!1!3\n"+
"2.2.2.2.3.3.3.2.2.2.2.2.3.3.3.2!-290!2!3!3\n"+
"4.4.4.4.3.3.3.3.2.2.2.2.1.1.1.1!270!2!3!3\n"+
"4.4.4.4.3.3.3.3.2.2.2.2.1.1.1.1!270!2!3!2\n"+
"2.2.2.2.3.3.3.3.3.3.3.3.3.3.3.3!250!2!2!1\n"+
"1.1.1.1.1.1.1.1.4.4.4.4.4.4.4.4!-360!2!1!1\n"+
"3.3.3.3.3.3.2.2.3.3.3.3.3.3.2.2!270!2!2!1\n"+
"2.2.2.2.4.4.4.4.2.2.2.2.4.4.4.4!-300!2!2!1\n"+
"4.4.4.4.4.4.3.3.4.4.4.4.4.4.3.3!-250!2!3!1\n"+
"3.3.3.3.2.2.2.2.3.3.3.3.2.2.2.2!-250!2!2!1\n"+
"3.3.3.3.3.4.4.4.3.3.3.3.3.4.4.4!230!2!4!1\n";


    string s = "1.1.4.4.4.3.3.3.1.1.1.4.4.2.2.2!230!2!3!2\n"+
"1.1.1.4.4.3.3.3.1.1.1.4.4.2.2.2!230!2!4!2\n"+
"1.1.2.2.3.3.4.4.1.1.4.4.2.2.3.3!250!2!2!2\n"+
"1.1.1.2.2.2.2.1.1.1.4.4.4.3.3.3!230!2!2!2\n"+
"3.3.4.4.4.2.2.2.1.1.1.4.4.2.2.2!260!2!1!2\n"+
"1.1.2.2.2.3.3.3.1.1.3.3.4.4.4.4!260!2!4!2\n"+
"3.3.2.2.2.4.4.1.1.4.4.2.2.3.3.3!250!2!2!2\n"+
"4.4.2.2.2.3.3.3.4.4.3.3.1.1.1.1!260!2!1!2\n"+
"1.1.2.2.3.3.4.4.1.1.4.4.2.2.3.3!260!2!!2\n"+
"1.1.2.2.3.3.4.4.1.1.1.2.2.2.4.4!280!2!2!2\n"+
"1.1.4.4.2.2.2.1.1.1.4.4.3.3.3.3!260!2!1!2\n"+
"1.1.1.4.4.4.4.2.2.2.1.1.3.3.3.3!280!2!2!2\n"+
"1.1.2.2.3.3.1.1.1.4.4.2.2.3.3.3!-280!2!3!2\n"+
"1.1.4.4.4.3.3.3.1.1.1.4.4.2.2.2!-250!2!4!2\n"+
"1.1.1.4.4.3.3.3.1.1.4.4.4.2.2.2!-250!2!1!2\n"+
"1.1.2.2.3.3.1.1.1.4.4.2.2.3.3.3!-280!2!2!2\n"+
"1.1.1.2.2.2.1.1.1.4.4.4.3.3.3.3!-260!2!3!2\n"+
"3.3.4.4.4.2.2.2.1.1.1.4.4.2.2.2!-240!2!4!2\n"+
"1.1.2.2.2.3.3.1.1.1.3.3.4.4.4.4!-240!2!1!2\n"+
"3.3.2.2.4.4.4.1.1.1.2.2.2.3.3.3!-260!2!2!2\n"+
"4.4.2.2.2.3.3.3.4.4.3.3.1.1.1.1!-260!2!3!2\n"+
"1.1.2.2.4.4.4.1.1.1.2.2.2.3.3.3!-280!2!4!2\n"+
"1.1.1.3.3.2.2.1.1.4.4.2.2.2.4.4!-280!2!1!2\n"+
"1.1.4.4.2.2.2.1.1.1.4.4.3.3.3.3!-260!2!2!2\n"+
"1.1.1.4.4.4.4.2.2.2.1.1.3.3.3.3!-280!2!3!2\n"+
"1.1.1.1.3.3.3.3.4.4.4.4.2.2.2.2!330!2!4!3\n"+
"1.1.1.2.2.2.2.1.1.1.4.4.4.3.3.3!350!2!1!3\n"+
"1.1.1.1.1.3.3.3.4.4.4.4.4.2.2.2!330!2!2!3\n"+
"1.1.4.4.4.3.3.3.1.1.1.4.4.2.2.2!330!2!3!3\n"+
"4.4.4.4.1.1.1.1.3.3.3.3.2.2.2.2!350!2!4!3\n"+
"2.2.2.1.1.1.1.2.2.2.4.4.4.3.3.3!350!2!1!3\n"+
"1.1.1.3.3.3.3.3.4.4.4.4.2.2.2.2!350!2!2!3\n"+
"1.1.4.4.4.3.3.3.1.1.1.4.4.2.2.2!330!2!3!3\n"+
"1.1.1.1.3.3.3.4.4.4.4.2.2.2.2.2!350!2!4!3\n"+
"1.1.1.1.1.3.3.3.3.4.4.4.4.2.2.2!330!2!1!3\n"+
"1.1.1.4.4.3.3.3.2.2.2.4.4.4.2.2!330!2!2!3\n"+
"1.1.1.1.3.3.3.3.4.4.4.4.2.2.2.2!-330!2!3!3\n"+
"1.1.1.2.2.2.2.1.1.1.4.4.4.3.3.3!-330!2!4!3\n"+
"1.1.1.1.1.3.3.3.4.4.4.4.4.2.2.2!-330!2!1!3\n"+
"1.1.4.4.4.3.3.3.1.1.1.4.4.2.2.2!-330!2!2!3\n"+
"4.4.4.4.1.1.1.1.3.3.3.3.2.2.2.2!-350!2!3!3\n"+
"2.2.2.1.1.1.1.2.2.2.4.4.4.3.3.3!-330!2!4!3\n"+
"1.1.1.3.3.3.3.3.4.4.4.4.2.2.2.2!-350!2!1!3\n"+
"1.1.4.4.4.3.3.3.1.1.1.4.4.2.2.2!-330!2!2!3\n"+
"1.1.1.1.3.3.3.4.4.4.4.2.2.2.2.2!-350!2!3!3\n"+
"1.1.1.1.1.3.3.3.3.4.4.4.4.2.2.2!-350!2!4!3\n"+
"1.1.1.4.4.3.3.3.2.2.2.4.4.4.2.2!-330!2!1!3\n"+
"1.1.1.1.1.3.3.3.3.3.2.2.2.2.2.2!300!2!3!3\n"+
"1.1.1.1.3.3.3.3.1.1.1.1.3.3.3.3!-300!2!1!3\n"+
"4.4.4.4.4.4.1.1.4.4.4.4.4.4.1.1!270!2!1!3\n"+
"2.2.2.2.3.3.3.2.2.2.2.2.3.3.3.2!-290!2!3!3\n"+
"4.4.4.4.3.3.3.3.2.2.2.2.1.1.1.1!270!2!3!3\n"+
"4.4.4.4.3.3.3.3.2.2.2.2.1.1.1.1!270!2!3!2\n"+
"2.2.2.2.3.3.3.3.3.3.3.3.3.3.3.3!250!2!2!1\n"+
"1.1.1.1.1.1.1.1.4.4.4.4.4.4.4.4!-360!2!1!1\n"+
"3.3.3.3.3.3.2.2.3.3.3.3.3.3.2.2!270!2!2!1\n"+
"2.2.2.2.4.4.4.4.2.2.2.2.4.4.4.4!-300!2!2!1\n"+
"4.4.4.4.4.4.3.3.4.4.4.4.4.4.3.3!-250!2!3!1\n"+
"3.3.3.3.2.2.2.2.3.3.3.3.2.2.2.2!-250!2!2!1\n"+
"3.3.3.3.3.4.4.4.3.3.3.3.3.4.4.4!230!2!4!1";
}
