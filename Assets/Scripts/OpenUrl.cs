using UnityEngine;
using System.Collections;

public class OpenUrl : MonoBehaviour {
	public string Url = "http://";
	public void Click()
	{
		Application.OpenURL(Url);
	}
}
