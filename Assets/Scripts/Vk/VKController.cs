using UnityEngine;
using com.playGenesis.VkUnityPlugin;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using com.playGenesis.VkUnityPlugin.MiniJSON;

public class VKController : MonoBehaviour {

    int nScreen = 0;
    public GameObject loginButton, inviteButtonVKG, inviteButtonIGG, vkScreen, screen_1, screen_2, userObject, gameScreen;
    public Sprite loginSprite, logoutSprite, inviteVKGSprite, inviteIGGSprite, goVKGSprite, goIGGSprite;
    Downloader downloader;
    VkApi vkApi;
    VKUser user;
    float time;
    GameObject[] screens = new GameObject[3];
   // public GoogleAnalyticsV3 googleAnalytics;
    bool isMemberVKG = false, isMemberIGG = false, isLoad = false;

    void Start() {
        screens[1] = screen_1;
        screens[2] = screen_2;
        vkApi = VkApi.VkApiInstance;
        downloader = vkApi.gameObject.GetComponent<Downloader>();
        IsMemberIGG();
        IsMemberVKG();
        if (vkApi.IsUserLoggedIn) {
            GetUserData();
        }
    }

    void Update() {
        if (time > 1)
        {
            IsMemberIGG();
            IsMemberVKG();
            CheckSprites();
            if (vkApi.IsUserLoggedIn && !isLoad) {
                GetUserData();
                isLoad = true;
            }
            time = 0;
        }
        else {
            time += Time.deltaTime;
        }
    }

    public void VKScreen(GameObject screen) {
        //googleAnalytics.LogEvent("ButtonClick", "VK", "Clicks", 1);
        screens[0] = screen;
        nScreen++;
        if (screen.name == "DeadScreen")
        {
            screen.SetActive(false);
            gameScreen.SetActive(false);
        }
        else {
            screen.SetActive(false);
        }
        
        vkScreen.SetActive(true);
        screens[nScreen].SetActive(true);
    }

    public void BackButton() {
        screens[nScreen].SetActive(false);
        nScreen--;
        if (nScreen == 0) {
            vkScreen.SetActive(false);
            screens[nScreen].SetActive(true);
            if (screens[0].name == "DeadScreen") {
                gameScreen.SetActive(true);
            }
        }
        else {
            screens[nScreen].SetActive(true);
        }
    }

    public void InviteFriend() {
        screens[nScreen].SetActive(false);
        nScreen++;
        screens[nScreen].SetActive(true);
    }

    public void Login_Logout() {
        if (vkApi.IsUserLoggedIn) {
            vkApi.Logout();
            isMemberIGG = false;
            isMemberVKG = false;
            DeleteUserData();
            CheckSprites();
        }
        else {
            vkApi.Login();
        }
    }

    public void IndieGamesGroup() {
        if (vkApi.IsUserLoggedIn)
        {
            if (isMemberIGG)
            {
                Application.OpenURL("https://vk.com/indiegameco");
            }
            else
            {
                JoinIGG();
            }
        }
        else
        {
            Login_Logout();
        }
    }

    public void VKGamesGroup() {
        if (vkApi.IsUserLoggedIn)
        {
            if (isMemberVKG)
            {
                Application.OpenURL("https://vk.com/vkgames");
            }
            else
            {
                JoinVKG();
            }
        }
        else {
            Login_Logout();
        }
    }

    void JoinVKG() {
        var request = new VKRequest()
        {
            url = "groups.join?group_id=78616012&v=" + VkApi.VkSetts.apiVersion,
            CallBackFunction = OnGetDataCompletedVKG,
        };
        vkApi.Call(request);
    }

    void OnGetDataCompletedVKG(VKRequest arg) {
        if (arg.error != null)
        {
            print("ERROR");
            FindObjectOfType<GlobalErrorHandler>().Notification.Notify(arg);
            return;
        }
        var dict = Json.Deserialize(arg.response) as Dictionary<string, object>;
        long member = (long)dict["response"];
        if (member == 1)
        {
            isMemberVKG = true;
        }
        else {
            isMemberVKG = false;
        }
        //CheckSprites();
    }

    void JoinIGG() {
        var request = new VKRequest()
        {
            url = "groups.join?group_id=127474704&v=" + VkApi.VkSetts.apiVersion,
            CallBackFunction = OnGetDataCompletedIGG,
        };
        vkApi.Call(request);
    }

    void OnGetDataCompletedIGG(VKRequest arg)
    {
        if (arg.error != null)
        {
            print("ERROR");
            FindObjectOfType<GlobalErrorHandler>().Notification.Notify(arg);
            return;
        }
        var dict = Json.Deserialize(arg.response) as Dictionary<string, object>;
        long member = (long)dict["response"];
        if (member == 1)
        {
            isMemberIGG = true;
        }
        else {
            isMemberIGG = false;
        }
        //CheckSprites();
    }

    void IsMemberIGG() {
        var request = new VKRequest()
        {
            url = "groups.isMember?group_id=indiegameco&user_id=" + VkApi.CurrentToken.user_id + "&v=" + VkApi.VkSetts.apiVersion,
            CallBackFunction = OnGetDataIsMemberIGG,
        };
        vkApi.Call(request);
    }

    void OnGetDataIsMemberIGG(VKRequest arg) {
        if (arg.error != null)
        {
            print("ERROR");
            FindObjectOfType<GlobalErrorHandler>().Notification.Notify(arg);
            return;
        }
        var dict = Json.Deserialize(arg.response) as Dictionary<string, object>;
        long member = (long)dict["response"];
        if (member == 1)
        {
            isMemberIGG = true;
        }
        else {
            isMemberIGG = false;
        }
        //CheckSprites();
    }

    void IsMemberVKG() {
        var request = new VKRequest()
        {
            url = "groups.isMember?group_id=vkgames&user_id=" + VkApi.CurrentToken.user_id + "&v=" + VkApi.VkSetts.apiVersion,
            CallBackFunction = OnGetDataIsMemberVKG,
        };
        vkApi.Call(request);
    }

    void OnGetDataIsMemberVKG(VKRequest arg) {
        if (arg.error != null)
        {
            print("ERROR");
            FindObjectOfType<GlobalErrorHandler>().Notification.Notify(arg);
            return;
        }
        var dict = Json.Deserialize(arg.response) as Dictionary<string, object>;
        long member = (long)dict["response"];
        if (member == 1)
        {
            isMemberVKG = true;
        }
        else {
            isMemberVKG = false;
        }
        //CheckSprites();
    }

    void GetUserData() {
        var request = new VKRequest()
        {
            url = "users.get?user_ids=" + VkApi.CurrentToken.user_id + "&fields=photo_200&v=" + VkApi.VkSetts.apiVersion,
            CallBackFunction = OnGetUserData,
        };
        vkApi.Call(request);
    }

    void OnGetUserData(VKRequest arg) {
        var dict = Json.Deserialize(arg.response) as Dictionary<string, object>;
        var items = (List<object>)dict["response"];
        foreach (var item in items) {
            user = VKUser.Deserialize(item);
        }
        userObject.GetComponent<FriendManager>().t.text = user.first_name + " " + user.last_name;
        userObject.GetComponent<FriendManager>().friend = user; 
        Action <DownloadRequest> doOnFinish = (downloadRequest) =>
        {
            var friendCard = (FriendManager)downloadRequest.CustomData[0];
            friendCard.setUpImage(downloadRequest.DownloadResult.texture);

        };
        var request = new DownloadRequest
        {
            url = user.photo_200,
            onFinished = doOnFinish,
            CustomData = new object[] { userObject.GetComponent<FriendManager>() }
        };
        downloader.download(request);
    }

    void DeleteUserData() {
        userObject.GetComponent<FriendManager>().t.text = "Войдите в ВК";
        userObject.GetComponent<FriendManager>().i.sprite = userObject.GetComponent<FriendManager>().noPhoto;
        isLoad = false;
    }

    void CheckSprites() {
        if (vkApi.IsUserLoggedIn)
        {
            loginButton.GetComponent<Image>().sprite = logoutSprite;
        }
        else
        {
            loginButton.GetComponent<Image>().sprite = loginSprite;
        }
        if (isMemberVKG)
        {
            inviteButtonVKG.GetComponent<Image>().sprite = goVKGSprite;
        }
        else {
            inviteButtonVKG.GetComponent<Image>().sprite = inviteVKGSprite;
        }
        if (isMemberIGG)
        {
            inviteButtonIGG.GetComponent<Image>().sprite = goIGGSprite;
        }
        else {
            inviteButtonIGG.GetComponent<Image>().sprite = inviteIGGSprite;
        }
    }
}
