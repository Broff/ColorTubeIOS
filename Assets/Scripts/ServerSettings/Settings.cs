using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;
using System;

public class Settings : MonoBehaviour {

	delegate void WaitAndStart();
	WaitAndStart waitMethod;

	public static Settings instance = null;
	public string loadSettUrl = "";
    public string facebookUrl = "http://";
	public GameObject gameSettings;
	public GameObject loadSettingsManager;
	public GameObject loadManager;
	public GameObject bannerScreen;
	static List<Banner> banners = new List<Banner>();
	static int frequency = 1;
	static int ads_percent = 50;
	static int gameCount = 0;
	static int launchNum;
	
	public static bool appOff = false;
	public static Banner appOffImg;
	
	public static bool appOffUpdate = false;
	public static Banner appOffUpdateImg;
	
	static bool loadSettings = false;
	
	public static bool nowADSShow = false;
	
	void Start () {
		Settings.gameCount++;
		if(instance!=null){
            Destroy(gameObject);
			//load();	
            return;
        }
		Settings.launchNum = PlayerPrefs.GetInt("LaunchCount");
		Settings.launchNum++;
		PlayerPrefs.SetInt("LaunchCount", Settings.launchNum);
		Debug.Log(Settings.launchNum);
	    instance = this;
        DontDestroyOnLoad (gameObject);
		//load();	
	}

    public void FacebookClick()
    {
        Application.OpenURL(facebookUrl);
    }
	
	public void showADS(){	

		if( launchNum % 2 != 0 && PlayerPrefs.GetInt("rateState") == 0 && gameCount == 1){
			rateBtn();
		} else {
			waitMethod = show;		
			StartCoroutine(startAfter(0.5f,waitMethod));
		}		
	}
	
	IEnumerator startAfter(float waitTime, WaitAndStart w)
	{
		yield return new WaitForSeconds(waitTime);
		w();
	}
	
	void rateBtn(){
		gameObject.GetComponent<AlertRateScript>().ShowRate(gameSettings);
	}
	
	void show(){
		if(appOff == true){
			if(appOffImg.resLoad == true){
			Debug.Log("APPOFF");
				bannerScreen.GetComponent<ADSScreenController>().ViewUpdate("");
				bannerScreen.transform.Find("Bg").gameObject.GetComponent<Image>().sprite = appOffImg.bannerTexture;
			} else {
			Debug.Log("APPOFF_1");
				showADS();
			}
		} else if(appOffUpdate == true){
			if(appOffUpdateImg.resLoad == true){
			Debug.Log("APPUPD");
				bannerScreen.GetComponent<ADSScreenController>().ViewUpdate(appOffUpdateImg.url);
				bannerScreen.transform.Find("Bg").gameObject.GetComponent<Image>().sprite = appOffUpdateImg.bannerTexture;
			} else {
			Debug.Log("APPUPD_1");
				showADS();
			}
		} else if(banners[1].resLoad == true){
		Debug.Log("ADS");
			Settings.nowADSShow = true;			
			bannerScreen.GetComponent<ADSScreenController>().View(banners[0].url);
			bannerScreen.transform.Find("Bg").gameObject.GetComponent<Image>().sprite = banners[0].bannerTexture;
			Debug.Log("SHOW");
		} else {
		Debug.Log("ADS_1");
			showADS();
		}		
	}
	
	void load(){		
		if(loadSettings == false && connState() == true){
			loadSettingsManager.GetComponent<SettingsLoadManager>().loadSettings(loadSettUrl, (int code, WWW www) => {
				if (code == 100) {
					string html = Encoding.UTF8.GetString (www.bytes, 0, www.bytes.Length);
					XmlDocument doc = new XmlDocument();				
					doc.LoadXml(html);
					foreach(XmlNode node in doc.SelectNodes("settings")) {
						foreach(XmlNode child in node.ChildNodes){
							if(child.Name == "app_off"){
								app_offParse(child);
							}
							
							if(child.Name == "app_update"){
								app_updateParse(child);
							}
							
							if(child.Name == "ads_frequence"){
								ads_frequenceParse(child);
							}
							
							if(child.Name == "ads_percent"){
								ads_percentParse(child);
							}
							
							if(child.Name == "banners"){
								bannersParse(child);
							}
						}					
					}
					Settings.loadSettings = true;
					Debug.Log("load xml true");
				} 
			});			
		}
		
		if(connState() == false){
			Debug.Log("NoConnection");
		}
	}
	
	void app_offParse(XmlNode node){
		foreach(XmlNode child in node.ChildNodes){
            if(child.Name == "val"){
				if(child.InnerText == "1"){
					appOff = true;
				} else {
					appOff = false;
				}
			}
			
			if(child.Name == "img"){
				appOffImg = new Banner(0, "",  "");
				loadManager.GetComponent<TextureLoadManager>().loadTexture(child.InnerText, appOffImg);
			}
		}
	}
	
	void app_updateParse(XmlNode node){
		string off = "";
		string version = "";
		string url = "";
		string img = "";
		foreach(XmlNode child in node.ChildNodes){
            if(child.Name == "val"){
				off = child.InnerText;
				Debug.Log(child.InnerText);
			}
			
			if(child.Name == "version"){
				 version = (child.InnerText);
				 Debug.Log(child.InnerText);
			}
			
			if(child.Name == "url"){
				url = (child.InnerText);
				Debug.Log(child.InnerText);
			}
			
			if(child.Name == "img"){
				img = (child.InnerText);
				Debug.Log(child.InnerText);
			}
		}
		Debug.Log(Application.version);
		if(off == "1"){
			string[] ver = version.Split('.');
			string[] verApp = Application.version.Split('.');
			if(Convert.ToInt32(verApp[0]) < Convert.ToInt32(ver[0])){
				appOffUpdate = true;
				appOffUpdateImg = new Banner(0, "",  url);
				loadManager.GetComponent<TextureLoadManager>().loadTexture(img, appOffUpdateImg);
			} else if(Convert.ToInt32(verApp[0]) >= Convert.ToInt32(ver[0])){
				if(Convert.ToInt32(verApp[1]) <= Convert.ToInt32(ver[1])){
					appOffUpdate = true;
					appOffUpdateImg = new Banner(0, "",  url);
					loadManager.GetComponent<TextureLoadManager>().loadTexture(img, appOffUpdateImg);
				}
				else 
				{
					appOffUpdate = false;
				}
			}
		} else {
			appOffUpdate = false;
		}
	}
	
	void ads_frequenceParse(XmlNode node){
		foreach(XmlNode child in node.ChildNodes){
            if(child.Name == "val"){
				Settings.frequency = Convert.ToInt32(child.InnerText);
			}
		}
	}
	
	void ads_percentParse(XmlNode node){
		foreach(XmlNode child in node.ChildNodes){
            if(child.Name == "val"){
				Settings.ads_percent = Convert.ToInt32(child.InnerText);
			}
		}
	}
	
	void bannersParse(XmlNode node){
		foreach(XmlNode child in node.ChildNodes){
            if(child.Name == "banner"){
				string name="";
				string url="";
				string img="";
				int percent=0;
				foreach(XmlNode childB in child.ChildNodes){
					if(childB.Name == "name"){
						name = (childB.InnerText);
					}
					
					if(childB.Name == "percent"){
						percent = Convert.ToInt32(childB.InnerText);
					}
					
					if(childB.Name == "url"){
						url = (childB.InnerText);
					}
					
					if(childB.Name == "img"){
						img = (childB.InnerText);
					}					
				}
				Banner b = new Banner(percent, name,  url);
				loadManager.GetComponent<TextureLoadManager>().loadTexture(img, b);
				Settings.banners.Add(b);
			}
		}
	}
	
	bool connState()
	{
		string HtmlText = GetHtmlFromUri("http://google.com");
		if(HtmlText == "")
		{
			return false;
		}
		else if(!HtmlText.Contains("schema.org/WebPage"))
		{
			return false;
		}
		else
		{
			return true;
		}
	}
	
	public string GetHtmlFromUri(string resource)
	{
		string html = string.Empty;
		HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
		try
		{
			using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
			{
				bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
				if (isSuccess)
				{
					using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
					{
						//We are limiting the array to 80 so we don't have
						//to parse the entire html document feel free to 
						//adjust (probably stay under 300)
						char[] cs = new char[80];
						reader.Read(cs, 0, cs.Length);
						foreach(char ch in cs)
						{
						 html +=ch;
						}
					}
				}
			}
		}
		catch
		{
		 return "";
		}
		return html;
	}
}
