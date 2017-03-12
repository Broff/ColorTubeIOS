using UnityEngine;
using System.Collections;

public class DeleteZoneController : MonoBehaviour {
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.name != "DeadZone") {
			Destroy(other.gameObject);
		}
	} 
	
	void OnCollisionEnter(Collision collis) 
	{ 			
		if (collis.gameObject.name != "DeadZone") {
			Destroy(collis.gameObject);
		}	
	} 
}
