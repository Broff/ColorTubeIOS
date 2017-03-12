using UnityEngine;
using System.Collections;
//using NativeAlert;

public class ShareNotification : MonoBehaviour {
	
	bool show = false;

	public void Show(){
		show = true;
	}
	
	void OnEnable()
	{
        //NativeAlertListener.onFinish += OnAlertFinish;
        //NativeAlertListener.onCancel += OnAlertCancel;
	}

	void OnDisable()
	{
        //NativeAlertListener.onFinish -= OnAlertFinish;
        //NativeAlertListener.onCancel -= OnAlertCancel;
	}

	void OnAlertFinish(string clickedBtn)
	{
		show = false;
		////if (clickedBtn == "Yes") {
		//} 
	}

	void OnAlertCancel()
	{
		log += "\n Cancelled";
	}

	string log = "";
	void OnGUI()
	{
		if(show == true){
			//GUILayout.Label (log);
			//Rect rect = new Rect (Screen.width/2 - 75,Screen.height/2-15,150,30);
			//if (GUI.Button (rect,"Change Color")) {
			//#if UNITY_ANDROID {
				//AndroidNativeAlert.ShowAlert("Опубликовано","Пост опубликован на вашей стене!", "Ок");
			//#elif UNITY_IPHONE
				//IOSNativeAlert.ShowAlert("Rate","Do you want rate Color Tube?", "Yes", "No");
			//#endif
			//}
		}
	}
}
