using UnityEngine;
using System.Collections;

public class SettingsLoadManager : MonoBehaviour {

	public delegate void LoadController(int err, WWW www);
	static SettingsLoadManager instance;
	public const int OK = 100;														
	public const int ERROR = 101;		
	
	void Start(){
		if(instance!=null){
            Destroy(gameObject);
            return;
        }
	    instance = this;
        DontDestroyOnLoad (gameObject);
	}
	
	IEnumerator loadXml(string url, LoadController controller)
	{
		WWW www = new WWW(url);
		yield return www;
		if (www.error == null)
			controller (OK, www);
		else 
			controller (ERROR, www);
	}
	
	public void loadSettings(string urlAddr, LoadController load){
		StartCoroutine(loadXml(urlAddr, load));
	}
}
