using UnityEngine;
using System;
using System.Collections.Generic;
using com.playGenesis.VkUnityPlugin;
using com.playGenesis.VkUnityPlugin.MiniJSON;

public class FriendsController : MonoBehaviour {

    public VkApi vkApi;
    public Downloader downloader;
    public List<VKUser> friends = new List<VKUser>();
    public VkSettings settings;
    public GameObject friendPrefab, scrollContent;
    public static float nFriend = 1;

    void Start()
    {
        settings = VkApi.VkSetts;
        vkApi = VkApi.VkApiInstance;
        downloader = vkApi.gameObject.GetComponent<Downloader>();
        if (vkApi.IsUserLoggedIn) {
            StartWorking();
        }
    }

    void StartWorking() {
        if (VKToken.TokenValidFor() < 120)
            vkApi.Login();
    }

    public void GetFriendsData() {
        var request = new VKRequest() {
            url = "friends.get?user_id=" + VkApi.CurrentToken.user_id + "&order=mobile&fields=photo_200&v=" + VkApi.VkSetts.apiVersion,
            CallBackFunction = OnGetFriendsCompleted,
        };
        vkApi.Call(request);
    }

    void OnGetFriendsCompleted(VKRequest arg) {
        if (arg.error != null)
        {
            FindObjectOfType<GlobalErrorHandler>().Notification.Notify(arg);
            return;
        }

        var dict = Json.Deserialize(arg.response) as Dictionary<string, object>;
        var resp = (Dictionary<string, object>)dict["response"];
        var items = (List<object>)resp["items"];

        foreach (var item in items)
        {
            friends.Add(VKUser.Deserialize(item));
        }
        nFriend = friends.Count;
        for (int i = 0; i < friends.Count; i++) {
            GameObject temp = Instantiate(friendPrefab);
            temp.transform.parent = scrollContent.transform;
            temp.transform.localScale = new Vector3(1, 1, 1);
            temp.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        var friendsOnScene = GameObject.FindObjectsOfType<FriendManager>();
        for (var i = 0; i < friends.Count; i++)
        {
            Action<DownloadRequest> doOnFinish = (downloadRequest) =>
            {
                var friendCard = (FriendManager)downloadRequest.CustomData[0];
                friendCard.setUpImage(downloadRequest.DownloadResult.texture);

            };
            friendsOnScene[i].t.text = friends[i].first_name + " " + friends[i].last_name;
            friendsOnScene[i].friend = friends[i];
            var request = new DownloadRequest
            {
                url = friends[i].photo_200,
                onFinished = doOnFinish,
                CustomData = new object[] { friendsOnScene[i] }
            };
            downloader.download(request);
        }
    }
}

