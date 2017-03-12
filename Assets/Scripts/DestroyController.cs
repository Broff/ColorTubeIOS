using UnityEngine;
using System.Collections;

public class DestroyController : MonoBehaviour {

    public float destroyTime;
	public void DestroyInit(){
	    Destroy(gameObject, destroyTime);
	}
}
