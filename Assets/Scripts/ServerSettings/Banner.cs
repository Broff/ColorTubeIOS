using UnityEngine;
using System.Collections;

public class Banner {

	public Sprite bannerTexture;
	public int chancePercent;
	public string name;
	public string url;
	public bool resLoad = false;
	
	public Banner(int percent, string name, string url){
		this.url = url;
		this.name = name;
		this.chancePercent = percent;
		//load.GetComponent<TextureLoadManager>().loadTexture(texture);
		//this.bannerTexture = load.GetComponent<TextureLoadManager>().getTexture();
	}
}
