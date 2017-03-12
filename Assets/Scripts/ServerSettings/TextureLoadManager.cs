using UnityEngine;
using System.Collections;

public class TextureLoadManager : MonoBehaviour {

	TextureLoadManager instance = null;
	public bool load = false;

	void Start(){
		if(instance!=null){
            Destroy(gameObject);
            return;
        }
	    instance = this;
        DontDestroyOnLoad (gameObject);
	}
	
	IEnumerator loadImg(string url, Banner ban)
	{
			WWW www = new WWW(url);
			yield return www;
			Sprite tex = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), Vector2.zero);
			ban.bannerTexture = Instantiate(tex);
			ban.resLoad = true;
	}
	
	public void loadTexture(string urlAddr, Banner b){
		StartCoroutine(loadImg(urlAddr, b));
	}
	
	void OnGUI()
	{
		//if (tex != null)
				//GUI.DrawTexture(new Rect(0, 0, 150, 200), tex, ScaleMode.ScaleToFit, true);
	}
}
