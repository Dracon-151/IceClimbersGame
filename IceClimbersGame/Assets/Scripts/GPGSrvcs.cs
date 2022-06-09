using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GPGSrvcs: MonoBehaviour
{
    public static PlayGamesPlatform platform;

    public bool success;

    private void Start()
    {
        if (PlayerPrefs.GetFloat("Sesion") == 0)
        {
            login();
        }
    }

    public void login()
    {
        if (platform == null)
        {
            PlayGamesPlatform.Activate();
        }

        Social.Active.localUser.Authenticate(success =>
        {
            if (success) Debug.Log("Logged in successfully");
            else
            {
                Debug.Log("Not logged in");
                PlayerPrefs.SetFloat("Sesion", 1);
                PlayerPrefs.Save();
            }
        });
    }

    public bool user()
    {
        return Social.localUser.authenticated;
    }

}
