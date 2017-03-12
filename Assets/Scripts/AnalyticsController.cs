using UnityEngine;
using System.Collections;

public class AnalyticsController : MonoBehaviour {

	//public GoogleAnalyticsV3 googleAnalytics;
	public static AnalyticsController instance = null;
	// Use this for initialization
	void Start () {
		if(instance!=null){
            Destroy(gameObject);
            return;
        }
	    instance = this;
        DontDestroyOnLoad (gameObject);
		//googleAnalytics.StartSession();
	}
}
